namespace FrogPay.Domain.Entities
{
    public class DadosBancarios
    {
        public Guid IdPessoa { get; set; }
        public required Pessoa Pessoa { get; set; }
        public required string CodigoBanco { get; set; }
        public required string Agencia { get; set; }
        public required string DigitoConta { get; set; }
    }
}
