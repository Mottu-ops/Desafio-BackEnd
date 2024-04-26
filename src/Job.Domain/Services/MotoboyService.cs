using Job.Domain.Commands;
using Job.Domain.Commands.User.Motoboy;
using Job.Domain.Commands.User.Motoboy.Validations;
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

        if(validate.IsValid && await motoboyRepository.CheckCnpjExistsAsync(motoboyEntity.Cnpj, cancellationToken))
        {
            logger.LogInformation("CNPJ já cadastrado {cnpj}", motoboyEntity.Cnpj);
            validate.Errors.Add(new ValidationFailure("Cnpj", "CNPJ já cadastrado"));
        }

        if(validate.IsValid && await motoboyRepository.CheckCnhExistsAsync(motoboyEntity.Document, cancellationToken))
        {
            logger.LogInformation("CNH já cadastrada {cnh}", motoboyEntity.Document);
            validate.Errors.Add(new ValidationFailure("Cnh", "CNH já cadastrada"));
        }

        if(!validate.IsValid)
        {
            logger.LogInformation("Erros de validação encontrados {errors}", validate.Errors);
            return new CommandResponse<string>(validate.Errors);
        }

        await motoboyRepository.CreateAsync(motoboyEntity, cancellationToken);


        logger.LogInformation("Motoboy criado com sucesso");
        return new CommandResponse<string>(motoboyEntity.Id);
    }

    public async Task<MotoboyQuery?> GetMotoboy(AuthenticationMotoboyCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Buscando motoboy {cnpj}", command.Cnpj);
        var motoboy = await motoboyRepository.GetAsync(command.Cnpj, command.Password, cancellationToken);

        if (motoboy is null)
        {
            logger.LogError("Motoboy não encontrado");
            return null;
        }

        logger.LogInformation("Motoboy encontrado com sucesso");
        return new MotoboyQuery(motoboy.Id, motoboy.Cnpj);
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

        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",  cnpj + extension);
        await using var stream = new FileStream(path, FileMode.OpenOrCreate);
        await file.FileDetails.CopyToAsync(stream, cancellationToken);

        motoboy.UpdateCnhImage(path);
        await motoboyRepository.UpdateAsync(motoboy, cancellationToken);

        logger.LogInformation("Upload de imagem realizado com sucesso");
        return new CommandResponse<string>(motoboy.Id);
    }
}