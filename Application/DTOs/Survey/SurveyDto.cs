using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Survey
{
    public class SurveyDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Instruction { get; set; }
        public string? ComponentReact { get; set; }
        public string? ComponentHtml { get; set; }
    }
}