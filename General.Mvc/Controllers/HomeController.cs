using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using General.Mvc.Models;
using General.Services.User;
using General.Core;
using General.Services.Role;
using General.Entities.User;
using General.Core.data;
using General.Entities.Role;
using General.Framework.Controllers;

namespace General.Mvc.Controllers
{

    public class HomeController : BaseController
    {
        private readonly IUserService _userService;

        private IRepository<User> _userRepository;
        private IRepository<Role> _roleRepository;
        private IRepository<Role> _otherRoleRepository;


        public HomeController(IUserService userService, IRepository<User> userRepository, IRepository<Role> roleRepository, IRepository<Role> otherRoleRepository)
        {
            this._userService = userService;
            this._userRepository = userRepository;
            this._roleRepository = roleRepository;
            this._otherRoleRepository = otherRoleRepository;
        }

        public IActionResult Index()
        {
            bool a = object.ReferenceEquals(_userRepository.dbContext, _roleRepository.dbContext);
            bool b = object.ReferenceEquals(_roleRepository, _otherRoleRepository);

            //var userlist = EngineContext.Current.Resove<IUserService>().GetAllUser();
            var userlist = _userService.GetAllUser();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
