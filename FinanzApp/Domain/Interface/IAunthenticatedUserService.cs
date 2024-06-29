using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IAunthenticatedUserService
    {
         string UserId { get; set; }
        string Nombre { get; set; }
        string EsUserSistema { get; set; }
    }
}
