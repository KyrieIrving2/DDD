using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace General.Framework.Controllers
{
    public class BaseController : Controller
    {
        private AjaxResult _ajaxResult;
        public BaseController()
        {
            this._ajaxResult = new AjaxResult();
        }

        public AjaxResult AjaxResult
        {
            get { return _ajaxResult; }
        }
    }
}
