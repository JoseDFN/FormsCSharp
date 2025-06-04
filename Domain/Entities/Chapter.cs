using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Chapter
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public string? ChapterNumber { get; set; }
        public string? ChapterTitle { get; set; }
        public string? ComponentHtml { get; set; }
        public string? ComponentReact { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation
        public Survey? Survey { get; set; }
        public ICollection<Question>? Questions { get; set; }
    }

}