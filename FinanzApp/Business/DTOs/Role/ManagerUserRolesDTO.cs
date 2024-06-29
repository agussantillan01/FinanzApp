using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Role
{
    public class ManagerUserRolesDTO
    {
        public int UserId { get; set; }
        public IList<UserRolesDTO> UserRoles { get; set; }
    }
}
