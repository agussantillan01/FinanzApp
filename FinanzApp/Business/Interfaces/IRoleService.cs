using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IRoleService
    {
        Task CreateRole(string role);
        Task DeleteRole(string role);
        Task<IEnumerable<IRoleService>> ObtenerRoles();
        Task ValidarRole(int idRole, List<ValidationFailure> validationFailures);

    }
}
