using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.QuestionDto
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public int ChapterId { get; set; }
        public string? QuestionNumber { get; set; }
        public string? ResponseType { get; set; }
        public string? QuestionText { get; set; }
        public string? CommentQuestion { get; set; }
    }
}