using Domain.EntitiesIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Principal;
using System.Security;
namespace Infrastructure.Models
{
    public class UsuarioLogin : IdentityUser<int>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool EsUserSistema { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }
        public bool OwnsToken(string token)
        {
            return this.RefreshTokens?.Find(x=> x.Token == token) != null;
        }

    }
}
