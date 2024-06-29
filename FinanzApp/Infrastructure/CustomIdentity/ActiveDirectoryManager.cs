using Infrastructure.CustomIdentity.Interface;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CustomIdentity
{
    public class ActiveDirectoryManager : IActiveDirectoryManager
    {
        private readonly IApplicationUserStore store;
        public ActiveDirectoryManager(IApplicationUserStore store)
        {
            this.store = store;
        }

        public void Login(string Email, string passsword)
        {
            string domain = ObtenerDominioActiveDirectory();
            using (PrincipalContext pc = new (ContextType.Domain, domain))
            {
                bool isValid = pc.ValidateCredentials(Email, passsword, ContextOptions.Negotiate);
                if (!isValid)
                {
                    throw new UnauthorizedAccessException("Error login Active Directory");
                }
            }
        }

        private string ObtenerDominioActiveDirectory()
        {
            try
            {
                var dominio = store.ObtenerDominio();
                return dominio.Valor;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public bool UsaActiveDirectory(string Email)
        {
            if (ObtenerUsaActiveDirectory())
            {
                if (ExisteDominio())
                    return !AplicaExcepcionActiveDirectory(Email);
            }

            return false;
        }

        private bool AplicaExcepcionActiveDirectory(string email)
        {
            return store.ObtenerExcepcionActiveDirectory(email) != null;
        }

        private bool ExisteDominio()
        {
            try
            {
                var dominio = store.ObtenerDominio();
                return !string.IsNullOrEmpty(dominio.Valor);
            }
            catch (Exception)
            {

                return false;
            }
        }

        private bool ObtenerUsaActiveDirectory()
        {
            try
            {
                return store.UsaActiveDirectory();
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
