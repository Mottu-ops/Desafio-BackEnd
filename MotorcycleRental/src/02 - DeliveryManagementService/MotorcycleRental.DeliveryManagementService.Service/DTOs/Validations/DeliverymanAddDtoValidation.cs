using FluentValidation;

namespace MotorcycleRental.DeliveryManagementService.Service.DTOs.Validations
{
    public class DeliverymanAddDtoValidation : AbstractValidator<DeliverymanAddDto>
    {
        public DeliverymanAddDtoValidation()
        {
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
                .Must(x => x.Length < 14 || x.Length > 18)
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

        private bool BeValidDate(DateTime date)
        {
            // Verifica se a data é maior que 01/01/1900
            if (date < new DateTime(1900, 1, 1))
                return false;

            // Verifica se é menor de 18 anos
            if ((DateTime.Now - date).TotalDays / 365.25 < 18)
                return false;

            return true;
        }
    }
}
