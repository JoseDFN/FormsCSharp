using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.SubQuestion
{
    public class SubQuestionDto
    {
        public int Id { get; set; }
        public int SubQuestionId { get; set; }
        public string? SubQuestionNumber { get; set; }
        public string? SubQuestionText { get; set; }
        public string? CommentSubQuestion { get; set; }
    }
}