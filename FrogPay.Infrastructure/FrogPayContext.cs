using FrogPay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FrogPay.Infrastructure
{
    public class FrogPayContext : DbContext
    {
        public IConfiguration Configuration { get; }

        public FrogPayContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseNpgsql(Configuration.GetConnectionString("FrogPayConnection"));
        }

        public FrogPayContext(DbContextOptions<FrogPayContext> options) : base(options)
        {
        }
 
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Loja> Lojas { get; set; }
        public DbSet<DadosBancarios> DadosBancarios { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração do relacionamento um-para-um entre Pessoa e DadosBancarios
            modelBuilder.Entity<Pessoa>()
                .HasOne(p => p.DadosBancarios)
                .WithOne(db => db.Pessoa)
                .HasForeignKey<DadosBancarios>(db => db.IdPessoa);

            // Configuração do relacionamento um-para-um entre Loja e Pessoa
            modelBuilder.Entity<Loja>()
                .HasOne(l => l.Pessoa)
                .WithOne(p => p.Loja)
                .HasForeignKey<Loja>(l => l.IdPessoa);

            // Configuração do relacionamento um-para-um entre Endereco e Pessoa
            modelBuilder.Entity<Endereco>()
                .HasOne(e => e.Pessoa)
                .WithOne(p => p.Endereco)
                .HasForeignKey<Endereco>(e => e.IdPessoa);

            // Configuração da chave primária para a tabela tb_dados_bancarios
            modelBuilder.Entity<DadosBancarios>()
                .HasKey(db => db.IdPessoa);

            // Aplicar outras configurações de modelo se necessário
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FrogPayContext).Assembly);
        }
    }
}