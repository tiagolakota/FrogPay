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

        public async Task<Loja> ObterPorIdAsync(Guid id)
        {
            return await _context.Lojas.FindAsync(id);
        }

        public async Task<IEnumerable<Loja>> ObterPorNomeFantasiaAsync(string nomeFantasia)
        {
            return await _context.Lojas
                .Where(loja => loja.NomeFantasia.ToLower().Contains(nomeFantasia.ToLower()))
                .ToListAsync();
        }

        public async Task<IEnumerable<Loja>> ObterPorRazaoSocialAsync(string razaoSocial)
        {
            return await _context.Lojas
                .Where(loja => loja.RazaoSocial.ToLower().Contains(razaoSocial.ToLower()))
                .ToListAsync();
        }

        public async Task<Loja> ObterPorCnpjAsync(string cnpj)
        {
            return await _context.Lojas
                .SingleOrDefaultAsync(loja => loja.CNPJ == cnpj);
        }

        public async Task AdicionarAsync(Loja loja)
        {
            await _context.Lojas.AddAsync(loja);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(string cnpj, Loja loja)
        {
            var lojaExistente = await _context.Lojas
                .SingleOrDefaultAsync(l => l.CNPJ == cnpj);

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

        public async Task RemoverAsync(string cnpj)
        {
            var lojaExistente = await _context.Lojas
                .SingleOrDefaultAsync(l => l.CNPJ == cnpj);

            if (lojaExistente != null)
            {
                _context.Lojas.Remove(lojaExistente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Loja>> ObterTodasAsync()
        {
            return await _context.Lojas.ToListAsync();
        }
    }
}
