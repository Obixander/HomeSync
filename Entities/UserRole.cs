using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UserRole
    {
        private int userRoleId;
        private int userId;
        private int roleId;

        public UserRole()
        {
            
        }
        public UserRole(int userId, int roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }

        public int UserRoleId { get => userRoleId; set => userRoleId = value; }
        public int UserId { get => userId; set => userId = value; }
        public int RoleId { get => roleId; set => roleId = value; }
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
