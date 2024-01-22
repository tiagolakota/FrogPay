using FrogPay.Domain.Entities;

namespace FrogPay.Application.Interfaces.Repositories
{
    public interface IDadosBancariosRepository
    {
        Task<DadosBancarios> ObterPorIdPessoaAsync(Guid idPessoa);
        Task AdicionarAsync(DadosBancarios dadosBancarios);
        Task AtualizarAsync(DadosBancarios dadosBancarios);
        Task RemoverAsync(Guid idPessoa);
    }
}
