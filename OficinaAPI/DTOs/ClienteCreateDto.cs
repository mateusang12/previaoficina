using System.ComponentModel.DataAnnotations;

namespace OficinaAPI.DTOs
{
    public class ClienteCreateDto
    {
        [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode passar de 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(14, ErrorMessage = "O CPF deve ter no máximo 14 caracteres.")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [StringLength(20, ErrorMessage = "O telefone deve ter no máximo 20 caracteres.")]
        public string Telefone { get; set; }
    }
}
