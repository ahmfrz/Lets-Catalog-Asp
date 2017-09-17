namespace LetsCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Brand_Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Created_Date = c.DateTime(nullable: false),
                        SubCategory_Id = c.Int(nullable: false),
                        SubCategory_SubCateogy_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Brand_Id)
                .ForeignKey("dbo.SubCategories", t => t.SubCategory_SubCateogy_Id, cascadeDelete: false)
                .Index(t => t.SubCategory_SubCateogy_Id);

            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        SubCateogy_Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Description = c.String(),
                        Created_Date = c.DateTime(nullable: false),
                        Category_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubCateogy_Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.Category_Id);

            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Category_Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Created_Date = c.DateTime(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Category_Id)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);

            CreateTable(
                "dbo.Users",
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
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Product_Id);

            CreateTable(
                "dbo.Products",
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
                .ForeignKey("dbo.Brands", t => t.Brand_Id, cascadeDelete: true)
                .ForeignKey("dbo.SubCategories", t => t.SubCategory_SubCateogy_Id, cascadeDelete: false)
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
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Product_Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.ProductSpecs", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductPics", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "SubCategory_SubCateogy_Id", "dbo.SubCategories");
            DropForeignKey("dbo.Products", "Brand_Id", "dbo.Brands");
            DropForeignKey("dbo.Brands", "SubCategory_SubCateogy_Id", "dbo.SubCategories");
            DropForeignKey("dbo.SubCategories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Categories", "User_Id", "dbo.Users");
            DropIndex("dbo.ProductSpecs", new[] { "Product_Id" });
            DropIndex("dbo.Products", new[] { "SubCategory_SubCateogy_Id" });
            DropIndex("dbo.Products", new[] { "Brand_Id" });
            DropIndex("dbo.ProductPics", new[] { "Product_Id" });
            DropIndex("dbo.Categories", new[] { "User_Id" });
            DropIndex("dbo.SubCategories", new[] { "Category_Id" });
            DropIndex("dbo.Brands", new[] { "SubCategory_SubCateogy_Id" });
            DropTable("dbo.ProductSpecs");
            DropTable("dbo.Products");
            DropTable("dbo.ProductPics");
            DropTable("dbo.Users");
            DropTable("dbo.Categories");
            DropTable("dbo.SubCategories");
            DropTable("dbo.Brands");
        }
    }
}
