using Microsoft.EntityFrameworkCore;
using SiteDeCadastro.Models;

namespace SiteDeCadastro.Data
{
    public class BancoContext : DbContext
    {

        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=DB_SistemaContatos;" +
                "User Id=sa;Password=1234@;" +
                "Trusted_Connection=true;" +
                "TrustServerCertificate=true;");
        }
        public DbSet<ContatoModel> Contatos { get; set; }
        public DbSet<UserModel> Usuarios  { get; set; }
    }
}
 