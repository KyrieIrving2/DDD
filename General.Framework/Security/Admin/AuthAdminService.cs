using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using General.Entities;
using General.Entities.User;
using General.Services.SysUser;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace General.Framework.Security.Admin
{
    public class AuthAdminService : IAuthAdminService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private ISysUserService _sysUserService;
        public AuthAdminService(IHttpContextAccessor httpContextAccessor, ISysUserService sysUserService)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._sysUserService = sysUserService;
        }

        /// <summary>
        /// 获取当前登录用户
        /// </summary>
        public Entities.SysUser.SysUser GetCurrentUser()
        {
            var result = _httpContextAccessor.HttpContext.AuthenticateAsync(CookieAdminAuthInfo.AuthenticationScheme).Result;
            if (result.Principal == null)
                return null;
            var token = result.Principal.FindFirst(ClaimTypes.Sid).Value;
            return _sysUserService.GetUserByToken(token ?? "");
        }

        /// <summary>
        /// 保存登录状态
        /// </summary>
        /// <param name="token"></param>
        /// <param name="acount"></param>
        public void SignIn(string token, string acount)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity("Forms");
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Sid, token));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, acount));
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            _httpContextAccessor.HttpContext.SignInAsync(CookieAdminAuthInfo.AuthenticationScheme, claimsPrincipal);
        }


    }
}
