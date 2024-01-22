namespace FrogPay.Domain.Entities
{
    public class Loja
    {
        public Guid Id { get; set; }
        public  string NomeFantasia { get; set; }
        public  string RazaoSocial { get; set; }
        public  string CNPJ { get; set; }
        public DateTime DataAbertura { get; set; }
        public Guid IdPessoa { get; set; }
        public  Pessoa Pessoa { get; set; }
    }
}
