namespace Catalog.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class productforeignkeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductPics", "Product_ID", "dbo.Product");
            DropForeignKey("dbo.ProductSpecs", "Product_ID", "dbo.Product");
            DropIndex("dbo.ProductPics", new[] { "Product_ID" });
            DropIndex("dbo.ProductSpecs", new[] { "Product_ID" });
            DropPrimaryKey("dbo.Product");
            DropPrimaryKey("dbo.ProductPics");
            DropPrimaryKey("dbo.ProductSpecs");
            DropColumn("dbo.Product", "ID");
            DropColumn("dbo.ProductPics", "ID");
            DropColumn("dbo.ProductSpecs", "ID");
            RenameColumn(table: "dbo.ProductPics", name: "Product_ID", newName: "ProductID");
            RenameColumn(table: "dbo.ProductSpecs", name: "Product_ID", newName: "ProductID");
            AddColumn("dbo.Product", "ProductID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.ProductPics", "ProductID", c => c.Int(nullable: false));
            AlterColumn("dbo.ProductSpecs", "ProductID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Product", "ProductID");
            AddPrimaryKey("dbo.ProductPics", "ProductID");
            AddPrimaryKey("dbo.ProductSpecs", "ProductID");
            CreateIndex("dbo.ProductPics", "ProductID");
            CreateIndex("dbo.ProductSpecs", "ProductID");
            AddForeignKey("dbo.ProductPics", "ProductID", "dbo.Product", "ProductID");
            AddForeignKey("dbo.ProductSpecs", "ProductID", "dbo.Product", "ProductID");
        }

        public override void Down()
        {
            DropForeignKey("dbo.ProductSpecs", "ProductID", "dbo.Product");
            DropForeignKey("dbo.ProductPics", "ProductID", "dbo.Product");
            DropIndex("dbo.ProductSpecs", new[] { "ProductID" });
            DropIndex("dbo.ProductPics", new[] { "ProductID" });
            DropPrimaryKey("dbo.ProductSpecs");
            DropPrimaryKey("dbo.ProductPics");
            DropPrimaryKey("dbo.Product");
            DropColumn("dbo.Product", "ProductID");
            AddColumn("dbo.ProductSpecs", "ID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.ProductPics", "ID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Product", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.ProductSpecs", "ProductID", c => c.Int());
            AlterColumn("dbo.ProductPics", "ProductID", c => c.Int());
            AddPrimaryKey("dbo.ProductSpecs", "ID");
            AddPrimaryKey("dbo.ProductPics", "ID");
            AddPrimaryKey("dbo.Product", "ID");
            RenameColumn(table: "dbo.ProductSpecs", name: "ProductID", newName: "Product_ID");
            RenameColumn(table: "dbo.ProductPics", name: "ProductID", newName: "Product_ID");
            CreateIndex("dbo.ProductSpecs", "Product_ID");
            CreateIndex("dbo.ProductPics", "Product_ID");
            AddForeignKey("dbo.ProductSpecs", "Product_ID", "dbo.Product", "ID");
            AddForeignKey("dbo.ProductPics", "Product_ID", "dbo.Product", "ID");
        }
    }
}
