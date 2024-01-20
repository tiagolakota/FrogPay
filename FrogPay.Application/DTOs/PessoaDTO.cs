namespace FrogPay.Application.Models
{
    public class PessoaDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}