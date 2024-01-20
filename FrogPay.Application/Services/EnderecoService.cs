using FrogPay.Application.Interfaces.Services;
using FrogPay.Application.Interfaces.Repositories;
using FrogPay.Domain.Entities;

namespace FrogPay.Application.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public EnderecoService(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }

        public async Task<Endereco> ObterPorIdPessoaAsync(Guid idPessoa)
        {
            return await _enderecoRepository.ObterPorIdPessoaAsync(idPessoa);
        }

        public async Task AdicionarAsync(Endereco endereco)
        {
            await _enderecoRepository.AdicionarAsync(endereco);
        }

        public async Task AtualizarAsync(Guid idPessoa, Endereco endereco)
        {
            await _enderecoRepository.AtualizarAsync(idPessoa, endereco);
        }

        public async Task RemoverAsync(Guid idPessoa)
        {
            await _enderecoRepository.RemoverAsync(idPessoa);
        }
    }
}