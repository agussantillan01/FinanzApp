using Business.DTOs.Account;
using Domain.Wrappers;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        //Task<Response<AuthenticationResponse>> AuthenticateEmpresaAsync(int empresaId, int userId, string userName, string ipAddress);
        Task<Response<string>> RegisterAsync(RegisterRequest request, string origin);
        Task<Response<string>> ConfirmEmailAsync(string userId, string code);
        Task ForgotPassword(ForgotPasswordRequest model, string origin);
        Task<Response<string>> ResetPassword(ResetPasswordRequest model);
        Task<Response<AuthenticationResponse>> RefreshToken(int userId, string refreshToken, string userName, string idEmpresa, string ip);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        Task<UsuarioLogin> GetUsuario(AuthenticationRequest request);
        Task<UsuarioLogin> GetUsuarioXId(int id);
    }
}
