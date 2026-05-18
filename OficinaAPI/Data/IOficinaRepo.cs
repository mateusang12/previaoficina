using OficinaAPI.Models;

namespace OficinaAPI.Data
{
    public interface IOficinaRepo
    {
        // Contrato para salvar qualquer alteração no banco
        bool SaveChanges();

        // Clientes
        void CriarCliente(Cliente cliente);
        IEnumerable<Cliente> ObterTodosClientes();
        Cliente ObterClientePorId(int id);

        // Veículos
        void CriarVeiculo(Veiculo veiculo);
        IEnumerable<Veiculo> ObterVeiculosPorCliente(int clienteId);

        // Serviços
        void CriarServico(Servico servico);
        IEnumerable<Servico> ObterTodosServicos();

        // Ordens de Serviço
        void CriarOrdemServico(OrdemServico os);
        OrdemServico ObterOrdemServicoPorId(int id);
    }
}