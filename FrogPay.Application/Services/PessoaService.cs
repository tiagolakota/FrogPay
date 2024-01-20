using FrogPay.Application.Interfaces;
using FrogPay.Application.Interfaces.Repositories;
using FrogPay.Domain.Entities;

namespace FrogPay.Application.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;

        public PessoaService(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        public async Task<IEnumerable<Pessoa>> ObterTodosAsync()
        {
            return await _pessoaRepository.ObterTodosAsync();
        }

        public async Task<Pessoa> ObterPorIdAsync(Guid id)
        {
            return await _pessoaRepository.ObterPorIdAsync(id);
        }

        public async Task AdicionarAsync(Pessoa pessoa)
        {
            await _pessoaRepository.AdicionarAsync(pessoa);
        }

        public async Task AtualizarAsync(Guid id, Pessoa pessoa)
        {
            await _pessoaRepository.AtualizarAsync(id, pessoa);
        }

        public async Task RemoverAsync(Guid id)
        {
            await _pessoaRepository.RemoverAsync(id);
        }
    }
}