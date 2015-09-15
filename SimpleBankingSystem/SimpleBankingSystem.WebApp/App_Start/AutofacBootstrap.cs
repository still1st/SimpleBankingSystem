using Autofac;
using Autofac.Integration.Mvc;
using SimpleBankingSystem.Domain.Services;
using System.Reflection;

namespace SimpleBankingSystem.WebApp.App_Start
{
    public class AutofacBootstrap
    {
        public static IContainer Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterAssemblyTypes(typeof(IBankService).Assembly)
                .Where(t => t.Name.EndsWith("Repository") || t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            return builder.Build();
        }
    }
}