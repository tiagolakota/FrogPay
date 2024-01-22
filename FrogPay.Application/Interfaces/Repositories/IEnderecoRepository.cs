using FrogPay.Domain.Entities;

namespace FrogPay.Application.Interfaces.Repositories
{
    public interface IEnderecoRepository
    {
        Task<Endereco> ObterPorIdPessoaAsync(Guid idPessoa);
        Task AdicionarAsync(Endereco endereco);
        Task AtualizarAsync(Guid idPessoa, Endereco endereco);
        Task RemoverAsync(Guid idPessoa);
        Task<IEnumerable<Endereco>> ObterTodosAsync();
        Task<IEnumerable<Endereco>> ObterTodosPaginadoAsync(int page, int pageSize);
    }
}