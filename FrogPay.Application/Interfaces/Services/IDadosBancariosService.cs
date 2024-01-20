using FrogPay.Domain.Entities;

namespace FrogPay.Application.Interfaces
{
    public interface IDadosBancariosService
    {
        Task<DadosBancarios> ObterPorIdPessoaAsync(Guid idPessoa);
        Task AdicionarAsync(DadosBancarios dadosBancarios);
        Task AtualizarAsync(Guid idPessoa, DadosBancarios dadosBancarios);
        Task RemoverAsync(Guid idPessoa);
    }
}