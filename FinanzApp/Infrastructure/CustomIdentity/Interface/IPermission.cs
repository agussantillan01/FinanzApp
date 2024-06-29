using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CustomIdentity.Interface
{
    public interface IPermission
    {
        bool VerificarPermiso(string value);
        void LoadFormPermisions(IList<string> list, string value);
        void LoadScreenPermissions(IList<string> list, string value);
        void AddPermission(List<IPermission> permisions);
    }
}
