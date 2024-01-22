using FrogPay.Domain.Entities;

namespace FrogPay.Application.Interfaces.Services
{
    public interface IPessoaService
    {
        Task<IEnumerable<Pessoa>> ObterTodosAsync();
        Task<Pessoa> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Pessoa>> ObterPorNomeAsync(string nome);
        Task<Pessoa> ObterPorCpfAsync(string cpf);
        Task AdicionarAsync(Pessoa pessoa);
        Task AtualizarAsync(string cpf, Pessoa pessoa);
        Task RemoverAsync(string cpf);
    }
}