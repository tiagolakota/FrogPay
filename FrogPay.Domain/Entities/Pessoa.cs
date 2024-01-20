namespace FrogPay.Domain.Entities
{
    public class Pessoa
    {
        public Guid Id { get; set; }
        public required string Nome { get; set; }
        public required string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public required Loja Loja { get; set; }
        public required DadosBancarios DadosBancarios { get; set; }
        public required Endereco Endereco { get; set; }
    }
}
