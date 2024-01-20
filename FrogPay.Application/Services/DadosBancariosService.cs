using FrogPay.Application.Interfaces;
using FrogPay.Application.Interfaces.Repositories;
using FrogPay.Domain.Entities;

namespace FrogPay.Application.Services
{
    public class DadosBancariosService : IDadosBancariosService
    {
        private readonly IDadosBancariosRepository _dadosBancariosRepository;

        public DadosBancariosService(IDadosBancariosRepository dadosBancariosRepository)
        {
            _dadosBancariosRepository = dadosBancariosRepository;
        }

        public async Task<DadosBancarios> ObterPorIdPessoaAsync(Guid idPessoa)
        {
            return await _dadosBancariosRepository.ObterPorIdPessoaAsync(idPessoa);
        }

        public async Task AdicionarAsync(DadosBancarios dadosBancarios)
        {
            await _dadosBancariosRepository.AdicionarAsync(dadosBancarios);
        }

        public async Task AtualizarAsync(Guid idPessoa, DadosBancarios dadosBancarios)
        {
            await _dadosBancariosRepository.AtualizarAsync(idPessoa, dadosBancarios);
        }

        public async Task RemoverAsync(Guid idPessoa)
        {
            await _dadosBancariosRepository.RemoverAsync(idPessoa);
        }
    }
}
