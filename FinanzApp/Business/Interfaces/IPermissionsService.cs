using Business.DTOs.Account;
using Domain.Wrappers;
using Infrastructure.CustomIdentity.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IPermissionsService
    { 
        Task<Response<PermissionDTO>> ObtenerPermisosRoles(string role);
        Task UpdateRolePermission(PermissionDTO model);
        Task<List<IPermission>> GetPermissionsAsync();
        bool ValidateMenuPermissions(List<IPermission> permissions, string claimValue);
        Task<List<string>> GetFieldPermissionsAsync(string form);
        Task<List<string>> GetScreenPermissionsAsync(string value);
    }
}
