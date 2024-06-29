using Business.Interfaces;
using FluentValidation.Results;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.IdentityService
{
    public class RoleService : IRoleService
    {
        private readonly Microsoft.AspNetCore.Identity.RoleManager<Grupo> _roleManager;  
        private readonly Microsoft.AspNetCore.Identity.UserManager<UsuarioLogin> _UserManager;  

        public RoleService(Microsoft.AspNetCore.Identity.RoleManager<Grupo> roleManager,
         Microsoft.AspNetCore.Identity.UserManager<UsuarioLogin> userManager)
        {
            _roleManager = roleManager;
            _UserManager = userManager;
        }
        public async Task CreateRole(string role)
        {
            var applicationRole = new Grupo(role); 
            await _roleManager.CreateAsync(applicationRole);
        }

        public Task DeleteRole(string role)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IRoleService>> ObtenerRoles()
        {
            throw new NotImplementedException();
        }

        public Task ValidarRole(int idRole, List<ValidationFailure> validationFailures)
        {
            throw new NotImplementedException();
        }
    }
}
