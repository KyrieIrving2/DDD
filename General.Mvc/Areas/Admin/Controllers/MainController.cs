using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Entities.User;
using General.Framework.Controllers.Admin;
using General.Framework.Security.Admin;
using General.Services.SysUser;
using Microsoft.AspNetCore.Mvc;

namespace General.Mvc.Areas.Admin.Controllers
{
    [Route("Admin/Main")]
    public class MainController : PublicAdminController
    {
        private ISysUserService _sysUserService;
        private IAuthAdminService _authAdminService;
        public MainController(ISysUserService sysUserService, IAuthAdminService authAdminService)
        {
            this._sysUserService = sysUserService;
            this._authAdminService = authAdminService;
        }

        [Route("index",Name ="MainIndex")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("SignOut",Name ="SignOut")]
        public IActionResult SignOut()
        {
            _authAdminService.SignOut();
            return RedirectToRoute("LoginAction");
        }
    }
}