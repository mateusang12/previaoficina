using OficinaAPI.Models;

namespace OficinaAPI.Data
{
    public interface IOficinaRepo
    {
        // --- Métodos de Clientes (Mantidos para não quebrar outras partes do sistema) ---
        IEnumerable<Usuario> ObterTodosClientes();
        void CriarCliente(Usuario cliente);
        Usuario ObterClientePorId(int id);

        // --- Métodos Novos de Ordens de Serviço (OS) ---
        void CriarOS(OrdemServico os);
        IEnumerable<OrdemServico> ObterTodasOS();
        OrdemServico ObterOSPorId(int id);
        void AtualizarOS(OrdemServico os);

        bool SaveChanges();
    }
}