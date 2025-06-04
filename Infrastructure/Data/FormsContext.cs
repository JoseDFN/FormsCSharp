using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class FormsContext : DbContext
    {
        public FormsContext(DbContextOptions<FormsContext> options) : base(options)
        {

        }

        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<SubQuestion> SubQuestions { get; set; }
        public DbSet<OptionQuestion> OptionQuestions { get; set; }
        public DbSet<OptionResponse> OptionResponses { get; set; }
        public DbSet<CategoryCatalog> CategoryCatalogs { get; set; }
        public DbSet<CategoryOption> CategoryOptions { get; set; }
        public DbSet<SummaryOption> SummaryOptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}