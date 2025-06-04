using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OptionResponse : BaseEntity
    {
        public int Id { get; set; }
        public string? OptionText { get; set; }

        // Navigation
        public ICollection<OptionQuestion>? OptionQuestions { get; set; }
        public ICollection<CategoryOption>? CategoryOptions { get; set; }
    }
}