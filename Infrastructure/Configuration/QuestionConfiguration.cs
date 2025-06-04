using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("questions");
            builder.HasKey(q => q.Id);

            builder.Property(q => q.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(q => q.ChapterId)
                .HasColumnName("chapter_id");

            builder.Property(q => q.QuestionNumber)
                   .HasColumnName("question_number")
                   .HasMaxLength(10)
                   .IsRequired(false);
            
            builder.Property(q => q.ResponseType)
                   .HasColumnName("response_type")
                   .HasMaxLength(10)
                   .IsRequired(false);

            builder.Property(q => q.QuestionText)
                .HasColumnName("question_text")
                .HasColumnType("text")
                .IsRequired();

            builder.Property(q => q.CommentQuestion)
                .HasColumnName("comment_question")
                .HasColumnType("text")
                .IsRequired(false);

            builder.Property(q => q.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd();

            builder.Property(q => q.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAddOrUpdate();
            
            builder.HasOne(q => q.Chapter)
                   .WithMany(c => c.Questions)
                   .HasForeignKey(q => q.ChapterId);

            builder.HasMany(q => q.SubQuestions)
                   .WithOne(sq => sq.Question)
                   .HasForeignKey(sq => sq.SubQuestionId);

            builder.HasMany(q => q.OptionQuestions)
                   .WithOne(o => o.Question)
                   .HasForeignKey(o => o.QuestionId);

            builder.HasMany(q => q.SummaryOptions)
                   .WithOne(so => so.Question)
                   .HasForeignKey(so => so.IdQuestion);
        }
    }
}