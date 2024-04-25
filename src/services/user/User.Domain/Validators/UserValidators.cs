using FluentValidation;
using User.Domain.Entities;

namespace User.Domain.Validators {
    public class UserValidator : AbstractValidator<Partner> {

        public UserValidator() {
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
                .Length(9).WithMessage("The CNH must have only nine digits.");

            RuleFor(x => x.Cnpj)
                .NotEmpty().WithMessage("The CNPJ number can't be empty.")
                .NotNull().WithMessage("The CNPJ number can't be null.")
                .Matches(@"[0-9]{2}\.?[0-9]{3}\.?[0-9]{3}\/?[0-9]{4}\-?[0-9]{2}").WithMessage("CNPJ invalid");

            RuleFor(x => x.DateBirth)
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

            RuleFor(x => x.Role).
                NotEmpty().WithMessage("The role can't be empty.")
                .NotNull().WithMessage("The role can't be null.")
                .Must(RoleValidation).WithMessage("The role values must be only 'user' or 'admin'.");

            
        }

        private bool CnhTypeValidation(string cnhType) {
            var list = new  List<string> {"A", "B", "AB"};
            return list.Contains(cnhType);
        }
        private bool RoleValidation(string cnhType) {
            var list = new  List<string> {"user", "admin"};
            return list.Contains(cnhType);
        }
    }
}