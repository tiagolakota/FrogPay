using System.Text.RegularExpressions;

namespace FrogPay.Common
{
    public static class CnpjHelper
    {
        public static bool ValidarCnpj(string cnpj)
        {
            // Remover caracteres não numéricos
            cnpj = Regex.Replace(cnpj, "[^0-9]", "");

            // Verificar se o CNPJ tem 14 dígitos
            if (cnpj.Length != 14)
            {
                return false;
            }

            // Verificar se todos os dígitos são iguais, o que torna o CNPJ inválido
            if (new string(cnpj[0], cnpj.Length) == cnpj)
            {
                return false;
            }

            // Calcular os dois dígitos verificadores
            int[] peso1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma = 0;

            for (int i = 0; i < 12; i++)
            {
                soma += int.Parse(cnpj[i].ToString()) * peso1[i];
            }

            int resto = soma % 11;
            int digito1 = (resto < 2) ? 0 : 11 - resto;

            int[] peso2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            soma = 0;

            for (int i = 0; i < 13; i++)
            {
                soma += int.Parse(cnpj[i].ToString()) * peso2[i];
            }

            resto = soma % 11;
            int digito2 = (resto < 2) ? 0 : 11 - resto;

            // Verificar se os dígitos calculados coincidem com os dígitos fornecidos
            return int.Parse(cnpj[12].ToString()) == digito1 && int.Parse(cnpj[13].ToString()) == digito2;
        }
    }
}