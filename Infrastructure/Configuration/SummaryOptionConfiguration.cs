using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class SummaryOptionConfiguration : IEntityTypeConfiguration<SummaryOption>
    {
        public void Configure(EntityTypeBuilder<SummaryOption> builder)
        {
            builder.ToTable("summary_options");
            builder.HasKey(so => so.Id);

            builder.Property(so => so.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()  // serial4 (auto-increment by PostgreSQL)
                .UseIdentityColumn()
                .IsRequired();

            builder.Property(so => so.SurveyId)
                .HasColumnName("id_survey");

            builder.Property(so => so.CodeNumber)
                .HasColumnName("code_number")
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(so => so.IdQuestion)
                .HasColumnName("id_question");

            builder.Property(so => so.ValorT)
                .HasColumnName("valor_t")
                .HasColumnType("text")
                .IsRequired(false);

            builder.HasOne(so => so.Survey)
                   .WithMany(s => s.SummaryOptions)
                   .HasForeignKey(so => so.SurveyId);

            builder.HasOne(so => so.Question)
                   .WithMany(q => q.SummaryOptions)
                   .HasForeignKey(so => so.IdQuestion);
        }
    }
}