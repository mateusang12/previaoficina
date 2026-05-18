using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OficinaAPI.Models
{
    public class OrdemServico
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DataAbertura { get; set; } = DateTime.Now;

        public DateTime? DataFechamento { get; set; }

        [Required]
        public string Status { get; set; } = "Pendente"; // Pendente, Em Andamento, Concluído

        [Required]
        public int VeiculoId { get; set; }

        [ForeignKey("VeiculoId")]
        public Veiculo Veiculo { get; set; }

        // Relacionamento Muitos-para-Muitos com Servico
        public List<Servico> Servicos { get; set; } = new List<Servico>();

        public decimal Total { get; set; }
    }
}