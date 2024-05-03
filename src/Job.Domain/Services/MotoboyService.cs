using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Job.Domain.Commands;
using Job.Domain.Commands.User.Motoboy;
using Job.Domain.Commands.User.Motoboy.Validations;
using Job.Domain.Commons;
using Job.Domain.Entities.User;
using Job.Domain.Queries.User;
using Job.Domain.Repositories;
using Job.Domain.Services.Interfaces;

namespace Job.Domain.Services;

public sealed class MotoboyService(
    ILogger<MotoboyService> logger,
    IMotoboyRepository motoboyRepository) : IMotoboyService
{
    public async Task<CommandResponse<string>> CreateAsync(CreateMotoboyCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Iniciando o processo de criação de um motoboy");
        var validate = await new CreateMotoboyValidation().ValidateAsync(command, cancellationToken);

        logger.LogInformation("Criando objeto motoboy");
        var motoboyEntity = new MotoboyEntity(command.Password, command.Name, command.Cnpj, DateOnly.FromDateTime(command.DateBirth), command.Cnh, command.TypeCnh);

        await CheckDocumentAsync(validate, motoboyEntity, cancellationToken);

        if(!validate.IsValid)
        {
            logger.LogInformation("Erros de validação encontrados {errors}", validate.Errors);
            return new CommandResponse<string>(validate.Errors);
        }

        await motoboyRepository.CreateAsync(motoboyEntity, cancellationToken);

        logger.LogInformation("Motoboy criado com sucesso");
        return new CommandResponse<string>(motoboyEntity.Id);
    }

    public async Task<CommandResponse<MotoboyQuery?>> GetMotoboy(AuthenticationMotoboyCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Buscando motoboy {cnpj}", command.Cnpj);
        var validate = await new AuthenticationMotoboyValidation().ValidateAsync(command, cancellationToken);

        if (!validate.IsValid)
            return new CommandResponse<MotoboyQuery?>(validate.Errors);

        var motoboy = await motoboyRepository.GetAsync(CnpjValidation.FormatCnpj(command.Cnpj), Cryptography.Encrypt(command.Password), cancellationToken);

        if (motoboy is null)
        {
            logger.LogError("Motoboy não encontrado");
            return new CommandResponse<MotoboyQuery?>();
        }

        logger.LogInformation("Motoboy encontrado com sucesso");
        var query = new MotoboyQuery(motoboy.Id, motoboy.Cnpj);
        return new CommandResponse<MotoboyQuery?>
        {
            Data = query
        };

    }

    public async Task<CommandResponse<string>> UploadImageAsync(string cnpj, UploadCnhMotoboyCommand file, CancellationToken cancellationToken)
    {
        logger.LogInformation("Iniciando upload de imagem");

        var validationFailures = new List<ValidationFailure>();
        var permittedExtensions = new[] { ".png", ".bmp" };
        var extension = Path.GetExtension(file.FileDetails.FileName).ToLowerInvariant();
        var permittedMimeTypes = new[] { "image/png", "image/bmp" };
        if (!permittedMimeTypes.Contains(file.FileDetails.ContentType) && !permittedExtensions.Contains(extension))
        {
            logger.LogInformation("Tipo de arquivo inválido");
            validationFailures.Add(new ValidationFailure("File", "Tipo de arquivo inválido"));
            return new CommandResponse<string>(validationFailures);
        }

        logger.LogInformation("Buscando motoboy {cnpj}", cnpj);
        var motoboy = await motoboyRepository.GetByCnpjAsync(cnpj, cancellationToken);

        if (motoboy is null)
        {
            logger.LogError("Motoboy não encontrado");
            validationFailures.Add(new ValidationFailure("Cnpj", "Motoboy não encontrado"));
            return new CommandResponse<string>(validationFailures);
        }

        var stream = file.FileDetails.OpenReadStream();
        var path = await UploadImage(file.FileDetails.FileName, stream, cancellationToken);

        if(path is null)
        {
            logger.LogError("Erro ao realizar upload de imagem");
            validationFailures.Add(new ValidationFailure("File", "Erro ao realizar upload de imagem"));
            return new CommandResponse<string>(validationFailures);
        }

        motoboy.UpdateCnhImage(path);
        await motoboyRepository.UpdateAsync(motoboy, cancellationToken);

        logger.LogInformation("Upload de imagem realizado com sucesso");
        return new CommandResponse<string>(motoboy.Id)
        {
            Data = path
        };
    }

    #region Private Methods

    private async Task CheckDocumentAsync(ValidationResult validate, MotoboyEntity motoboyEntity,
        CancellationToken cancellationToken)
    {
        if(validate.IsValid && await motoboyRepository.CheckCnpjExistsAsync(motoboyEntity.Cnpj, cancellationToken))
        {
            logger.LogInformation("CNPJ já cadastrado {cnpj}", motoboyEntity.Cnpj);
            validate.Errors.Add(new ValidationFailure("Cnpj", "CNPJ já cadastrado"));
        }

        if(validate.IsValid && await motoboyRepository.CheckCnhExistsAsync(motoboyEntity.Cnh, cancellationToken))
        {
            logger.LogInformation("CNH já cadastrada {cnh}", motoboyEntity.Cnh);
            validate.Errors.Add(new ValidationFailure("Cnh", "CNH já cadastrada"));
        }
    }

    private static async Task<string?> UploadImage(string fileName, Stream stream, CancellationToken cancellationToken)
    {
        var account = new Account(
            "dpft0wjf0",
            "141897328334374",
            "atZyaFEhIZRcnLaOhnJVceydWXY");

        var cloudinary = new Cloudinary(account);
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(fileName, stream)
        };
        var result = await cloudinary.UploadAsync(uploadParams, cancellationToken);
        return  result?.SecureUrl.AbsoluteUri;
    }

    #endregion
}