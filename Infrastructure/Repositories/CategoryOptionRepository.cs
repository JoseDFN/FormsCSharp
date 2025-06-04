using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class CategoryOptionRepository : IGenericRepository<CategoryOption>, ICategoryOptionRepository
    {
        public void Add(CategoryOption entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<CategoryOption> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CategoryOption> Find(Expression<Func<CategoryOption, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CategoryOption>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CategoryOption> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(CategoryOption entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<CategoryOption> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(CategoryOption entity)
        {
            throw new NotImplementedException();
        }
    }
}