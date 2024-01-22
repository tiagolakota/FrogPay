namespace FrogPay.Domain.Entities
{
    public class DadosBancarios
    {
        public Guid IdPessoa { get; set; }
        public  Pessoa Pessoa { get; set; }
        public  string CodigoBanco { get; set; }
        public  string Agencia { get; set; }
        public  string DigitoConta { get; set; }
    }
}
