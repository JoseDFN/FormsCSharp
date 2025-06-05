using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class CategoryOptionRepository : GenericRepository<CategoryOption>, ICategoryOptionRepository
    {
        private readonly FormsContext _context;

        public CategoryOptionRepository(FormsContext context) : base(context)
        {
            _context = context;
            
        }
    }
}