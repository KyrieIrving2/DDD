using System;
using System.Collections.Generic;
using System.Text;
using General.Entities.SysUser;
using General.Entities.User;
using General.Framework.Security.Admin;

namespace General.Framework.Infrastructure
{
    public class WorkContext : IWorkContext
    {
        IAuthAdminService _authenticationService;
        public WorkContext(IAuthAdminService authenticationService)
        {
            this._authenticationService = authenticationService;
        }


        public SysUser CurrentUser
        {
            get
            {

                return _authenticationService.GetCurrentUser();
            }
        }
    }
}
