namespace Job.Domain.Commands.User.Motoboy.Validations;

public class CreateMotoboyValidation : AbstractValidator<CreateMotoboyCommand>
{
    public CreateMotoboyValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Nome é obrigatório");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Senha é obrigatória");

        RuleFor(x => x.Cnpj)
            .NotEmpty()
            .WithMessage("Cnpj é obrigatório")
            .Must(IsCnpj)
            .WithMessage("Cnpj inválido");

        RuleFor(x => x.DateBirth)
            .NotEmpty()
            .WithMessage("Data de nascimento é obrigatória")
            .LessThanOrEqualTo(DateTime.Now.AddYears(-18))
            .WithMessage("Data de nascimento deve ser maior que 18 anos");

        RuleFor(x => x.Cnh)
            .NotEmpty()
            .WithMessage("Cnh é obrigatório");

        RuleFor(x => x.TypeCnh)
            .NotEmpty()
            .WithMessage("Tipo de cnh é obrigatório");
    }

    private static bool IsCnpj(string cnpj)
    {
        var multiplicador1 = new[] {5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2};
        var multiplicador2 = new[] {6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2};
        cnpj = cnpj.Trim();
        cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
        if (cnpj.Length != 14)
            return false;
        var tempCnpj = cnpj[..12];
        var soma = 0;
        for (var i = 0; i < 12; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
        var resto = (soma % 11);
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        var digito = resto.ToString();
        tempCnpj += digito;
        soma = 0;
        for (var i = 0; i < 13; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
        resto = (soma % 11);
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito += resto;
        return cnpj.EndsWith(digito);
    }
}