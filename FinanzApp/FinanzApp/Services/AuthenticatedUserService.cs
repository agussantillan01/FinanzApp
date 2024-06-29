using Domain.Interface;
using System.Security.Claims;

namespace FinanzApp.Services
{
    public class AuthenticatedUserService : IAunthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
            //IdEmpresa = httpContextAccessor.HttpContext?.User?.FindFirstValue("idEmpresa");
            EsUserSistema = httpContextAccessor.HttpContext?.User?.FindFirstValue("userSistema");
            Nombre = httpContextAccessor.HttpContext?.User?.Identity.Name;
        }

        public string UserId { get; }
        //public string IdEmpresa { get; }
        public string Nombre { get; }
        public string EsUserSistema { get; }
        string IAunthenticatedUserService.UserId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string IAunthenticatedUserService.Nombre { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string IAunthenticatedUserService.EsUserSistema { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
