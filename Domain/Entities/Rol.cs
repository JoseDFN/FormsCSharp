using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Rol : BaseEntity
    {
        public int Id { get; set; } 
    public string Name { get; set; } = string.Empty;
    //public ICollection<UserMember> UserMembers { get; set; } = new HashSet<UserMember>();
    public ICollection<MemberRols> MemberRols { get; set; } = new HashSet<MemberRols>();
    }
}