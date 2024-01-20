using FrogPay.Application.Interfaces.Repositories;
using FrogPay.Domain.Entities;
using FrogPay.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FrogPay.Infrastructure.Data.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly FrogPayContext _context;

        public EnderecoRepository(FrogPayContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Endereco> ObterPorIdPessoaAsync(Guid idPessoa)
        {
            return await _context.Enderecos.FirstOrDefaultAsync(e => e.IdPessoa == idPessoa);
        }

        public async Task AdicionarAsync(Endereco endereco)
        {
            await _context.Enderecos.AddAsync(endereco);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Guid idPessoa, Endereco endereco)
        {
            var enderecoExistente = await _context.Enderecos.FindAsync(idPessoa);

            if (enderecoExistente != null)
            {
                enderecoExistente.UFEstado = endereco.UFEstado;
                enderecoExistente.Cidade = endereco.Cidade;
                enderecoExistente.Bairro = endereco.Bairro;
                enderecoExistente.Logradouro = endereco.Logradouro;
                enderecoExistente.Numero = endereco.Numero;
                enderecoExistente.Complemento = endereco.Complemento;

                _context.Enderecos.Update(enderecoExistente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoverAsync(Guid idPessoa)
        {
            var endereco = await _context.Enderecos.FindAsync(idPessoa);

            if (endereco != null)
            {
                _context.Enderecos.Remove(endereco);
                await _context.SaveChangesAsync();
            }
        }
    }
}