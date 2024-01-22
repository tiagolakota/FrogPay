using FrogPay.Application.Interfaces.Repositories;
using FrogPay.Application.Interfaces.Services;
using FrogPay.Common;
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
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O ID da pessoa não é válido.");
            }

            return await _pessoaRepository.ObterPorIdAsync(id);
        }

        public async Task<IEnumerable<Pessoa>> ObterPorNomeAsync(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("O parâmetro 'nome' deve ser fornecido.");
            }

            return await _pessoaRepository.ObterPorNomeAsync(nome);
        }

        public async Task<Pessoa> ObterPorCpfAsync(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                throw new ArgumentException("O parâmetro 'cpf' deve ser fornecido.");
            }

            return await _pessoaRepository.ObterPorCpfAsync(cpf);
        }

        public async Task AdicionarAsync(Pessoa pessoa)
        {
            if (string.IsNullOrWhiteSpace(pessoa.Nome) ||
                string.IsNullOrWhiteSpace(pessoa.CPF) ||
                pessoa.DataNascimento == null ||
                pessoa.DataNascimento > DateTime.Now ||
                pessoa.DataNascimento < DateTime.Parse("1800-01-01") ||
                !DateTime.TryParse(pessoa.DataNascimento.ToString(), out _))
            {
                throw new ArgumentException("Dados inválidos. Certifique-se de fornecer valores válidos para todos os campos.");
            }

            if (!CpfHelper.ValidarCpf(pessoa.CPF))
            {
                throw new ArgumentException("O formato do CPF não é válido.");
            }

            var pessoaExistente = await _pessoaRepository.ObterPorCpfAsync(pessoa.CPF);

            if (pessoaExistente != null)
            {
                throw new ArgumentException("Já existe uma pessoa com o mesmo CPF.");
            }

            await _pessoaRepository.AdicionarAsync(pessoa);
        }

        public async Task AtualizarAsync(string cpf, Pessoa pessoa)
        {
            if (string.IsNullOrWhiteSpace(pessoa.Nome) ||
                string.IsNullOrWhiteSpace(pessoa.CPF) ||
                pessoa.DataNascimento == null ||
                pessoa.DataNascimento > DateTime.Now ||
                pessoa.DataNascimento < DateTime.Parse("1800-01-01") ||
                !DateTime.TryParse(pessoa.DataNascimento.ToString(), out _))
            {
                throw new ArgumentException("Dados inválidos. Certifique-se de fornecer valores válidos para todos os campos.");
            }

            var pessoaExistente = await _pessoaRepository.ObterPorCpfAsync(cpf);

            if (pessoaExistente == null)
            {
                throw new ArgumentException("Pessoa não encontrada.");
            }

            if (!CpfHelper.ValidarCpf(pessoa.CPF))
            {
                throw new ArgumentException("O formato do CPF não é válido.");
            }

            await _pessoaRepository.AtualizarAsync(cpf, pessoa);
        }

        public async Task RemoverAsync(string cpf)
        {
            var pessoaExistente = await _pessoaRepository.ObterPorCpfAsync(cpf);

            if (pessoaExistente == null)
            {
                throw new ArgumentException("Pessoa não encontrada.");
            }

            await _pessoaRepository.RemoverAsync(cpf);
        }
    }
}
