using System.ComponentModel.DataAnnotations;

namespace SiteDeCadastro.Models
{
    public class ResetPassModel
    {
        [Required(ErrorMessage = "Insira o Login do contato!")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Insira o Email do contato!")]
        [EmailAddress(ErrorMessage = "Email incorreto!")]
        public string Email { get; set; }
    }
}
