using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Grupo :IdentityRole<int>
    {
        public Grupo() : base () { }
        public Grupo(string roleName) : base(roleName)
        {

        }
    }
}
