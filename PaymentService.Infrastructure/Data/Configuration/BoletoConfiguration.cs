using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentService.Domain.Models;

namespace PaymentService.Infrastructure.Data.Configuration
{
    public class BoletoConfiguration : IEntityTypeConfiguration<Boleto>
    {
        public void Configure(EntityTypeBuilder<Boleto> builder)
        {
            builder.HasKey(b => b.IdBoleto);

            builder.Property(b => b.IdentificadorBoleto)
                .IsRequired()
                .HasColumnName("identificador_boleto")
                .HasColumnType("varchar(36)");

            builder.Property(b => b.DataVencimento)
                .IsRequired()
                .HasColumnName("data_vencimento")
                .HasColumnType("datetime");

            builder.Property(b => b.DataPagamento)
                .IsRequired()
                .HasColumnName("data_pagamento")
                .HasColumnType("datetime");

            builder.Property(b => b.Valor)
                .IsRequired()
                .HasColumnName("valor")
                .HasColumnType("decimal(18,2)");

            builder.Property(b => b.NomePagador)
                .IsRequired()
                .HasColumnName("nome_pagador")
                .HasColumnType("varchar(50)");

            builder.Property(b => b.NomeRecebedor)
                .IsRequired()
                .HasColumnName("nome_recebedor")
                .HasColumnType("varchar(50)");

            builder.Property(b => b.Status)
                .IsRequired()
                .HasColumnName("status")
                .HasConversion<int>()
                .HasColumnType("int");

            builder.Property(b => b.CodBanco)
                .IsRequired()
                .HasColumnName("cod_banco")
                .HasColumnType("int");

            builder.ToTable("Boletos");
        }
    }
}
