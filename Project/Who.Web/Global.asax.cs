using Autofac;
using Autofac.Integration.Mvc;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Who.DAL.DatabaseInitialize;
using Who.Web.DIC;

namespace Who.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IContainer Container { get; set; }
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            DependencyInversionContainer.Initialize(builder);
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
