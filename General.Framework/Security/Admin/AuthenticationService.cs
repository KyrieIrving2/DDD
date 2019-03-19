using System;
using System.Collections.Generic;
using System.Text;
using General.Entities.User;

namespace General.Framework.Security.Admin
{
    public class AuthenticationService : IAuthenticationService
    {


        public User GetCurrentUser
        {
            get { return new User() { Id = 1, Name = "sx" }; }
        }
    }
}
