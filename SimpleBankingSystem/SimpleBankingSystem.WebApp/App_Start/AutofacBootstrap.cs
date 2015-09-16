using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Kingstar.Data.Base;
using SimpleBankingSystem.Domain.Repositories;
using SimpleBankingSystem.Domain.Services;
using System.Reflection;
using System.Web.Mvc;

namespace SimpleBankingSystem.WebApp.App_Start
{
    public class AutofacBootstrap
    {
        public static IContainer Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(typeof(IContextFactory).Assembly)
                .Where(t => t.Name.EndsWith("Impl"))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(IBankService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            return container;
        }
    }
}