using System;
using System.Collections.Generic;
using System.Text;

namespace General.Services.SysUser
{
   public interface ISysUserService
    {
        General.Entities.SysUser.SysUser GetUserByName(string account);
        (bool status, string messege, string token, General.Entities.SysUser.SysUser) IsValidateUser(string account, string password, string r);
        Entities.SysUser.SysUser GetUserByToken(string token);

        void CreateUser();
    }
}
