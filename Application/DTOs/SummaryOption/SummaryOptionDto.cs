using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.SummaryOption
{
    public class SummaryOptionDto
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public string? CodeNumber { get; set; }
        public int IdQuestion { get; set; }
        public string? ValorT { get; set; }
    }
}