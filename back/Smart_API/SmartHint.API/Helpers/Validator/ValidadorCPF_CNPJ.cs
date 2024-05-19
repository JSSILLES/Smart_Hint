namespace SmartHint.API.Helpers.Validator
{
    public class ValidadorCPF_CNPJ
    {
        public static bool ValidarCPF(string cpf)
        {
            cpf = cpf.Trim();

            // Remover caracteres não numéricos
            cpf = cpf.Replace(".", "").Replace("-", "");

            // Verificar se o CPF possui 11 dígitos
            if (cpf.Length != 11)
                return false;

            // Verificar se todos os dígitos são iguais, o que não é permitido
            bool todosDigitosIguais = true;
            for (int i = 1; i < cpf.Length; i++)
            {
                if (cpf[i] != cpf[0])
                {
                    todosDigitosIguais = false;
                    break;
                }
            }
            if (todosDigitosIguais)
                return false;

            // Calcular o primeiro dígito verificador
            int soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * (10 - i);
            }
            int resto = soma % 11;
            int digitoVerificador1 = resto < 2 ? 0 : 11 - resto;

            // Verificar o primeiro dígito verificador
            if (digitoVerificador1 != int.Parse(cpf[9].ToString()))
                return false;

            // Calcular o segundo dígito verificador
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * (11 - i);
            }
            resto = soma % 11;
            int digitoVerificador2 = resto < 2 ? 0 : 11 - resto;

            // Verificar o segundo dígito verificador
            if (digitoVerificador2 != int.Parse(cpf[10].ToString()))
                return false;

            // CPF válido
            return true;
        }

        public static bool ValidarCNPJ(string cnpj)
        {
            cnpj = cnpj.Trim();

            // Remover caracteres não numéricos
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            // Verificar se o CNPJ possui 14 dígitos
            if (cnpj.Length != 14)
                return false;

            // Verificar se todos os dígitos são iguais, o que não é permitido
            bool todosDigitosIguais = true;
            for (int i = 1; i < cnpj.Length; i++)
            {
                if (cnpj[i] != cnpj[0])
                {
                    todosDigitosIguais = false;
                    break;
                }
            }
            if (todosDigitosIguais)
                return false;

            // Calcular o primeiro dígito verificador
            int[] multiplicadores1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma = 0;
            for (int i = 0; i < 12; i++)
            {
                soma += int.Parse(cnpj[i].ToString()) * multiplicadores1[i];
            }
            int resto = soma % 11;
            int digitoVerificador1 = resto < 2 ? 0 : 11 - resto;

            // Verificar o primeiro dígito verificador
            if (digitoVerificador1 != int.Parse(cnpj[12].ToString()))
                return false;

            // Calcular o segundo dígito verificador
            int[] multiplicadores2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            soma = 0;
            for (int i = 0; i < 13; i++)
            {
                soma += int.Parse(cnpj[i].ToString()) * multiplicadores2[i];
            }
            resto = soma % 11;
            int digitoVerificador2 = resto < 2 ? 0 : 11 - resto;

            // Verificar o segundo dígito verificador
            if (digitoVerificador2 != int.Parse(cnpj[13].ToString()))
                return false;

            // CNPJ válido
            return true;
        }
    }
}
