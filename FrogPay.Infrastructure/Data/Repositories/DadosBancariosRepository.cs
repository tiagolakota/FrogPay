using FrogPay.Application.Interfaces.Repositories;
using FrogPay.Domain.Entities;
using FrogPay.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FrogPay.Infrastructure.Data.Repositories
{
    public class DadosBancariosRepository : IDadosBancariosRepository
    {
        private readonly FrogPayContext _context;

        public DadosBancariosRepository(FrogPayContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<DadosBancarios> ObterPorIdPessoaAsync(Guid idPessoa)
        {
            return await _context.DadosBancarios.FirstOrDefaultAsync(db => db.IdPessoa == idPessoa);
        }

        public async Task AdicionarAsync(DadosBancarios dadosBancarios)
        {
            await _context.DadosBancarios.AddAsync(dadosBancarios);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Guid idPessoa, DadosBancarios dadosBancarios)
        {
            var dadosExistente = await _context.DadosBancarios.FindAsync(idPessoa);

            if (dadosExistente != null)
            {
                dadosExistente.CodigoBanco = dadosBancarios.CodigoBanco;
                dadosExistente.Agencia = dadosBancarios.Agencia;
                dadosExistente.DigitoConta = dadosBancarios.DigitoConta;

                _context.DadosBancarios.Update(dadosExistente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoverAsync(Guid idPessoa)
        {
            var dadosBancarios = await _context.DadosBancarios.FindAsync(idPessoa);

            if (dadosBancarios != null)
            {
                _context.DadosBancarios.Remove(dadosBancarios);
                await _context.SaveChangesAsync();
            }
        }
    }
}