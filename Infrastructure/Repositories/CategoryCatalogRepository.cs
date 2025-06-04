using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class CategoryCatalogRepository : IGenericRepository<CategoryCatalog>, ICategoryCatalogRepository
    {
        public void Add(CategoryCatalog entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<CategoryCatalog> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CategoryCatalog> Find(Expression<Func<CategoryCatalog, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CategoryCatalog>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CategoryCatalog> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(CategoryCatalog entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<CategoryCatalog> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(CategoryCatalog entity)
        {
            throw new NotImplementedException();
        }
    }
}