using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Account
{
    public class RoleClaimsDTO
    {
        public string Descripcion { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }
    }
}
