using System;
using System.Data.Entity;
using System.IO;
using System.Reflection;
using Who.Data;
using Who.Data.Enums;

namespace Who.DAL.DatabaseInitialize
{
    public class ApplicationDbContextInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            context.MetaDataEntities.Add(new MetaDataEntity
            {
                Name = MetaDataEnum.ImagesPerRound.ToString(),
                Value = 4.ToString(),
                Type = MetaDataTypeEnum.Int.ToString()
            });

            context.MetaDataEntities.Add(new MetaDataEntity
            {
                Name = MetaDataEnum.RoundsPerGame.ToString(),
                Value = 5.ToString(),
                Type = MetaDataTypeEnum.Int.ToString()
            });
            
            string[] fileNames = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin") + "/StoredProcedures");
            foreach(string fileName in fileNames)
            {
                string script = File.ReadAllText(fileName);
                context.Database.ExecuteSqlCommand(script);
            }
           

            context.Users.Add(new UserEntity
            {
                FullName = "Demo Tester"
            });
        }
    }
}
