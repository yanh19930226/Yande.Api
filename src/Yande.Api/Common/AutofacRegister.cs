using Autofac;
using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Yande.Api.Common
{
    public class AutofacRegister : Autofac.Module
    {
        protected override void Load(ContainerBuilder container)
        {
            var assemblysServices = Assembly.Load("Yande.Core.Service"); // 需要暴露接口所在的程序集
            container.RegisterAssemblyTypes(assemblysServices)
                .SingleInstance()  // 设置单例注入
               .AsImplementedInterfaces() // 把所有接口全暴露出来
               .EnableInterfaceInterceptors();
        }
    }
}
