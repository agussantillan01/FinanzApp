using Business.DTOs.Account;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Helpers
{
    public static class ClaimsHelper
    {
        public static void GetPermissions(this List<RoleClaimsDTO> allPermissions, Type policy)
        {
            FieldInfo[] fields = policy.GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (FieldInfo fi in fields)
                allPermissions.Add(new RoleClaimsDTO { Value = fi.GetValue(null).ToString(), Type = "Permissions" });
        }

        public static async Task AddPermissionClaim(this RoleManager<Grupo> roleManager, Grupo role, string permission)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            if(!allClaims.Any(a=> a.Type== "Permission" && a.Value == permission))
            {
                await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
            }
        }
    }
}
