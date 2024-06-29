using Infrastructure.DBConnection;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Features
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly ApplicationDbContext _Application;
        public PermissionAuthorizationHandler(ApplicationDbContext application)
        {
            this._Application = application;

        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            //if (context.User == null || !context.User.Claims.Any())
            //    return;

            //if (bool.Parse(context.User?.FindFirst("userSistema").Value))
            //{
            //    context.Succeed(requirement);
            //    return;
            //}
            if (context.User == null || !context.User.Claims.Any())
                return;

            var userName = context.User?.Identity?.Name;

            var user = _Application.Usuarios.Where(x => x.Nombre == userName);
            if (user != null)
            {
                context.Succeed(requirement);
                return;

            }
            //var permissionss = context.User.Claims.Where(x => x.Type == "Permission" &&
            //                                                    x.Value == requirement.Permission &&
            //                                                    x.Issuer == "LOCAL AUTHORITY");

            //var id = int.Parse(context.User?.FindFirst("uid").Value);
            //var idEmpresa = context.User?.FindFirst("idEmpresa").Value;

            //var exists = await(from P in _identityContext.Permisos
            //                   join PU in _identityContext.PermisosUsuario on P.Id equals PU.IdPermiso
            //                   where PU.UserId == id &&
            //                         PU.IdEmpresa == idEmpresa &&
            //                         P.ClaimType == "Permission" &&
            //                         P.ClaimValue == requirement.Permission
            //                   select PU).AnyAsync();

            ////if (permissionss.Any())
            //if (exists)
            //{
            //    context.Succeed(requirement);
            //    return;
            //}

            //await ValidarPermisoEnRol(context, requirement);
            return;
        }
    
    }
}
