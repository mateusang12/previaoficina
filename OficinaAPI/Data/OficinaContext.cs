using Microsoft.EntityFrameworkCore;
using OficinaAPI.Models;

namespace OficinaAPI.Data
{
    public class OficinaContext : DbContext
    {
        public OficinaContext(DbContextOptions<OficinaContext> options) : base(options)
        {
        }

        // Tabela de usuários para autenticação
        public DbSet<Usuario> Usuarios { get; set; }

        // Tabela das Ordens de Serviço do sistema
        public DbSet<OrdemServico> OrdensServicos { get; set; }
    }
}