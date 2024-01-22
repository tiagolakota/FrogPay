using System.Text.RegularExpressions;

namespace FrogPay.Common
{
    public static class CpfHelper
    {
        public static bool ValidarCpf(string cpf)
        {
            // Remover caracteres não numéricos
            cpf = Regex.Replace(cpf, "[^0-9]", "");

            // Verificar se o CPF tem 11 dígitos
            if (cpf.Length != 11)
            {
                return false;
            }

            // Verificar se todos os dígitos são iguais, o que torna o CPF inválido
            if (new string(cpf[0], cpf.Length) == cpf)
            {
                return false;
            }

            // Calcular os dois dígitos verificadores
            int[] peso1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * peso1[i];
            }

            int resto = soma % 11;
            int digito1 = (resto < 2) ? 0 : 11 - resto;

            int[] peso2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            soma = 0;

            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * peso2[i];
            }

            resto = soma % 11;
            int digito2 = (resto < 2) ? 0 : 11 - resto;

            // Verificar se os dígitos calculados coincidem com os dígitos fornecidos
            return int.Parse(cpf[9].ToString()) == digito1 && int.Parse(cpf[10].ToString()) == digito2;
        }
    }
}