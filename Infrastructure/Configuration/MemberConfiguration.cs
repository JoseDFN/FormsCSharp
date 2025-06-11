using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            // Aquí puedes configurar las propiedades de la entidad Marca
            // utilizando el objeto 'builder'.
            builder.ToTable("members");
            builder.HasKey(e => e.Id); // Asumiendo que 'Id' es la clave primaria
            builder.Property(p => p.Username)
                                 .IsRequired()
                                 .HasMaxLength(200);
            builder.HasIndex(p => p.Username).IsUnique();

            builder.Property(p => p.Email)
                    .IsRequired()
                    .HasMaxLength(200);
            builder.HasIndex(p => p.Email).IsUnique();
            builder
            .HasMany(p => p.Roles)
            .WithMany(p => p.Members)
            .UsingEntity<MemberRols>(
                j => j
                    .HasOne(pt => pt.Rol)
                    .WithMany(t => t.MemberRols)
                    .HasForeignKey(pt => pt.RolId),
                j => j
                    .HasOne(pt => pt.Member)
                    .WithMany(p => p.MemberRols)
                    .HasForeignKey(pt => pt.MemberId),
                j =>
                {
                    j.HasKey(t => new { t.MemberId, t.RolId });
                });
        }
    }
}