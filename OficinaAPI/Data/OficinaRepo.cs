using OficinaAPI.Models;

namespace OficinaAPI.Data
{
    public class OficinaRepo : IOficinaRepo
    {
        private readonly OficinaContext _context;

        public OficinaRepo(OficinaContext context)
        {
            _context = context;
        }

        // --- Implementação dos Métodos de Clientes antigos ---
        public IEnumerable<Usuario> ObterTodosClientes()
        {
            return _context.Usuarios.ToList();
        }

        public void CriarCliente(Usuario cliente)
        {
            if (cliente == null) throw new ArgumentNullException(nameof(cliente));
            _context.Usuarios.Add(cliente);
        }

        public Usuario ObterClientePorId(int id)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Id == id);
        }

        // --- Implementação dos Métodos de Ordens de Serviço ---
        public void CriarOS(OrdemServico os)
        {
            if (os == null) throw new ArgumentNullException(nameof(os));
            _context.OrdensServicos.Add(os);
        }

        public IEnumerable<OrdemServico> ObterTodasOS()
        {
            return _context.OrdensServicos.ToList();
        }

        public OrdemServico ObterOSPorId(int id)
        {
            return _context.OrdensServicos.FirstOrDefault(o => o.Id == id);
        }

        public void AtualizarOS(OrdemServico os)
        {
            _context.OrdensServicos.Update(os);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}