using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using IPermission = Infrastructure.CustomIdentity.Interface.IPermission;
namespace Infrastructure.Models
{
    public class Permission : IPermission
    {
        public int Id { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public void AddPermission(List<IPermission> permissions)
        {

        }

        public void LoadFormPermisions(IList<string> list, string value)
        {
            var newClaim = ClaimValue.Replace("Permissions.", "").ToLower();

            if (newClaim.StartsWith(value))
            {
                if (list.Contains(newClaim.Replace(".view", ".edit")))
                    return;

                var claimView = list.SingleOrDefault(x => x.ToLower().Equals($"{value}.view"));

                if (!string.IsNullOrEmpty(claimView) && newClaim.EndsWith("edit"))
                {
                    list.Remove(claimView);
                    list.Add(newClaim);
                }
                else if (!list.Contains(newClaim))
                {
                    list.Add(newClaim);
                }
            }
        }

        public void LoadScreenPermissions(IList<string> list, string value)
        {
            var newClaim = ClaimValue.Replace("Permissions.", "").ToLower();

            if (newClaim.StartsWith(value) && !list.Contains(newClaim))
            {
                list.Add(newClaim);
            }
        }

        public bool VerificarPermiso(string value)
        {
            return ClaimValue.ToLower() == value.ToLower();
        }
    }
}
