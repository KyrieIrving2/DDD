using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace General.Core.Librarys
{
   public class RunTimeHelper
    {
        public static Assembly GetAssemblyName(string assemblyName)
        {
            return AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(assemblyName));
        }
    }
}
