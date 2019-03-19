using System;
using System.Collections.Generic;
using System.Text;
using General.Entities.User;
using General.Framework.Security.Admin;

namespace General.Framework.Infrastructure
{
    public class WorkContext : IWorkContext
    {
        IAuthenticationService _authenticationService;
        public WorkContext(IAuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService;
        }


        public User CurrentUser
        {
            get
            {
                return _authenticationService.GetCurrentUser;
            }
        }
    }
}
