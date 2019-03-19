using System;
using System.Collections.Generic;
using System.Text;

namespace General.Framework.Infrastructure
{
   public interface IWorkContext
    {
        Entities.User.User CurrentUser { get; }
    }
}
