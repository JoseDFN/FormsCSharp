using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Question : BaseEntity
    {
        public int Id { get; set; }
        public int ChapterId { get; set; }
        public string? QuestionNumber { get; set; }
        public string? ResponseType { get; set; }
        public string? QuestionText { get; set; }
        public string? CommentQuestion { get; set; }

        // Navigation
        public Chapter? Chapter { get; set; }
        public ICollection<SubQuestion>? SubQuestions { get; set; }
        public ICollection<OptionQuestion>? OptionQuestions { get; set; }
        public ICollection<SummaryOption>? SummaryOptions { get; set; }
    }
}