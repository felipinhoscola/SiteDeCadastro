using SiteDeCadastro.Enums;
using System;

namespace SiteDeCadastro.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login {  get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public PerfilEnum Perfil { get; set; }
        public DateTime DateCad { get; set; }
        public DateTime? LastAtt { get; set; }
    }
}
