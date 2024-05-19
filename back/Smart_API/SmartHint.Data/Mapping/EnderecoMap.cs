using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartHint.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHint.Data.Mapping
{
    public class EnderecoMap : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("Endereco");

            builder.Property(a => a.Rua)
                .HasColumnType("varchar(100)");

            builder.Property(a => a.Numero)
                .HasColumnType("varchar(5)");

            builder.Property(a => a.Complemento)
               .HasColumnType("varchar(40)");

            builder.Property(a => a.Bairro)
                .HasColumnType("varchar(30)");

            builder.Property(a => a.Cidade)
                .HasColumnType("varchar(40)");

            builder.Property(a => a.UF)
                .HasColumnType("varchar(2)");

            builder.Property(a => a.UF)
               .HasColumnType("varchar(8)");
        }
    }
}
