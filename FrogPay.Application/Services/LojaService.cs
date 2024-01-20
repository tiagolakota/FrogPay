using FrogPay.Application.Interfaces;
using FrogPay.Application.Interfaces.Repositories;
using FrogPay.Domain.Entities;

namespace FrogPay.Application.Services
{
    public class LojaService : ILojaService
    {
        private readonly ILojaRepository _lojaRepository;

        public LojaService(ILojaRepository lojaRepository)
        {
            _lojaRepository = lojaRepository;
        }

        public async Task<IEnumerable<Loja>> ObterTodosAsync()
        {
            return await _lojaRepository.ObterTodosAsync();
        }

        public async Task<Loja> ObterPorIdAsync(Guid id)
        {
            return await _lojaRepository.ObterPorIdAsync(id);
        }

        public async Task AdicionarAsync(Loja loja)
        {
            await _lojaRepository.AdicionarAsync(loja);
        }

        public async Task AtualizarAsync(Guid id, Loja loja)
        {
            await _lojaRepository.AtualizarAsync(id, loja);
        }

        public async Task RemoverAsync(Guid id)
        {
            await _lojaRepository.RemoverAsync(id);
        }
    }
}
