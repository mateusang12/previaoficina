using OficinaAPI.Models;

namespace OficinaAPI.Data
{
    public class OficinaRepo : IOficinaRepo
    {
        private readonly OficinaContext _context;

        // Injeção de dependência do nosso contexto do MySQL
        public OficinaRepo(OficinaContext context)
        {
            _context = context;
        }

        // Salva qualquer alteração pendente no banco de dados
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        // ================= CLIENTES =================
        public void CriarCliente(Cliente cliente)
        {
            if (cliente == null)
            {
                throw new ArgumentNullException(nameof(cliente));
            }
            _context.Clientes.Add(cliente);
        }

        public IEnumerable<Cliente> ObterTodosClientes()
        {
            return _context.Clientes.ToList();
        }

        public Cliente ObterClientePorId(int id)
        {
            return _context.Clientes.FirstOrDefault(c => c.Id == id);
        }

        // ================= VEÍCULOS =================
        public void CriarVeiculo(Veiculo veiculo)
        {
            if (veiculo == null)
            {
                throw new ArgumentNullException(nameof(veiculo));
            }
            _context.Veiculos.Add(veiculo);
        }

        public IEnumerable<Veiculo> ObterVeiculosPorCliente(int clienteId)
        {
            return _context.Veiculos.Where(v => v.ClienteId == clienteId).ToList();
        }

        // ================= SERVIÇOS =================
        public void CriarServico(Servico servico)
        {
            if (servico == null)
            {
                throw new ArgumentNullException(nameof(servico));
            }
            _context.Servicos.Add(servico);
        }

        public IEnumerable<Servico> ObterTodosServicos()
        {
            return _context.Servicos.ToList();
        }

        // ============= ORDENS DE SERVIÇO =============
        public void CriarOrdemServico(OrdemServico os)
        {
            if (os == null)
            {
                throw new ArgumentNullException(nameof(os));
            }
            _context.OrdensServico.Add(os);
        }

        public OrdemServico ObterOrdemServicoPorId(int id)
        {
            return _context.OrdensServico.FirstOrDefault(os => os.Id == id);
        }
    }
}
