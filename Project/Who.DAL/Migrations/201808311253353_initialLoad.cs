namespace Who.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialLoad : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Score = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        Duration = c.Time(nullable: false, precision: 7),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Rounds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CorrectAnswer = c.Boolean(),
                        Game_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .Index(t => t.Game_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Rounds", "Game_Id", "dbo.Games");
            DropIndex("dbo.Rounds", new[] { "Game_Id" });
            DropIndex("dbo.Games", new[] { "User_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Rounds");
            DropTable("dbo.Games");
        }
    }
}
