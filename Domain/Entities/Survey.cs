using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Survey
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Instruction { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? ComponentReact { get; set; }
        public string? ComponentHtml { get; set; }

        // Navigation
        public ICollection<Chapter>? Chapters { get; set; }
        public ICollection<SummaryOption>? SummaryOptions { get; set; }
    }
}