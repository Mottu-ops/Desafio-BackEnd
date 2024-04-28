namespace Job.Domain.Commons;

public static class Cnh
{
    public static bool IsCnh(string cnh)
    {
        if (cnh == new string(Convert.ToChar(cnh[..1]), 11))
            return false;

        var digitoVerificador1 = GerarDigitoVerificador(cnh[..9], false);
        var diferencial = 0;
        if (digitoVerificador1 == 10)
        {
            digitoVerificador1 = 0;
            diferencial = 2;
        }

        var digitoVerificador2 = GerarDigitoVerificador(cnh[..9], true);
        digitoVerificador2 = digitoVerificador2 == 10 ? 0 : digitoVerificador2 - diferencial;

        var cnhReal = cnh[..9] + digitoVerificador1 + digitoVerificador2;
        return cnh == cnhReal;
    }

    private static int GerarDigitoVerificador(string digitos, bool crescente)
    {
        var soma = 0;
        var multiplicador = crescente ? 1 : 9;
        for (var indice = 0; indice < digitos.Length; indice++)
        {
            soma += int.Parse(digitos.Substring(indice, 1)) * multiplicador;
            multiplicador += crescente ? 1 : -1;
        }

        return soma % 11;
    }
}