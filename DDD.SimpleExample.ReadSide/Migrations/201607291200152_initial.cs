namespace DDD.SimpleExample.ReadSide.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerModel",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Status = c.Int(nullable: false),
                        AggregateId = c.Guid(nullable: false),
                        Identity = c.Long(nullable: false, identity: true),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.AggregateId, unique: true)
                .Index(t => t.Identity, unique: true);
            
            CreateTable(
                "dbo.ProjectModel",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CustomerId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Status = c.Int(nullable: false),
                        AggregateId = c.Guid(nullable: false),
                        Identity = c.Long(nullable: false, identity: true),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerModel", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.AggregateId, unique: true)
                .Index(t => t.Identity, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectModel", "CustomerId", "dbo.CustomerModel");
            DropIndex("dbo.ProjectModel", new[] { "Identity" });
            DropIndex("dbo.ProjectModel", new[] { "AggregateId" });
            DropIndex("dbo.ProjectModel", new[] { "CustomerId" });
            DropIndex("dbo.CustomerModel", new[] { "Identity" });
            DropIndex("dbo.CustomerModel", new[] { "AggregateId" });
            DropTable("dbo.ProjectModel");
            DropTable("dbo.CustomerModel");
        }
    }
}
