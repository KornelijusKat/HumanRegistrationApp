using HumanRegistrationApp.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanRegistrationApp.Database.AccountService
{
    public interface IAccountService
    {
        User SignUpAccount(string username, string password);
        User Login(string username, string passwords);
    }
}
