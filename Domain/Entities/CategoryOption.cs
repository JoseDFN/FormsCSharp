using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CategoryOption
    {
        public int Id { get; set; }
        public int CatalogOptionsId { get; set; }
        public int CategoriesOptionsId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation
        public CategoryCatalog? CategoryCatalog { get; set; }
        public OptionResponse? CatalogOptions { get; set; }
    }
}