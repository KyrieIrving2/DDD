using General.Core.Librarys;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace General.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 通过反射程序集注入
        /// </summary>
        /// <param name="services"></param>
        public static void AddAssembly(this IServiceCollection services,string assemblyName,ServiceLifetime serviceLifetime=ServiceLifetime.Scoped)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services) + "为空");
            if (assemblyName == null)
                throw new ArgumentNullException(nameof(assemblyName) + "为空");
            var assembly = RunTimeHelper.GetAssemblyName(assemblyName);
            if (assembly == null)
                throw new DllNotFoundException(nameof(assembly) + "为空");
            var types = assembly.GetTypes();
            var list = types.Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType).ToList();
            foreach (var type in list)
            {
                var interfaceList = type.GetInterfaces();
                if (interfaceList.Any())
                {
                    var inter = interfaceList.First();
                    switch(serviceLifetime)
                    {
                        case ServiceLifetime.Scoped:
                            services.AddScoped(inter, type);
                            break;
                        case ServiceLifetime.Singleton:
                            services.AddSingleton(inter, type);
                            break;
                        case ServiceLifetime.Transient:
                            services.AddTransient(inter, type);
                            break;
                    }      
                }
            }
        }
    }
}
