using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class OptionResponseConfiguration : IEntityTypeConfiguration<OptionResponse>
    {
        public void Configure(EntityTypeBuilder<OptionResponse> builder)
        {
            builder.ToTable("options_response");
            builder.HasKey(or => or.Id);

            builder.Property(or => or.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(or => or.OptionText)
                .HasColumnName("optiontext")
                .IsRequired(false)
                .HasColumnType("text");
                
            builder.Property(or => or.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd();

            builder.Property(or => or.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}