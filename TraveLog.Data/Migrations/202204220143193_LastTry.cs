namespace TraveLog.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LastTry : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blog",
                c => new
                    {
                        BlogId = c.Int(nullable: false, identity: true),
                        Thoughts = c.String(),
                        VisitedId = c.Int(nullable: false),
                        CountryId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.BlogId)
                .ForeignKey("dbo.ApplicationUser", t => t.UserId)
                .ForeignKey("dbo.Country", t => t.CountryId, cascadeDelete: true)
                .ForeignKey("dbo.Location", t => t.LocationId, cascadeDelete: true)
                .ForeignKey("dbo.Visited", t => t.VisitedId, cascadeDelete: true)
                .Index(t => t.VisitedId)
                .Index(t => t.CountryId)
                .Index(t => t.LocationId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(),
                        ProfileName = c.String(),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        CountryName = c.String(),
                        Continent = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CountryId)
                .ForeignKey("dbo.ApplicationUser", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Location",
                c => new
                    {
                        LocationId = c.Int(nullable: false, identity: true),
                        Cities = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.LocationId)
                .ForeignKey("dbo.ApplicationUser", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Visited",
                c => new
                    {
                        VisitedId = c.Int(nullable: false, identity: true),
                        DateVisited = c.DateTime(nullable: false),
                        InitialThoughts = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.VisitedId)
                .ForeignKey("dbo.ApplicationUser", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Blog", "VisitedId", "dbo.Visited");
            DropForeignKey("dbo.Visited", "UserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.Blog", "LocationId", "dbo.Location");
            DropForeignKey("dbo.Location", "UserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.Blog", "CountryId", "dbo.Country");
            DropForeignKey("dbo.Country", "UserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.Blog", "UserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropIndex("dbo.Visited", new[] { "UserId" });
            DropIndex("dbo.Location", new[] { "UserId" });
            DropIndex("dbo.Country", new[] { "UserId" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Blog", new[] { "UserId" });
            DropIndex("dbo.Blog", new[] { "LocationId" });
            DropIndex("dbo.Blog", new[] { "CountryId" });
            DropIndex("dbo.Blog", new[] { "VisitedId" });
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Visited");
            DropTable("dbo.Location");
            DropTable("dbo.Country");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.Blog");
        }
    }
}
