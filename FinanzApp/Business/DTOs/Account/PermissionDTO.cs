using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Account
{
    public class PermissionDTO
    {
        public int RoleId { get; set; }
        public IList<RoleClaimsDTO> RoleClaims { get; set; }
    }
}
