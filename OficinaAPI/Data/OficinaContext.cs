using Microsoft.EntityFrameworkCore;
using OficinaAPI.Models;

namespace OficinaAPI.Data
{
    public class OficinaContext : DbContext
    {
        public OficinaContext(DbContextOptions<OficinaContext> opt) : base(opt)
        {
        }

        // Mapeamento das tabelas que serão criadas no MySQL
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<OrdemServico> OrdensServico { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração crucial para o MySQL: 
            // Define o tamanho máximo do CPF antes de aplicar o índice único.
            modelBuilder.Entity<Cliente>()
                .Property(c => c.Cpf)
                .HasMaxLength(14);

            // Garante que não existirão dois clientes com o mesmo CPF
            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.Cpf)
                .IsUnique();
        }
    }
}
