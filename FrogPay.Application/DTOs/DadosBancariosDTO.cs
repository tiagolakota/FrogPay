namespace FrogPay.Application.Models
{
    public class DadosBancariosDTO
    {
        public Guid IdPessoa { get; set; }
        public string CodigoBanco { get; set; }
        public string Agencia { get; set; }
        public string DigitoConta { get; set; }
    }
}