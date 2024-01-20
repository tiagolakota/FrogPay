using FrogPay.Application.Models;
using FrogPay.Domain.Entities;

namespace FrogPay.Application.Interfaces.Services
{
    public interface IPessoaService
    {
        Task<IEnumerable<Pessoa>> ObterTodosAsync();
        Task<Pessoa> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(Pessoa pessoa);
        Task AtualizarAsync(Guid id, Pessoa pessoa);
        Task RemoverAsync(Guid id);
    }
}