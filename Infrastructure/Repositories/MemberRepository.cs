using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MemberRepository : GenericRepository<Member>, IMemberRepository
    {
        private readonly FormsContext _context; 
        public MemberRepository(FormsContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Member> GetByUsernameAsync(string username)
        {
            return await _context.Members
                            .Include(u => u.Rols)
                            .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
        }
    }

}