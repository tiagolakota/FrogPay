namespace FrogPay.Domain.Entities
{
    public class Endereco
    {
        public Guid IdPessoa { get; set; }
        public required Pessoa Pessoa { get; set; }
        public required string UFEstado { get; set; }
        public required string Cidade { get; set; }
        public required string Bairro { get; set; }
        public required string Logradouro { get; set; }
        public required string Numero { get; set; }
        public required string Complemento { get; set; }
    }
}
