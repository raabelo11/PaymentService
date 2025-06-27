using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentService.Domain.Models;

namespace PaymentService.Infrastructure.Data.Configuration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnName("nome")
                .HasColumnType("varchar(50)");

            builder.Property(p => p.Senha)
                .IsRequired()
                .HasColumnName("senha")
                .HasColumnType("varchar(250)");

            builder.Property(p => p.Email)
                .IsRequired()
                .HasColumnName("email")
                .HasColumnType("varchar(50)");

            builder.Property(p => p.DataCadastro)
                .IsRequired()
                .HasColumnName("data_cadastro")
                .HasColumnType("datetime");

            builder.Property(p => p.TipoUsuario)
                .IsRequired()
                .HasColumnName("tipo_usuario")
                .HasConversion<int>()
                .HasColumnType("int");

            builder.Property(p => p.Excluido)
                .IsRequired()
                .HasColumnName("excluido")
                .HasConversion<bool>()
                .HasColumnType("bit");

            builder.ToTable("Users");
        }
    }
}
