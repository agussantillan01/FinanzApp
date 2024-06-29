using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CustomIdentity.Interface;
using IPermission = Infrastructure.CustomIdentity.Interface.IPermission;
namespace Infrastructure.Models
{
    public class Role : IPermission
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<IPermission> Permissions { get; set; }

        public Role() {
            Permissions = new();
        }
        public void AddPermission(List<IPermission> permisions)
        {
            Permissions.AddRange(permisions);
        }

        public void LoadFormPermisions(IList<string> list, string value)
        {
            foreach (var item in Permissions)
            {
                item.LoadFormPermisions(list, value);
            }
        }

        public void LoadScreenPermissions(IList<string> list, string value)
        {
            foreach (var item in Permissions)
            {
                item.LoadScreenPermissions(list, value);
            }
        }

        public bool VerificarPermiso(string value)
        {
            foreach (var item in Permissions)
            {
                if (item.VerificarPermiso(value))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
