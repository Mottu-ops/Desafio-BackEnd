using FluentValidation;
using System.Text.RegularExpressions;
using User.Domain.Entities;
using static User.Domain.Entities.Client;

namespace User.Domain.Validators
{
    public class Validator : AbstractValidator<Client>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("The name can't be empty.")
                .NotNull().WithMessage("The name can't be null.")
                .MaximumLength(80).WithMessage("The name must have the maximum of one hundred characters.")
                .MinimumLength(2).WithMessage("The name must have at least two characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("The password can't be empty.")
                .NotNull().WithMessage("The password can't be null.")
                .MinimumLength(8).WithMessage("The Password must have at least eight characters.");

            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("The password can't be empty.")
                .NotNull().WithMessage("The password can't be null.")
                .EmailAddress().WithMessage("The e-mail must be valid");

            RuleFor(x => x.CnhNumber)
                .NotEmpty().WithMessage("The CNH number can't be empty.")
                .NotNull().WithMessage("The CHN number cant'be null.")
                .Length(11).WithMessage("The driver's license must only have eleven digits.");

            RuleFor(x => x.CPFCnpj)
                .NotEmpty().WithMessage("The CPF/CNPJ number can't be empty.")
                .NotNull().WithMessage("The CPF/CNPJ number can't be null.")
                .Must(CpfCnpjValidation).WithMessage("CPF/CNPJ invalid");

            RuleFor(x => x.Birth)
                .NotEmpty().WithMessage("The birth date can't be empty.")
                .NotNull().WithMessage("The birth date can't be null.")
                .LessThan(DateTime.Now).WithMessage("The birth date must be in the past.");

            RuleFor(x => x.CnhType).
                NotEmpty().WithMessage("The CNH's type can't be empty.")
                .NotNull().WithMessage("The CNH type can't be null.")
                .Must(CnhTypeValidation).WithMessage("The CNH type value must be only 'A', 'B' or 'AB'.");

            RuleFor(x => x.CnhImage).
                NotEmpty().WithMessage("The CNH's type can't be empty.")
                .NotNull().WithMessage("The CNH type can't be null.");

        }

        private bool CnhTypeValidation(string cnhType)
        {
            var list = new List<string> { "A", "B", "AB" };
            return list.Contains(cnhType);
        }

       

        private bool CpfCnpjValidation(string cpfcnpj)
        {
            if (cpfcnpj.Length == 11)
                return ValidateCPF(cpfcnpj);
            else if (cpfcnpj.Length == 14)
                return ValidateCNPJ(cpfcnpj);

            return false;

        }

        private bool ValidateCPF(string cpf)
        {
            return Regex.IsMatch(cpf, @"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$");
        }

        private bool ValidateCNPJ(string cnpj)
        {
            return Regex.IsMatch(cnpj, @"[0-9]{2}\.?[0-9]{3}\.?[0-9]{3}\/?[0-9]{4}\-?[0-9]{2}");
        }

    }
}
