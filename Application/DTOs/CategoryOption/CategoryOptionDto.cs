using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CategoryOptionDto
    {
        public int Id { get; set; }
        public int CatalogOptionsId { get; set; }
        public int CategoriesOptionsId { get; set; }
    }
}