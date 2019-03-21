using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Entities.User;
using General.Framework.Controllers.Admin;
using General.Services.SysUser;
using Microsoft.AspNetCore.Mvc;

namespace General.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MainController : PublicAdminController
    {
        private ISysUserService _sysUserService;
        public MainController(ISysUserService sysUserService)
        {
            this._sysUserService = sysUserService;
        }

        public IActionResult Index()
        {
            //_sysUserService.CreateUser();
            return View();
        }
    }
}