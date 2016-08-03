namespace DDD.SimpleExample.ReadSide.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserModel",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Role = c.Int(nullable: false),
                        AggregateId = c.Guid(nullable: false),
                        Identity = c.Long(nullable: false, identity: true),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.AggregateId, unique: true)
                .Index(t => t.Identity, unique: true);
            
            CreateTable(
                "dbo.UserModelProjectModel",
                c => new
                    {
                        UserModel_Id = c.String(nullable: false, maxLength: 128),
                        ProjectModel_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserModel_Id, t.ProjectModel_Id })
                .ForeignKey("dbo.UserModel", t => t.UserModel_Id, cascadeDelete: true)
                .ForeignKey("dbo.ProjectModel", t => t.ProjectModel_Id, cascadeDelete: true)
                .Index(t => t.UserModel_Id)
                .Index(t => t.ProjectModel_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserModelProjectModel", "ProjectModel_Id", "dbo.ProjectModel");
            DropForeignKey("dbo.UserModelProjectModel", "UserModel_Id", "dbo.UserModel");
            DropIndex("dbo.UserModelProjectModel", new[] { "ProjectModel_Id" });
            DropIndex("dbo.UserModelProjectModel", new[] { "UserModel_Id" });
            DropIndex("dbo.UserModel", new[] { "Identity" });
            DropIndex("dbo.UserModel", new[] { "AggregateId" });
            DropTable("dbo.UserModelProjectModel");
            DropTable("dbo.UserModel");
        }
    }
}
