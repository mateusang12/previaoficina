using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OficinaAPI.Models
{
    public class Veiculo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Marca { get; set; }

        [Required]
        [StringLength(100)]
        public string Modelo { get; set; }

        [Required]
        public int Ano { get; set; }

        [Required]
        [StringLength(7)]
        public string Placa { get; set; }

        // Chave Estrangeira para Cliente
        [Required]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }
    }
}
