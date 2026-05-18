using System.ComponentModel.DataAnnotations;

namespace OficinaAPI.DTOs
{
    public class VeiculoCreateDto
    {
        [Required]
        public string Marca { get; set; }

        [Required]
        public string Modelo { get; set; }

        [Required]
        public int Ano { get; set; }

        [Required]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "A placa deve ter 7 caracteres.")]
        public string Placa { get; set; }

        [Required]
        public int ClienteId { get; set; }
    }
}