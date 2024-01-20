using FrogPay.Application.Interfaces.Repositories;
using FrogPay.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FrogPay.Infrastructure.Data.Repositories
{
    public class LojaRepository : ILojaRepository
    {
        private readonly FrogPayContext _context;

        public LojaRepository(FrogPayContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Loja>> ObterTodosAsync()
        {
            return await _context.Lojas.ToListAsync();
        }

        public async Task<Loja> ObterPorIdAsync(Guid id)
        {
            return await _context.Lojas.FindAsync(id);
        }

        public async Task AdicionarAsync(Loja loja)
        {
            await _context.Lojas.AddAsync(loja);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Guid id, Loja loja)
        {
            var lojaExistente = await _context.Lojas.FindAsync(id);

            if (lojaExistente != null)
            {
                lojaExistente.NomeFantasia = loja.NomeFantasia;
                lojaExistente.RazaoSocial = loja.RazaoSocial;
                lojaExistente.CNPJ = loja.CNPJ;
                lojaExistente.DataAbertura = loja.DataAbertura;

                _context.Lojas.Update(lojaExistente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoverAsync(Guid id)
        {
            var loja = await _context.Lojas.FindAsync(id);

            if (loja != null)
            {
                _context.Lojas.Remove(loja);
                await _context.SaveChangesAsync();
            }
        }
    }
}