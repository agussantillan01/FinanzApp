using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CustomIdentity.Interface
{
    public interface IActiveDirectoryManager
    {
        bool UsaActiveDirectory(string Email);
        void Login (string Email, string passsword);  
    }
}
