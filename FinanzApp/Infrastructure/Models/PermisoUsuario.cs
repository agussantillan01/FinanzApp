using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class PermisoUsuario : IdentityUserClaim<int>
    {
        public string IdEmpresa { get; set; }
        public int IdPermiso { get; set; }
    }
}
