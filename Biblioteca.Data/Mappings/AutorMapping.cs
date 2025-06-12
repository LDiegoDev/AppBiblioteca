using Biblioteca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Data.Mappings
{
    public class AutorMapping : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            // 1 : N => Autor : Livros

            builder.HasMany(x => x.Livros)
                .WithOne(x => x.Autor)
                .HasForeignKey(x => x.AutorId);


            builder.ToTable("Autores");
        }
    }
}
