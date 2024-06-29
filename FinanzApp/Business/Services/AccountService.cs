using Business.DTOs.Account;
using Business.Exceptions;
using Business.Interfaces;
using Domain.Settings;
using Domain.Wrappers;
using Infrastructure.CustomIdentity.Interface;
using Infrastructure.DBConnection;
using Infrastructure.Helpers;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class AccountService : IAccountService
    {
        #region atributos 
        private readonly IActiveDirectoryManager _activeDirectoryManager;
        private readonly JWTSettings _jwtSetting;
        private readonly Microsoft.AspNetCore.Identity.UserManager<UsuarioLogin> _userManager;
        private readonly ApplicationDbContext _ApplicationDbContext;
        private readonly SignInManager<UsuarioLogin> _SingInManager;
        #endregion
    
        #region Constructor
        public AccountService(ApplicationDbContext ApplicationDbContext,
                                IActiveDirectoryManager activeDirectoryManager,
                                Microsoft.AspNetCore.Identity.UserManager<UsuarioLogin> userManager,
                                JWTSettings jwtSetting,
                                SignInManager<UsuarioLogin> SingInManager)
        {
            _activeDirectoryManager = activeDirectoryManager;
            _ApplicationDbContext = ApplicationDbContext;
            _userManager = userManager;
            _jwtSetting = jwtSetting;
            _SingInManager = SingInManager;
        }
        #endregion
        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            var user = await GetUsuario(request);
            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
            //var refreshToken = GenerateRefreshToken(ipAddress, user.Id);
            //await UpdateRefreshToken(refreshToken);

            AuthenticationResponse response = new AuthenticationResponse();
            response.Id = user.Id.ToString();
            response.Email = user.Email;
            response.IsVerified = true;// user.EmailConfirmed;
            //response.RefreshToken = refreshToken.Token;
            //response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            //response.ExpireDate = jwtSecurityToken.ValidTo.ToLocalTime();

            return new Response<AuthenticationResponse>(response, $"{user.Email.Trim()} autenticado");
        }
        private async Task<JwtSecurityToken> GenerateJWToken(UsuarioLogin user)//, string idEmpresa)
        {
            string ipAddress = IpHelper.GetIpAddress();

            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.Sub, user.UserName.Trim()),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 new Claim(JwtRegisteredClaimNames.Email, user.Email.Trim()),
                 new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName.Trim()),
                 new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", user.UserName.Trim()),
                 new Claim("uid", user.Id.ToString()),
                 new Claim("userSistema", user.EsUserSistema.ToString()),
                 new Claim("ip", ipAddress),
                 //new Claim("idEmpresa", idEmpresa.Trim())
            };

            //var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key.PadRight(256)));
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSetting.Issuer,
                audience: _jwtSetting.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSetting.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
        public Task<Response<string>> ConfirmEmailAsync(string userId, string code)
        {
            throw new NotImplementedException();
        }

        public Task ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            throw new NotImplementedException();
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<UsuarioLogin> GetUsuario(AuthenticationRequest request)
        {
            UsuarioLogin us = new UsuarioLogin();

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new ApiException("Email y/o contraseña incorrecta");
            }
            //await validarLogin(request, user);
            return us;
        }

        public Task<UsuarioLogin> GetUsuarioXId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<AuthenticationResponse>> RefreshToken(int userId, string refreshToken, string userName, string idEmpresa, string ip)
        {
            throw new NotImplementedException();
        }

        public Task<Response<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            throw new NotImplementedException();
        }

        public Task<Response<string>> ResetPassword(ResetPasswordRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
