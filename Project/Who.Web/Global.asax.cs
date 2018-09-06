using Autofac;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Who.BL.IServices;
using Who.DAL;
using Who.DAL.Services;
using Who.Data;
using Who.Web.Controllers;
using Autofac.Integration.Mvc;
using Who.DAL.DatabaseInitialize;
using Who.BL.Services;

namespace Who.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IContainer Container { get; set; }
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType(typeof(Repository<UserEntity>)).As(typeof(IRepository<UserEntity>));
            builder.RegisterType(typeof(Repository<ImageEntity>)).As(typeof(IRepository<ImageEntity>));
            builder.RegisterType(typeof(Repository<GameEntity>)).As(typeof(IRepository<GameEntity>));
            builder.RegisterType(typeof(Repository<RoundEntity>)).As(typeof(IRepository<RoundEntity>));
            builder.RegisterType(typeof(Repository<ImageInRoundEntity>)).As(typeof(IRepository<ImageInRoundEntity>));
            builder.RegisterType(typeof(Repository<MetaDataEntity>)).As(typeof(IRepository<MetaDataEntity>));
            builder.RegisterType(typeof(GameService)).As(typeof(IGameService));
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            Container = builder.Build(); 

            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));

            Database.SetInitializer(new ApplicationDbContextInitializer());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        
    }
}
