namespace FrogPay.Domain.Entities
{
    public class Pessoa
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public Loja Loja { get; set; }
        public DadosBancarios DadosBancarios { get; set; }
        public Endereco Endereco { get; set; }
    }
}
