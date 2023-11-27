using System.ComponentModel.DataAnnotations;// Trás todas essas funcoes que estão dentro das []

namespace SiteDeCadastro.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Insira o Nome do contato!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Insira o E-mail do contato!")]
        [EmailAddress(ErrorMessage = "E-mail invalido!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Insira o Celular do contato!!")]
        [Phone(ErrorMessage = "Número de celular é invalido!")]
        public string Celular { get; set; }
        public int? UsuarioId { get; set; }
        public UserModel? Usuario { get; set; }
    }
}
