using System.ComponentModel.DataAnnotations;

namespace OficinaAPI.Models
{
    public class OrdemServico
    {
        public int Id { get; set; }
        public string? NumeroOS { get; set; }
        public string NomeCliente { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string TipoServico { get; set; }
        public string MarcaCarro { get; set; }
        public string ModeloCarro { get; set; }
        public string Status { get; set; } = "Pendente"; // "Pendente", "Em Andamento", "Finalizado"
        public string? MecanicoResponsavel { get; set; }
        public string? ComentarioFinal { get; set; }
    }
}