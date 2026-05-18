using System.ComponentModel.DataAnnotations;

namespace OficinaAPI.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string SenhaHash { get; set; }

        [Required]
        public string Regra { get; set; } = "Admin";
    }
}
