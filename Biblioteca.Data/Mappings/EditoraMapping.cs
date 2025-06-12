using Biblioteca.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Data.Mappings
{
    public class EditoraMapping : IEntityTypeConfiguration<Editora>
    {
        public void Configure(EntityTypeBuilder<Editora> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Documento)
                .IsRequired()
                .HasColumnType("varchar(14)");

            // 1 : 1 => Editora : Endereco
            builder.HasOne(f => f.Endereco)
                .WithOne(e => e.Editora);

            // 1 : N => Editora : Livros
            builder.HasMany(f => f.Livros)
                .WithOne(p => p.Editora)
                .HasForeignKey(p => p.EditoraId);

            builder.ToTable("Editoras");
        }
    }
}
