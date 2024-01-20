using FrogPay.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FrogPay.Infrastructure.Data.Contexts
{
    public class FrogPayContext : DbContext
    {
        public FrogPayContext(DbContextOptions<FrogPayContext> options) : base(options)
        {
        }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Loja> Lojas { get; set; }
        public DbSet<DadosBancarios> DadosBancarios { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FrogPayContext).Assembly);
        }
    }
}
