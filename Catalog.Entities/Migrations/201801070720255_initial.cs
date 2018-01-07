namespace Catalog.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brand",
                c => new
                    {
                        Brand_Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Created_Date = c.DateTime(nullable: false),
                        SubCategory_Id = c.Int(nullable: false),
                        SubCategory_SubCateogy_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Brand_Id)
                .ForeignKey("dbo.SubCategory", t => t.SubCategory_SubCateogy_Id, cascadeDelete: true)
                .Index(t => t.SubCategory_SubCateogy_Id);
            
            CreateTable(
                "dbo.SubCategory",
                c => new
                    {
                        SubCateogy_Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Description = c.String(),
                        Created_Date = c.DateTime(nullable: false),
                        Category_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubCateogy_Id)
                .ForeignKey("dbo.Category", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Category_Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Created_Date = c.DateTime(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Category_Id)
                .ForeignKey("dbo.User", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        User_Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Email = c.String(nullable: false, maxLength: 100),
                        Picture_URL = c.String(),
                    })
                .PrimaryKey(t => t.User_Id);
            
            CreateTable(
                "dbo.ProductPics",
                c => new
                    {
                        Picture_id = c.Int(nullable: false, identity: true),
                        Picture_URL = c.String(),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Picture_id)
                .ForeignKey("dbo.Product", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Product_Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Description = c.String(),
                        Created_Date = c.DateTime(nullable: false),
                        SubCategory_Id = c.Int(nullable: false),
                        Brand_Id = c.Int(nullable: false),
                        SubCategory_SubCateogy_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Product_Id)
                .ForeignKey("dbo.Brand", t => t.Brand_Id, cascadeDelete: true)
                .ForeignKey("dbo.SubCategory", t => t.SubCategory_SubCateogy_Id, cascadeDelete: true)
                .Index(t => t.Brand_Id)
                .Index(t => t.SubCategory_SubCateogy_Id);
            
            CreateTable(
                "dbo.ProductSpecs",
                c => new
                    {
                        Spec_Id = c.Int(nullable: false, identity: true),
                        Color = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Spec_Id)
                .ForeignKey("dbo.Product", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductSpecs", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.ProductPics", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.Product", "SubCategory_SubCateogy_Id", "dbo.SubCategory");
            DropForeignKey("dbo.Product", "Brand_Id", "dbo.Brand");
            DropForeignKey("dbo.Brand", "SubCategory_SubCateogy_Id", "dbo.SubCategory");
            DropForeignKey("dbo.SubCategory", "Category_Id", "dbo.Category");
            DropForeignKey("dbo.Category", "User_Id", "dbo.User");
            DropIndex("dbo.ProductSpecs", new[] { "Product_Id" });
            DropIndex("dbo.Product", new[] { "SubCategory_SubCateogy_Id" });
            DropIndex("dbo.Product", new[] { "Brand_Id" });
            DropIndex("dbo.ProductPics", new[] { "Product_Id" });
            DropIndex("dbo.Category", new[] { "User_Id" });
            DropIndex("dbo.SubCategory", new[] { "Category_Id" });
            DropIndex("dbo.Brand", new[] { "SubCategory_SubCateogy_Id" });
            DropTable("dbo.ProductSpecs");
            DropTable("dbo.Product");
            DropTable("dbo.ProductPics");
            DropTable("dbo.User");
            DropTable("dbo.Category");
            DropTable("dbo.SubCategory");
            DropTable("dbo.Brand");
        }
    }
}
