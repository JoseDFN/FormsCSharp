using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CategoryCatalog
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation
        public ICollection<CategoryOption>? CategoryOptions { get; set; }
        public ICollection<OptionQuestion>? OptionQuestions { get; set; }
    }
}