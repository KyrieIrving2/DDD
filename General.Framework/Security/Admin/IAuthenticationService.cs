using System;
using System.Collections.Generic;
using System.Text;

namespace General.Framework.Security.Admin
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// 当前登录用户
        /// </summary>
        Entities.User.User GetCurrentUser { get; }
    }
}
