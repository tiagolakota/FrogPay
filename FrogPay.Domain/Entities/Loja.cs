namespace FrogPay.Domain.Entities
{
    public class Loja
    {
        public Guid Id { get; set; }
        public required string NomeFantasia { get; set; }
        public required string RazaoSocial { get; set; }
        public required string CNPJ { get; set; }
        public DateTime DataAbertura { get; set; }
        public Guid IdPessoa { get; set; }
        public required Pessoa Pessoa { get; set; }
    }
}
