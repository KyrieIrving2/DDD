using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Entities.SysUser;
using General.Framework.Controllers.Admin;
using General.Framework.Security.Admin;
using General.Services.SysUser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace General.Mvc.Areas.Admin.Controllers
{
    [Route("Admin/Login")]
    public class LoginController : PublicAdminController
    {

        private ISysUserService _sysUserService;
        private IAuthAdminService _authAdminService;

        public LoginController(ISysUserService sysUserService, IAuthAdminService authAdminService)
        {
            this._sysUserService = sysUserService;
            this._authAdminService = authAdminService;
        }

        [Route("Login",Name ="LoginAction")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginModel model)
        {
            if(!ModelState.IsValid)
            {
                AjaxResult.Messege = "用户名或密码错误！";
                AjaxResult.Status = false;
                return Json(AjaxResult);
            }
            else
            {
                var result = _sysUserService.IsValidateUser(model.Account, model.Password, "");
                if(result.status)
                {
                    _authAdminService.SignIn(result.token, result.Item4.Account);
                    AjaxResult.Messege = "登录成功！";
                    AjaxResult.Status = true;

                   var sysuser =  _authAdminService.GetCurrentUser();
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
    }
}