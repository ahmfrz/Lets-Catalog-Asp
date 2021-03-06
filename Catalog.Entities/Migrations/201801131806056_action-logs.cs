namespace Catalog.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class actionlogs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActionLog",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Controller = c.String(),
                        Action = c.String(),
                        IP = c.String(),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ActionLog");
        }
    }
}
