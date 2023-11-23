using System.ComponentModel.DataAnnotations;

namespace SiteDeCadastro.Models
{
    public class ChangePassModel
    {
        [Required(ErrorMessage = "Insira a senha atual!")]
        public string OldPass { get; set; }
        [Required(ErrorMessage = "Insira a nova senha!")]
        public string NewPass { get; set; }
    }
}
