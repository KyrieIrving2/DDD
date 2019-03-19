using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Entities.User;
using General.Framework.Controllers.Admin;
using Microsoft.AspNetCore.Mvc;

namespace General.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MainController : PublicAdminController
    {
        public IActionResult Index()
        {
            User user = workContext.CurrentUser;
            return View();
        }
    }
}