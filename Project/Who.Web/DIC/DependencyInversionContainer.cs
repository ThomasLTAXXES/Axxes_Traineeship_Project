using Autofac;
using Autofac.Integration.Mvc;
using Who.BL.IRepositories;
using Who.BL.IServices;
using Who.BL.Services;
using Who.DAL.Repositories;
using Who.Data;

namespace Who.Web.DIC
{
    public static class DependencyInversionContainer
    {
        public static void Initialize(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(UserRepository)).As(typeof(IRepository<UserEntity>));
            builder.RegisterType(typeof(UserRepository)).As(typeof(IUserRepository));
            builder.RegisterType(typeof(Repository<ImageEntity>)).As(typeof(IRepository<ImageEntity>));
            builder.RegisterType(typeof(ImageRepository)).As(typeof(IImageRepository));
            builder.RegisterType(typeof(GameRepository)).As(typeof(IGameRepository));
            builder.RegisterType(typeof(RoundRepository)).As(typeof(IRoundRepository));
            builder.RegisterType(typeof(Repository<ImageInRoundEntity>)).As(typeof(IRepository<ImageInRoundEntity>));
            builder.RegisterType(typeof(Repository<MetaDataEntity>)).As(typeof(IRepository<MetaDataEntity>));
            builder.RegisterType(typeof(MetaDataRepository)).As(typeof(IMetaDataRepository));

            builder.RegisterType(typeof(UserService)).As(typeof(IUserService));
            builder.RegisterType(typeof(GameService)).As(typeof(IGameService));
            builder.RegisterType(typeof(ImageService)).As(typeof(IImageService));

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
        }
    }
}