using Business.DTOs.Role;
using Domain.Wrappers;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IUserRoleService 
    {
        Task<Response<ManagerUserRolesDTO>> ObtenerRolesEmpleados();
        Task<List<Role>> GetRolesAsync(int userId);
        Task<List<string>> GetRoleNameAsync(int userId); 
        Task Update (ManagerUserRolesDTO model);
    }
}
