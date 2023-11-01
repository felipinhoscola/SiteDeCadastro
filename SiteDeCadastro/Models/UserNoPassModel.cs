using SiteDeCadastro.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace SiteDeCadastro.Models
{
    public class UserNoPassModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Insira o Nome do contato!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Insira o Login do contato!")]
        public string Login {  get; set; }
        [Required(ErrorMessage = "Insira o Email do contato!")]
        [EmailAddress(ErrorMessage ="O Email está incorreto!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Selecione um Perfil")]
        public PerfilEnum? Perfil { get; set; }
    }
}
