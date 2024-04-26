using Job.Domain.Commands;
using Job.Domain.Commands.Rent;
using Job.Domain.Commands.Rent.Validations;
using Job.Domain.Entities.Rent;
using Job.Domain.Enums;
using Job.Domain.Repositories;
using Job.Domain.Services.Interfaces;

namespace Job.Domain.Services;

public sealed class RentService(
    ILogger<RentService> logger,
    IRentRepository rentRepository,
    IMotoRepository motoRepository,
    IMotoboyRepository motoboyRepository
) : IRentService
{
    public async Task<CommandResponse<string>> CreateRentAsync(CreateRentCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Iniciando o processo de criação de um aluguel");
        var validate = await new CreateRentValidation().ValidateAsync(command, cancellationToken);

        logger.LogInformation("Buscando motoboy");
        var motoboy = await motoboyRepository.GetByCnpjAsync(command.Cnpj, cancellationToken);

        if (motoboy is null)
        {
            logger.LogInformation("Motoboy não encontrado {Cnpj}", command.Cnpj);
            validate.Errors.Add(new ValidationFailure("IdMotoboy", "Motoboy não encontrado"));
        }

        if (motoboy?.Type == ECnhType.B)
        {
            logger.LogInformation("Moto boy não possuir CNH Tipo A");
            validate.Errors.Add(new ValidationFailure("TypeCnh", "Tipo de CNH não compatível"));
        }

        logger.LogInformation("Buscando moto");
        var moto = await motoRepository.GetByIdAsync(command.IdMoto, cancellationToken);
        if (moto is null)
        {
            logger.LogInformation("Moto não encontrada {idMoto}", command.IdMoto);
            validate.Errors.Add(new ValidationFailure("IdMoto", "Moto não encontrada"));
        }

        if (!validate.IsValid)
        {
            logger.LogInformation("Erros de validação encontrados {errors}", validate.Errors);
            return new CommandResponse<string>(validate.Errors);
        }

        logger.LogInformation("Criando objeto aluguel");
        var rentEntity = new RentEntity(motoboy!.Id, command.IdMoto, DateOnly.FromDateTime(command.DatePreview),
            command.Plan);

        await rentRepository.CreateAsync(rentEntity, cancellationToken);

        logger.LogInformation("Aluguel criado com sucesso");
        return new CommandResponse<string>(rentEntity.Id)
        {
            Data = $"Valor do aluguel é de R$ {rentEntity.Value} no plano de {rentEntity.Plan} Dias"
        };
    }

    public async Task<CommandResponse<string>> CancelRentAsync(CancelRentCommand command,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Iniciando o processo de cancelamento de um aluguel");
        var validate = await new CancelRentValidation().ValidateAsync(command, cancellationToken);

        logger.LogInformation("Buscando aluguel");
        var rent = await rentRepository.GetByIdAsync(command.Id, cancellationToken);
        if (rent is null)
        {
            logger.LogInformation("Aluguel não encontrado {id}", command.Id);
            validate.Errors.Add(new ValidationFailure("Id", "Aluguel não encontrado"));
        }

        if (!validate.IsValid)
        {
            logger.LogInformation("Erros de validação encontrados {errors}", validate.Errors);
            return new CommandResponse<string>(validate.Errors);
        }

        var fine = rent!.CalculateFine(DateOnly.FromDateTime(command.DatePreview));
        await rentRepository.UpdateAsync(rent, cancellationToken);
        var response = new CommandResponse<string>(rent.Id)
        {
            Data = "Valor da multa é de R$ " + fine
        };
        return response;
    }
}