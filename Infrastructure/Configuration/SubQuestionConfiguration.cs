using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class SubQuestionConfiguration : IEntityTypeConfiguration<SubQuestion>
    {
        public void Configure(EntityTypeBuilder<SubQuestion> builder)
        {
            builder.ToTable("sub_questions");
            builder.HasKey(sq => sq.Id);

            builder.Property(sq => sq.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(sq => sq.SubQuestionId)
                .HasColumnName("subquestion_id");

            builder.Property(sq => sq.SubQuestionNumber)
                   .HasColumnName("subquestion_number")
                   .HasMaxLength(10)
                   .IsRequired(false);
            
            builder.Property(sq => sq.SubQuestionText)
                .HasColumnName("subquestiontext")
                .HasColumnType("text")
                .IsRequired();

            builder.Property(sq => sq.CommentSubQuestion)
                .HasColumnName("comment_subquestion")
                .HasColumnType("text")
                .IsRequired(false);

            builder.Property(sq => sq.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd();

            builder.Property(sq => sq.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAddOrUpdate();
            
            builder.HasOne(sq => sq.Question)
                   .WithMany(q => q.SubQuestions)
                   .HasForeignKey(sq => sq.SubQuestionId);
        }
    }
}