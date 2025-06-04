using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OptionQuestion : BaseEntity
    {
        public int Id { get; set; }
        public int OptionId { get; set; }
        public int OptionCatalogId { get; set; }
        public int QuestionId { get; set; }
        public int? SubQuestionId { get; set; }
        public string? NumberOption { get; set; }
        public string? CommentOptionRes { get; set; }

        // Navigation
        public Question? Question { get; set; }
        public SubQuestion? SubQuestion { get; set; }
        public CategoryCatalog? OptionCatalog { get; set; }
        public OptionResponse? OptionResponse { get; set; }
    }
}