using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Role
    {
        private int roleId;
        private string roleName;
        private int authLevel;
        private List<User> users; 
        public Role()
        {
            
        }
        public Role(string roleName, int? authlvel)
        {
            RoleName = roleName;
            AuthLevel = authlvel ?? 0;
        }

        public int RoleId { get => roleId; set => roleId = value; }
        public string RoleName { get => roleName; set => roleName = value; }
        public int AuthLevel { get => authLevel; set => authLevel = value; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
