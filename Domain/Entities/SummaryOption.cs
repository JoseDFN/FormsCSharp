using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SummaryOption
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public string? CodeNumber { get; set; }
        public int IdQuestion { get; set; }
        public string? ValorT { get; set; }

        // Navigation
        public Survey? Survey { get; set; }
        public Question? Question { get; set; }
    }
}