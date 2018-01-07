namespace Catalog.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class collection_update : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brand",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Created_Date = c.DateTime(nullable: false),
                        SubCategory_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SubCategory", t => t.SubCategory_ID)
                .Index(t => t.SubCategory_ID);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Created_Date = c.DateTime(nullable: false),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.User_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.SubCategory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Description = c.String(),
                        Created_Date = c.DateTime(nullable: false),
                        Category_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Category", t => t.Category_ID)
                .Index(t => t.Category_ID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Description = c.String(),
                        Created_Date = c.DateTime(nullable: false),
                        Brand_ID = c.Int(nullable: false),
                        SubCategory_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Brand", t => t.Brand_ID, cascadeDelete: true)
                .ForeignKey("dbo.SubCategory", t => t.SubCategory_ID)
                .Index(t => t.Brand_ID)
                .Index(t => t.SubCategory_ID);
            
            CreateTable(
                "dbo.ProductPics",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Picture_URL = c.String(),
                        Product_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Product", t => t.Product_ID)
                .Index(t => t.Product_ID);
            
            CreateTable(
                "dbo.ProductSpecs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Color = c.Int(nullable: false),
                        Product_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Product", t => t.Product_ID)
                .Index(t => t.Product_ID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Email = c.String(nullable: false, maxLength: 100),
                        Picture_URL = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Category", "User_ID", "dbo.User");
            DropForeignKey("dbo.SubCategory", "Category_ID", "dbo.Category");
            DropForeignKey("dbo.Product", "SubCategory_ID", "dbo.SubCategory");
            DropForeignKey("dbo.ProductSpecs", "Product_ID", "dbo.Product");
            DropForeignKey("dbo.ProductPics", "Product_ID", "dbo.Product");
            DropForeignKey("dbo.Product", "Brand_ID", "dbo.Brand");
            DropForeignKey("dbo.Brand", "SubCategory_ID", "dbo.SubCategory");
            DropIndex("dbo.ProductSpecs", new[] { "Product_ID" });
            DropIndex("dbo.ProductPics", new[] { "Product_ID" });
            DropIndex("dbo.Product", new[] { "SubCategory_ID" });
            DropIndex("dbo.Product", new[] { "Brand_ID" });
            DropIndex("dbo.SubCategory", new[] { "Category_ID" });
            DropIndex("dbo.Category", new[] { "User_ID" });
            DropIndex("dbo.Brand", new[] { "SubCategory_ID" });
            DropTable("dbo.User");
            DropTable("dbo.ProductSpecs");
            DropTable("dbo.ProductPics");
            DropTable("dbo.Product");
            DropTable("dbo.SubCategory");
            DropTable("dbo.Category");
            DropTable("dbo.Brand");
        }
    }
}
