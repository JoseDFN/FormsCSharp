using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class CategoryOptionConfiguration : IEntityTypeConfiguration<CategoryOption>
    {
        public void Configure(EntityTypeBuilder<CategoryOption> builder)
        {
            builder.ToTable("category_options");
            builder.HasKey(co => co.Id);

            builder.Property(co => co.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(co => co.CatalogOptionsId)
                .HasColumnName("catalogoptions_id");

            builder.Property(co => co.CategoriesOptionsId)
                .HasColumnName("categoriesoptions_id");

            builder.Property(co => co.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd();

            builder.Property(co => co.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAddOrUpdate();

            // Relaciones dependientes
            builder.HasOne(co => co.CatalogOptions)
                   .WithMany(or => or.CategoryOptions)
                   .HasForeignKey(co => co.CatalogOptionsId);

            builder.HasOne(co => co.CategoryCatalog)
                   .WithMany(cc => cc.CategoryOptions)
                   .HasForeignKey(co => co.CategoriesOptionsId);
        }
    }
}