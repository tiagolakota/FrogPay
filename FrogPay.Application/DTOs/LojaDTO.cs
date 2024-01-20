namespace FrogPay.Application.Models
{
    public class LojaDTO
    {
        public Guid Id { get; set; }
        public Guid IdPessoa { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }
        public DateTime DataAbertura { get; set; }
    }
}