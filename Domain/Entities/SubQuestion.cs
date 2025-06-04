using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SubQuestion
    {
        public int Id { get; set; }
        public int SubQuestionId { get; set; }
        public string? SubQuestionNumber { get; set; }
        public string? SubQuestionText { get; set; }
        public string? CommentSubQuestion { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation
        public Question? Question { get; set; }
        public ICollection<OptionQuestion>? OptionQuestions { get; set; }
    }
}