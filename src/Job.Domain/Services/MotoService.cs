using Job.Domain.Commands;
using Job.Domain.Commands.Moto;
using Job.Domain.Commands.Moto.Validations;
using Job.Domain.Entities.Moto;
using Job.Domain.Queries.Moto;
using Job.Domain.Repositories;
using Job.Domain.Services.Interfaces;

namespace Job.Domain.Services;

public sealed class MotoService(
    ILogger<MotoService> logger,
    IMotoRepository motoRepository,
    IRentRepository rentRepository) : IMotoService
{
    public async Task<CommandResponse> CreateAsync(CreateMotoCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Iniciando o processo de criação de uma moto");
        var validator = await new CreateMotoValidation().ValidateAsync(command, cancellationToken);

        var moto = new MotoEntity(command.Year, command.Model, command.Plate);
        logger.LogInformation("Objeto moto criado com sucesso");
        if (validator.IsValid && await motoRepository.CheckPlateExistsAsync(moto.Plate, cancellationToken))
        {
            logger.LogInformation("Placa já cadastrada {plate}", moto.Plate);
            validator.Errors.Add(new ValidationFailure("Plate", "Placa já cadastrada"));
        }

        if (!validator.IsValid)
        {
            logger.LogInformation("Erros de validação encontrados {errors}", validator.Errors);
            return new CommandResponse(validator.Errors);
        }

        logger.LogInformation("Moto criada com sucesso");
        await motoRepository.CreateAsync(moto, cancellationToken);
        return new CommandResponse(moto.Id);
    }

    public async Task<CommandResponse> UpdateAsync(UpdateMotoCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Iniciando o processo de atualização de uma moto");
        var validator = await new UpdateMotoValidation().ValidateAsync(command, cancellationToken);

        var moto = await motoRepository.GetByIdAsync(command.Id, cancellationToken);
        if (moto is null)
        {
            logger.LogInformation("Moto não encontrada {id}", command.Id);
            validator.Errors.Add(new ValidationFailure("Id", "Moto não encontrada"));
        }

        if (!validator.IsValid)
        {
            logger.LogInformation("Erros de validação encontrados {errors}", validator.Errors);
            return new CommandResponse(validator.Errors);
        }

        moto!.Update(command.Year, command.Model, command.Plate);
        logger.LogInformation("Moto atualizada com sucesso");
        await motoRepository.UpdateAsync(moto, cancellationToken);
        return new CommandResponse(moto.Id);
    }

    public async Task<CommandResponse> DeleteAsync(Guid idMoto, CancellationToken cancellationToken)
    {
        logger.LogInformation("Iniciando o processo de exclusão de uma moto");
        var moto = await motoRepository.GetByIdAsync(idMoto, cancellationToken);
        if (moto is null)
        {
            logger.LogInformation("Moto não encontrada {id}", idMoto);
            return new CommandResponse(new List<ValidationFailure> { new("Id", "Moto não encontrada") });
        }

        var rent = await rentRepository.GetByMotoIdAsync(moto.Id, cancellationToken);

        if(rent is not null && rent.DateEnd > DateOnly.FromDateTime(DateTime.Now))
        {
            logger.LogInformation("Moto com aluguel ativo");
            return new CommandResponse(new List<ValidationFailure> { new("Rent", "Moto com aluguel ativo") });
        }

        logger.LogInformation("Moto excluída com sucesso");
        await motoRepository.DeleteAsync(moto, cancellationToken);
        return new CommandResponse(moto.Id);
    }

    public async Task<IEnumerable<MotoQuery>> GetAllAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Iniciando a busca de todas as motos");
        var motos = await motoRepository.GetAllAsync(cancellationToken);
        logger.LogInformation("Busca de todas as motos finalizada com sucesso");
        return motos.Select(moto => new MotoQuery(moto.Id, moto.Year, moto.Model, moto.Plate));
    }

    public async Task<MotoQuery?> GetByIdAsync(Guid idMoto, CancellationToken cancellationToken)
    {
        logger.LogInformation("Iniciando a busca de uma moto");
        var moto = await motoRepository.GetByIdAsync(idMoto, cancellationToken);
        if (moto is null)
        {
            logger.LogInformation("Moto não encontrada {id}", idMoto);
            return null;
        }

        logger.LogInformation("Busca de uma moto finalizada com sucesso");
        return new MotoQuery(moto.Id, moto.Year, moto.Model, moto.Plate);
    }
}