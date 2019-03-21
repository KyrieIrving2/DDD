using System;
using System.Collections.Generic;
using System.Text;

namespace General.Framework.Security.Admin
{
    public interface IAuthAdminService
    {
        /// <summary>
        /// 当前登录用户
        /// </summary>
        Entities.SysUser.SysUser GetCurrentUser();

        /// <summary>
        /// 保存登录状态
        /// </summary>
        /// <param name="token"></param>
        /// <param name="acount"></param>
        void SignIn(string token, string acount);

    }
}
