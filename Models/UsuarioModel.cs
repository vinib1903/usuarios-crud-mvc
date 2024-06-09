using System.ComponentModel.DataAnnotations;

namespace MvcUsers.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        [Required (ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        public string CPF { get; set; }
        public string? Endereco { get; set; }
        [Phone(ErrorMessage = "O telefone informado é inválido.")]
        public string? Telefone { get; set; }

    }
}
