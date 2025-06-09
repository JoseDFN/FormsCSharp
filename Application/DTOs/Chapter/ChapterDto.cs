using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Chapter
{
    public class ChapterDto
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public string? ChapterNumber { get; set; }
        public string? ChapterTitle { get; set; }
        public string? ComponentHtml { get; set; }
        public string? ComponentReact { get; set; }
    }
}