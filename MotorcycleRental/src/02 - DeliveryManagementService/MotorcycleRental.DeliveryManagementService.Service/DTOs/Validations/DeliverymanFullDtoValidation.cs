using FluentValidation;

namespace MotorcycleRental.DeliveryManagementService.Service.DTOs.Validations
{
    public class DeliverymanFullDtoValidation : AbstractValidator<DeliverymanFullDto>
    {
        public DeliverymanFullDtoValidation()
        {
            RuleFor(m => m.Id)
                .NotEmpty()
                .WithMessage("{PropertyName}: cannot be empty!")
                .NotNull()
                .WithMessage("{PropertyName}: Cannot be Null!");
            RuleFor(m => m.Name)
               .NotEmpty()
               .WithMessage("{PropertyName}: cannot be empty!")
               .NotNull()
               .WithMessage("{PropertyName}: Cannot be Null!");
            //Poderia criar um função para validar os digitos, mas vou deixar somente validando tamanho
            RuleFor(m => m.CNPJ)
                .NotEmpty()
                .WithMessage("{PropertyName}: cannot be empty!")
                .NotNull()
                .WithMessage("{PropertyName}: Cannot be Null!")
                .Must(x => x.Length >= 14 || x.Length <= 18)
                .WithMessage("Inválid CNPJ.");

            RuleFor(m => m.DriverLicenseNumber)
                .NotEmpty()
                .WithMessage("{PropertyName}: cannot be empty!")
                .NotNull()
                .WithMessage("{PropertyName}: Cannot be Null!");
            RuleFor(m => m.DriverLicenseType)
                .NotEmpty()
                .WithMessage("{PropertyName}: cannot be empty!")
                .NotNull()
                .WithMessage("{PropertyName}: Cannot be Null!");
        }
    }
}