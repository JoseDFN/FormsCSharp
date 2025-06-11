using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Rol : BaseEntity
    {
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Member> Members { get; set; } = new HashSet<Member>();
        public ICollection<MemberRols> MemberRols { get; set; } = new HashSet<MemberRols>();
    }
}