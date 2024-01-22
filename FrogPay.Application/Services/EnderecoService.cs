using FrogPay.Application.Interfaces.Repositories;
using FrogPay.Application.Interfaces.Services;
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
            if (idPessoa == Guid.Empty)
            {
                throw new ArgumentException("O ID da pessoa não é válido.");
            }

            return await _enderecoRepository.ObterPorIdPessoaAsync(idPessoa);
        }

        public async Task AdicionarAsync(Endereco endereco)
        {
            if (endereco == null)
            {
                throw new ArgumentException("Os dados do endereço são inválidos.");
            }

            await _enderecoRepository.AdicionarAsync(endereco);
        }

        public async Task AtualizarAsync(Guid idPessoa, Endereco endereco)
        {
            if (endereco == null)
            {
                throw new ArgumentException("Os dados do endereço são inválidos.");
            }

            var enderecoExistente = await _enderecoRepository.ObterPorIdPessoaAsync(idPessoa);

            if (enderecoExistente == null)
            {
                throw new ArgumentException("Endereço não encontrado.");
            }

            await _enderecoRepository.AtualizarAsync(idPessoa, endereco);
        }

        public async Task RemoverAsync(Guid idPessoa)
        {
            var enderecoExistente = await _enderecoRepository.ObterPorIdPessoaAsync(idPessoa);

            if (enderecoExistente == null)
            {
                throw new ArgumentException("Endereço não encontrado.");
            }

            await _enderecoRepository.RemoverAsync(idPessoa);
        }

        public async Task<IEnumerable<Endereco>> ObterTodosAsync()
        {
            return await _enderecoRepository.ObterTodosAsync();
        }

        public async Task<IEnumerable<Endereco>> ObterTodosPaginadoAsync(int page, int pageSize)
        {
            return await _enderecoRepository.ObterTodosPaginadoAsync(page, pageSize);
        }
    }
}