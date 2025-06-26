using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentService.Domain.Models;

namespace PaymentService.Infrastructure.Data.Configuration
{
    public class HistBoletoConfiguration : IEntityTypeConfiguration<HistBoleto>
    {
        public void Configure(EntityTypeBuilder<HistBoleto> builder)
        {
            builder.HasKey(p => p.IdBoletoHist);

            builder.Property(p => p.IdBoletoHist)
                .HasColumnName("id_boleto_hist")
                .HasColumnType("bigint");

            builder.Property(p => p.StatusBoleto)
                .HasColumnName("status_boleto")
                .HasConversion<int>()
                .HasColumnType("int");

            builder.Property(p => p.DataHoraStatus)
                .HasColumnName("data_hora_status")
                .HasColumnType("datetime");

            builder.ToTable("HistBoletos");
        }
    }
}