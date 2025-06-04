using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CategoryCatalog : BaseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // Navigation
        public ICollection<CategoryOption>? CategoryOptions { get; set; }
        public ICollection<OptionQuestion>? OptionQuestions { get; set; }
    }
}