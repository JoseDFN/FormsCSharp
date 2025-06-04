using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class CategoryCatalogConfiguration : IEntityTypeConfiguration<CategoryCatalog>
    {
        public void Configure(EntityTypeBuilder<CategoryCatalog> builder)
        {
            builder.ToTable("categories_catalog");
            builder.HasKey(cc => cc.Id);

            builder.Property(cc => cc.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(cc => cc.Name)
                .HasColumnName("name")
                .IsRequired(false)
                .HasMaxLength(255);

            builder.Property(cc => cc.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd();

            builder.Property(cc => cc.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}