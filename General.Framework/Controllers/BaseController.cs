using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace General.Framework.Controllers
{
    public class BaseController : Controller
    {
        public AjaxResult AjaxResult
        {
            get { return new AjaxResult(); }
        }
    }
}
