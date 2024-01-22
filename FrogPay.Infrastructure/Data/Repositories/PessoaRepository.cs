using FrogPay.Application.Interfaces.Repositories;
using FrogPay.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FrogPay.Infrastructure.Data.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly FrogPayContext _context;

        public PessoaRepository(FrogPayContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Pessoa>> ObterTodosAsync()
        {
            return await _context.Pessoas.ToListAsync();
        }

        public async Task<Pessoa> ObterPorIdAsync(Guid id)
        {
            return await _context.Pessoas.FindAsync(id);
        }

        public async Task<Pessoa> ObterPorCpfAsync(string cpf)
        {
            return await _context.Pessoas.FirstOrDefaultAsync(p => p.CPF == cpf);
        }

        public async Task AdicionarAsync(Pessoa pessoa)
        {
            await _context.Pessoas.AddAsync(pessoa);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(string cpf, Pessoa pessoa)
        {
            var pessoaExistente = await _context.Pessoas.FirstOrDefaultAsync(p => p.CPF == cpf);

            if (pessoaExistente != null)
            {
                pessoaExistente.Nome = pessoa.Nome;
                pessoaExistente.CPF = pessoa.CPF;
                pessoaExistente.DataNascimento = pessoa.DataNascimento;
                pessoaExistente.Ativo = pessoa.Ativo;

                _context.Pessoas.Update(pessoaExistente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoverAsync(string cpf)
        {
            var pessoa = await _context.Pessoas.FirstOrDefaultAsync(p => p.CPF == cpf);

            if (pessoa != null)
            {
                _context.Pessoas.Remove(pessoa);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Pessoa>> ObterPorNomeAsync(string nome)
        {
            // Carregar todos os registros da tabela para a memória
            var pessoas = await _context.Pessoas.AsNoTracking().ToListAsync();

            // Aplicar o filtro em memória
            var result = pessoas.Where(p => p.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase)).ToList();

            return result;
        }
    }
}
