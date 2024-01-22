using System.ComponentModel.DataAnnotations;

namespace FrogPay.Domain.Entities
{
    public class Endereco
    {
        [Key]
        public Guid IdPessoa { get; set; }
        public  Pessoa Pessoa { get; set; }
        public  string UFEstado { get; set; }
        public  string Cidade { get; set; }
        public  string Bairro { get; set; }
        public  string Logradouro { get; set; }
        public  string Numero { get; set; }
        public  string Complemento { get; set; }
    }
}
