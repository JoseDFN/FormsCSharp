using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMemberRepository : IGenericRepository<Member>
    {
        Task<Member> GetByUsernameAsync(string username);
    }
}