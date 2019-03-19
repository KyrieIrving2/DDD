using General.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace General.Mvc
{
    public class GeneralEngine: IEngine
    {
        private IServiceProvider _serviceProvider;

        public GeneralEngine(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public T Resove<T>() where T : class
        {
            return _serviceProvider.GetService<T>();
        }
    }
}
