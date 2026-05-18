using System.ComponentModel.DataAnnotations;

namespace OficinaAPI.Models
{
    public class Servico
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        public decimal Preco { get; set; }

        public string Descricao { get; set; }
    }
}