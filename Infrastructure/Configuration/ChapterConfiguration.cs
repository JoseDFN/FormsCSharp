using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class ChapterConfiguration : IEntityTypeConfiguration<Chapter>
    {
        public void Configure(EntityTypeBuilder<Chapter> builder)
        {
            builder.ToTable("chapters");
            builder.HasKey(ch => ch.Id);

            builder.Property(ch => ch.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(ch => ch.SurveyId)
                .HasColumnName("survey_id");

            builder.Property(ch => ch.ChapterNumber)
                .HasColumnName("chapter_number")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(ch => ch.ChapterTitle)
                .HasColumnName("chapter_title")
                .HasColumnType("text")
                .IsRequired();
            
            builder.Property(ch => ch.ComponentHtml)
                .HasColumnName("componenthtml")
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(ch => ch.ComponentReact)
                .HasColumnName("componentreact")
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(ch => ch.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd();

            builder.Property(ch => ch.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAddOrUpdate();

            builder.HasOne(ch => ch.Survey)
                .WithMany(s => s.Chapters)
                .HasForeignKey(ch => ch.SurveyId);
        }
    }
}