using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Core.Librarys;
using General.Entities.SysUser;
using General.Framework.Controllers.Admin;
using General.Framework.Security.Admin;
using General.Services.SysUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace General.Mvc.Areas.Admin.Controllers
{
    [Route("Admin/Login")]
    public class LoginController : PublicAdminController
    {
        private const string S_KEY = "S_KEY";

        private ISysUserService _sysUserService;
        private IAuthAdminService _authAdminService;

        public LoginController(ISysUserService sysUserService, IAuthAdminService authAdminService)
        {
            this._sysUserService = sysUserService;
            this._authAdminService = authAdminService;
        }

        [Route("Login", Name = "LoginAction")]
        [HttpGet]
        public IActionResult Login()
        {
            var r = EncryptorHelper.GetMD5(Guid.NewGuid().ToString());
            HttpContext.Session.SetString(S_KEY, r);
            string ramdom = HttpContext.Session.GetString(S_KEY);
            LoginModel model = new LoginModel()
            {
                R = r
            };
            return View(model);
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                AjaxResult.Messege = "用户名或密码错误！";
                AjaxResult.Status = false;
                return Json(AjaxResult);
            }
            else
            {
                string r = HttpContext.Session.GetString(S_KEY);
                var result = _sysUserService.IsValidateUser(model.Account, model.Password, r);
                if (result.status)
                {
                    _authAdminService.SignIn(result.token, result.Item4.Account);
                    AjaxResult.Messege = "登录成功！";
                    AjaxResult.Status = true;
                    return Json(AjaxResult);
                }
                else
                {
                    AjaxResult.Messege = "用户名或密码错误！";
                    AjaxResult.Status = false;
                    return Json(AjaxResult);
                }
            }
        }

        [HttpGet]
        [Route("GetSaltByAccount", Name = "GetSaltByAccount")]
        public IActionResult GetSaltByAccount(string account)
        {
            var user = _sysUserService.GetUserByName(account);
            return Content(user.Salt ?? "");
        }
    }
}