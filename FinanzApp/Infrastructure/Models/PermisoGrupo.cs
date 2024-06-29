using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class PermisoGrupo : IdentityRoleClaim<int>
    {
        public int IdEmpresa { get; set; }
        public int IdPermiso { get; set; }
    }
}
