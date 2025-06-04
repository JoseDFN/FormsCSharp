using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class OptionQuestionConfiguration : IEntityTypeConfiguration<OptionQuestion>
    {
        public void Configure(EntityTypeBuilder<OptionQuestion> builder)
        {
            builder.ToTable("option_questions");
            builder.HasKey(oq => oq.Id);

            builder.Property(oq => oq.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(oq => oq.OptionId)
                .HasColumnName("option_id");

            builder.Property(oq => oq.OptionCatalogId)
                .HasColumnName("optioncatalog_id");

            builder.Property(oq => oq.QuestionId)
                .HasColumnName("optionquestion_id");

            builder.Property(oq => oq.SubQuestionId)
                   .HasColumnName("subquestion_id");

            builder.Property(oq => oq.NumberOption)
                   .HasColumnName("numberoption")
                   .HasMaxLength(20)
                   .IsRequired(false);

            builder.Property(oq => oq.CommentOptionRes)
                .HasColumnName("comment_optionres")
                .HasColumnType("text")
                .IsRequired(false);

            builder.Property(oq => oq.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd();

            builder.Property(oq => oq.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAddOrUpdate();
            
            builder.HasOne(oq => oq.Question)
                   .WithMany(q => q.OptionQuestions)
                   .HasForeignKey(oq => oq.QuestionId);

            builder.HasOne(oq => oq.SubQuestion)
                   .WithMany(sq => sq.OptionQuestions)
                   .HasForeignKey(oq => oq.SubQuestionId)
                   .IsRequired(false);

            builder.HasOne(oq => oq.OptionResponse)
                   .WithMany(or => or.OptionQuestions)
                   .HasForeignKey(oq => oq.OptionId);

            builder.HasOne(oq => oq.OptionCatalog)
                   .WithMany(cc => cc.OptionQuestions)
                   .HasForeignKey(oq => oq.OptionCatalogId);
        }
    }
}