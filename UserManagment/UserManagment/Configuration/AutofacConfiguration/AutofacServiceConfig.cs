using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoMapper;
using UserManagment.BLL.Services;
using UserManagment.BLL.Services.RightsResolvers;
using UserManagment.DAL.Contexts;
using UserManagment.DAL.Repositories;
using UserManagment.DAL.UnitOfWork;
using UserManagment.Domain.Enums;
using UserManagment.Domain.Interfaces.Repositories;
using UserManagment.Domain.Interfaces.Services;
using UserManagment.Domain.Interfaces.UnitOfWork;
using UserManagment.Domain.Models;

namespace UserManagment.Configuration.AutofacConfiguration
{
    public class AutofacServiceConfig
    {
        private static IContainer container;

        public static void SetConfiguration()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            WebApiConfig(builder, config);
            DalConfig(builder);
            BllConfig(builder);
            AutoMapperConfig(builder);
            ServiceConfig(builder);
            container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        public static IUserService GetUserService()
        {
            var a = container.Resolve<IUserService>();
            return a;
        }

        private static void WebApiConfig(ContainerBuilder builder, HttpConfiguration config)
        {
            builder.RegisterControllers(typeof(WebApiApplication).Assembly);
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);
            builder.RegisterWebApiModelBinderProvider();
        }

        private static void DalConfig(ContainerBuilder builder)
        {
            builder.RegisterType<UserStoryContext>().InstancePerLifetimeScope();
            builder.RegisterType<Repository<User>>().As<IRepository<User>>().InstancePerLifetimeScope();
            builder.RegisterType<Repository<Role>>().As<IRepository<Role>>().InstancePerLifetimeScope();
            builder.RegisterType<Repository<Job>>().As<IRepository<Job>>().InstancePerLifetimeScope();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<JobRepository>().As<IJobRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
        }

        private static void BllConfig(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<RoleService>().As<IRoleService>().InstancePerLifetimeScope();
            builder.RegisterType<JobService>().As<IJobService>().InstancePerLifetimeScope();
        }

        private static void AutoMapperConfig(ContainerBuilder builder)
        {
            builder.Register(c => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingConfigurator());
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().InstancePerLifetimeScope();
        }

        private static void ServiceConfig(ContainerBuilder builder)
        {
            builder.RegisterType<UserRightsResolver>().Keyed<IRigtsResolver>(Roles.User);
            builder.RegisterType<ManagerRightsResolver>().Keyed<IRigtsResolver>(Roles.Manager);
            builder.RegisterType<AdminRightsResolver>().Keyed<IRigtsResolver>(Roles.Admin);
        }

    }
}