using Infrastructure.CustomIdentity.Interface;
using Infrastructure.DBConnection;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CustomIdentity
{
    public class ApplicationUserStore : UserStore<UsuarioLogin, Grupo, ApplicationDbContext, int,
        PermisoUsuario, GrupoUsuario, Microsoft.AspNetCore.Identity.IdentityUserLogin<int>, IdentityUserToken<int>, IdentityRoleClaim<int>>, IApplicationUserStore
    {
        public ApplicationUserStore(ApplicationDbContext context)
            : base(context)
        {

        }
        public Task AddClaimsAsync(UsuarioLogin user, IEnumerable<Claim> claims, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Definicion ObtenerDominio()
        {
            throw new NotImplementedException();
        }

        public ExcepcionActiveDirectory ObtenerExcepcionActiveDirectory(string usuario)
        {
            throw new NotImplementedException();
        }

        public bool UsaActiveDirectory()
        {

            return false;
            //return Context.Definiciones.FirstOrDefault(c => c.Item == ACTIVE_DIRECTORY)?.Valor.ToLower().Trim() == "si";
        }
    }
}
