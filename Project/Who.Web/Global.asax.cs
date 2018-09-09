using Autofac;
using Autofac.Integration.Mvc;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Who.BL.IRepositories;
using Who.BL.IServices;
using Who.BL.Services;
using Who.DAL.DatabaseInitialize;
using Who.DAL.Repositories;
using Who.DAL.Services;
using Who.Data;

namespace Who.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IContainer Container { get; set; }
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType(typeof(UserRepository)).As(typeof(IRepository<UserEntity>));
            builder.RegisterType(typeof(UserRepository)).As(typeof(IUserRepository));
            builder.RegisterType(typeof(Repository<ImageEntity>)).As(typeof(IRepository<ImageEntity>));
            builder.RegisterType(typeof(ImageRepository)).As(typeof(IImageRepository)); 
            builder.RegisterType(typeof(GameRepository)).As(typeof(IGameRepository));
            builder.RegisterType(typeof(Repository<RoundEntity>)).As(typeof(IRepository<RoundEntity>));
            builder.RegisterType(typeof(Repository<ImageInRoundEntity>)).As(typeof(IRepository<ImageInRoundEntity>));
            builder.RegisterType(typeof(Repository<MetaDataEntity>)).As(typeof(IRepository<MetaDataEntity>));

            builder.RegisterType(typeof(UserService)).As(typeof(IUserService));
            builder.RegisterType(typeof(GameService)).As(typeof(IGameService));
            builder.RegisterType(typeof(ImageService)).As(typeof(IImageService));
            
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
