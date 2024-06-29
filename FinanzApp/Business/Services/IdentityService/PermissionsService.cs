using Business.DTOs.Account;
using Business.DTOs.Role;
using Business.Helpers;
using Business.Interfaces;
using Domain.Interface;
using Domain.Wrappers;
using Infrastructure.CustomIdentity.Interface;
using Infrastructure.DBConnection;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.IdentityService
{
    public class PermissionsService : IPermissionsService
    {
        private readonly ApplicationDbContext _application;
        private readonly RoleManager<Grupo> _roleManager;
        //private readonly IMapper _mapper;
        private readonly IAunthenticatedUserService _authenticatedUser;
        private readonly IUserRoleService _userRoleService;
        public PermissionsService(ApplicationDbContext application, RoleManager<Grupo> roleManager, IAunthenticatedUserService authenticatedUser, IUserRoleService userRoleService)
        {
            _application = application;
            _roleManager = roleManager;
            _authenticatedUser = authenticatedUser;
            _userRoleService = userRoleService;
        }
        #region Métodos Interfaz 


        public async Task<List<string>> GetFieldPermissionsAsync(string value) //Se envia el nombre de pantalla - devuelve el permiso  por pantalla (view/edit/delete/create)=> (no se utliza en ERP)
        {
            var claimValue = $"Permissions.{value}".ToLower();

            if (bool.Parse(_authenticatedUser.EsUserSistema))
                return await GetUserSistemaFieldPermissions(claimValue);

            List<string> result = new();
            var permissions = await GetPermissionsAsync();

            foreach (var item in permissions)
            {
                item.LoadFormPermisions(result, value.ToLower());
            }

            return result;
        }

        public async Task<List<IPermission>> GetPermissionsAsync()
        {
            var userId = int.Parse(_authenticatedUser.UserId);
            var roles = await _userRoleService.GetRolesAsync(userId);
            var permissions = await GetUserPermissionsAsync();

            foreach (var item in roles)
            {
                item.AddPermission(await GetRolePermissionsAsync(item.Id));
                permissions.Add(item);
            }

            return permissions;
        }


        public async Task<List<string>> GetScreenPermissionsAsync(string value)
        {
            var claimValue = $"Permissions.{value}.".ToLower();

            if (bool.Parse(_authenticatedUser.EsUserSistema))
                return await GetUserSistemaFieldPermissions(claimValue, false);

            List<string> result = new();
            var permissions = await GetPermissionsAsync();

            foreach (var item in permissions)
            {
                item.LoadScreenPermissions(result, $"{value.ToLower()}.");
            }

            return result;
        }

        public async Task<Response<PermissionDTO>> ObtenerPermisosRoles(string roleName)
        {
            var role = await _roleManager.FindByIdAsync(roleName);
            //var permisosGrupoQuery = await (from pg in _application.PermisosGrupo
            //                                join p in _application.Permisos on pg.IdPermiso equals p.Id
            //                                where pg.RoleId == role.Id && p.ClaimValue.StartsWith("PERMISSIONS.REPORTES")
            //                                select new RoleClaimsDTO
            //                                {
            //                                    Value = p.ClaimValue,
            //                                    Type = p.ClaimType,
            //                                    Descripcion = p.Descripcion
            //                                }).ToListAsync();

            var model = new PermissionDTO
            {
                RoleId = role.Id,
                //RoleClaims = permisosGrupoQuery
            };




            return new Response<PermissionDTO>(model);

        }

        public async Task UpdateRolePermission(PermissionDTO model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId.ToString());
            var claims = await _roleManager.GetClaimsAsync(role);
            foreach (var claim in claims)
                await _roleManager.RemoveClaimAsync(role, claim);

            var selectedClaims = model.RoleClaims.Where(a => a.Selected).ToList();
            foreach (var claim in selectedClaims)
                await _roleManager.AddPermissionClaim(role, claim.Value);

            return;
        }

        public bool ValidateMenuPermissions(List<Infrastructure.CustomIdentity.Interface.IPermission> permissions, string claimValue)
        {
            if (bool.Parse(_authenticatedUser.EsUserSistema))
                return true;

            foreach (var item in permissions)
            {
                if (item.VerificarPermiso($"{Permissions.Permissions.PREFIJO_PERMISOS_MENU}{claimValue}"))
                {
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region funciones 
        private async Task<List<string>> GetUserSistemaFieldPermissions(string claimValue, bool isField = true)
        {
            var userId = int.Parse(_authenticatedUser.UserId);
            //var sistema = await _identityContext.Sistemas.SingleOrDefaultAsync(x => x.Descripcion == Sistema.ERP_CONTAB);
            var permissions = await _application.Permisos.Where(x => x.ClaimType == Permissions.Permissions.CLAIMTYPE_PERMISOS &&
                                                                      x.ClaimValue.StartsWith(claimValue))
                                                             .Select(x => x.ClaimValue.ToLower())
                                                             .Distinct().ToListAsync();


            for (int i = 0; i < permissions.Count; i++)
                permissions[i] = isField ? permissions[i].ToLower().Replace("permissions.", "").Replace("view", "edit") : permissions[i].ToLower().Replace("permissions.", "");

            return permissions.Distinct().ToList();
        }

        private async Task<List<IPermission>> GetRolePermissionsAsync(int roleId)
        {
            List<IPermission> resultado = new();
            //var sistema = await _application.Sistemas.SingleOrDefaultAsync(x => x.Descripcion == Sistema.ERP_CONTAB);
            // .ToListAsync();

            var permissions = await (from P in _application.Permisos
                                     join PG in _application.PermisosGrupo on P.Id equals PG.IdPermiso
                                     where
                                           P.ClaimType == Permissions.Permissions.CLAIMTYPE_PERMISOS &&
                                           PG.RoleId == roleId// &&
                                                              // PG.IdEmpresa == _authenticatedUser.IdEmpresa
                                     select new Permission()
                                     {
                                         Id = PG.Id,
                                         ClaimType = P.ClaimType,
                                         ClaimValue = P.ClaimValue
                                     }).ToListAsync();

            resultado.AddRange(permissions);
            return resultado;
        }

        private async Task<List<IPermission>> GetUserPermissionsAsync()
        {
            List<IPermission> resultado = new();
            var userId = int.Parse(_authenticatedUser.UserId);
            //var sistema = await _application.Sistemas.SingleOrDefaultAsync(x => x.Descripcion == Sistema.ERP_CONTAB);
            //.ToListAsync();
            var permissions = await (from P in _application.Permisos
                                     join PU in _application.PermisosUsuario on P.Id equals PU.IdPermiso
                                     where P.ClaimType == Permissions.Permissions.CLAIMTYPE_PERMISOS &&
                                           PU.UserId == userId
                                     select new Permission()
                                     {
                                         Id = PU.Id,
                                         ClaimType = P.ClaimType,
                                         ClaimValue = P.ClaimValue
                                     }).ToListAsync();

            resultado.AddRange(permissions);
            return resultado;
        }

        public async Task<List<RoleView>> GetUserPermissions()
        {
            var userId = int.Parse(_authenticatedUser.UserId);
            var roles = await _userRoleService.GetRolesAsync(userId);
            var permissions = await GetUserPermissionsAsync();
            var listRoleView = new List<RoleView>();
            foreach (var item in roles)
            {
                var roleView = new RoleView();
                roleView.ListPermissions = new List<Permission>();
                var listPermission = await (from P in _application.Permisos
                                            join PG in _application.PermisosGrupo on P.Id equals PG.IdPermiso
                                            where
                                                  P.ClaimType == Permissions.Permissions.CLAIMTYPE_PERMISOS &&
                                                  PG.RoleId == item.Id// &&
                                                                      // PG.IdEmpresa == _authenticatedUser.IdEmpresa
                                            select new Permission()
                                            {
                                                Id = PG.Id,
                                                ClaimType = P.ClaimType,
                                                ClaimValue = P.ClaimValue
                                            }).ToListAsync();

                roleView.Roles = item;
                roleView.ListPermissions.AddRange(listPermission);

                listRoleView.Add(roleView);
            }


            return listRoleView;
        }

        public async Task<List<Permiso>> GetPermissions()
        {
            var resultado = await (_application.Permisos
                            .Where(p => !_application.Permisos
                            .Any(p2 => p2.Id == p.Id && p2.Descripcion.Contains("Cliente")))
                            .Distinct())
                            .ToListAsync();

            return resultado;
        }
        #endregion
    }
}
