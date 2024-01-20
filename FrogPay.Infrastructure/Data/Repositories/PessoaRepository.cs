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

        public async Task AdicionarAsync(Pessoa pessoa)
        {
            await _context.Pessoas.AddAsync(pessoa);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Guid id, Pessoa pessoa)
        {
            var pessoaExistente = await _context.Pessoas.FindAsync(id);

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

        public async Task RemoverAsync(Guid id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);

            if (pessoa != null)
            {
                _context.Pessoas.Remove(pessoa);
                await _context.SaveChangesAsync();
            }
        }
    }
}
