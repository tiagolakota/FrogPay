using FrogPay.Application.Models;
using FrogPay.Domain.Entities;

namespace FrogPay.Application.Interfaces.Services
{
    public interface IEnderecoService
    {
        Task<Endereco> ObterPorIdPessoaAsync(Guid idPessoa);
        Task AdicionarAsync(Endereco endereco);
        Task AtualizarAsync(Guid idPessoa, Endereco endereco);
        Task RemoverAsync(Guid idPessoa);
    }
}