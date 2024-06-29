using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Role
{
    public class RoleView
    {
        public Infrastructure.Models.Role? Roles { get; set; }
        public List<Permission> ListPermissions { get; set; }
    }
}
