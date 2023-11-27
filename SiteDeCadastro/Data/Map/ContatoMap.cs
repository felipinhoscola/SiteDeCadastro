using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiteDeCadastro.Models;

namespace SiteDeCadastro.Data.Map
{
    public class ContatoMap : IEntityTypeConfiguration<ContatoModel>
    {
        public void Configure(EntityTypeBuilder<ContatoModel> builder)
        {
            builder.HasKey(x => x.Id); //esta falando q a id é a chave primaria
            builder.HasOne(x => x.Usuario); // Tem relacão com a tabela Usuario
        }
    }
}
