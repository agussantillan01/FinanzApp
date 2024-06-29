using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CustomIdentity
{
    public class CustomPasswordHasher : IPasswordHasher<UsuarioLogin>

    {
        public string HashPassword(UsuarioLogin user, string password)
        {
            MD5 mD5Hash = MD5.Create();
            string passHasheado;
            passHasheado = GetMd5Hash(mD5Hash, password);
            return passHasheado.ToUpper();
        }

        private static string GetMd5Hash(MD5 mD5Hash, string password)
        {
            byte[] data = mD5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i<data.Length; i++)
            {
                stringBuilder.Append(data[i].ToString("x2"));
            }
            return stringBuilder.ToString().ToUpper() ;
        }

        public Microsoft.AspNetCore.Identity.PasswordVerificationResult VerifyHashedPassword(UsuarioLogin user, string hashedPassword, string providedPassword)
        {
            throw new NotImplementedException();
        }
    }
}
