using FrogPay.Domain.Entities;

namespace FrogPay.Application.Interfaces.Repositories
{
    public interface IPessoaRepository
    {
        Task<IEnumerable<Pessoa>> ObterTodosAsync();
        Task<Pessoa> ObterPorCpfAsync(string cpf);
        Task AdicionarAsync(Pessoa pessoa);
        Task AtualizarAsync(string cpf, Pessoa pessoa);
        Task RemoverAsync(string cpf);
        Task<IEnumerable<Pessoa>> ObterPorNomeAsync(string nome);
        Task<Pessoa> ObterPorIdAsync(Guid id);
    }
}