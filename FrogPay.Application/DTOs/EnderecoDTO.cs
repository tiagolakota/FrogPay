namespace FrogPay.Application.Models
{
    public class EnderecoDTO
    {
        public Guid IdPessoa { get; set; }
        public string UFEstado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
    }
}