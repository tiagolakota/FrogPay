using FrogPay.Application.Models;
using FrogPay.Domain.Entities;

namespace FrogPay.Application.Interfaces
{
    public interface ILojaService
    {
        Task<IEnumerable<Loja>> ObterTodosAsync();
        Task<Loja> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(Loja loja);
        Task AtualizarAsync(Guid id, Loja loja);
        Task RemoverAsync(Guid id);
    }
}