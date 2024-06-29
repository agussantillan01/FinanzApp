using Business.DTOs.Account;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace FinanzApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;
        public AuthController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }
        [HttpPost("Authenticate")]
        public async Task<IAccountService> Authenticate(AuthenticationRequest request)
        {
            return (IAccountService)Ok(await _accountService.AuthenticateAsync(request, GenerateIPAdress()));
        }


        #region FuncionesPrivadas 
        private string GenerateIPAdress() {
            if (Request.Headers.ContainsKey("X-Forwarded-For")) return Request.Headers["X-Forwarded-For"];
            else return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

        }
        #endregion

    }
}
