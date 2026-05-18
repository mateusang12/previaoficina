using System.ComponentModel.DataAnnotations;

namespace OficinaAPI.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(14)]
        public string Cpf { get; set; }

        [Required]
        [StringLength(20)]
        public string Telefone { get; set; }

        // Relacionamento: Um cliente pode ter vários veículos
        public List<Veiculo> Veiculos { get; set; } = new List<Veiculo>();
    }
}
