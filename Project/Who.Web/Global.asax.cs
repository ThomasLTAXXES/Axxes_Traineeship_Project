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
using Who.DAL.Migrations;
using Who.DAL.Services;
using Who.Data;
using Who.Web.Controllers;
using Autofac.Integration.Mvc;

namespace Who.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IContainer Container { get; set; }
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType(typeof(Repository<UserEntity>)).As(typeof(IRepository<UserEntity>));
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            Container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, DbMigrationConfig>());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        
    }
}
