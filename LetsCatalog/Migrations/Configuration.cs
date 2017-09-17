namespace LetsCatalog.Migrations
{
    using Catalog.Models.Entities;
    using CommonUtilities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LetsCatalog.Infrastructure.CatalogDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LetsCatalog.Infrastructure.CatalogDb context)
        {
            var user = new User { User_Id = 1, Name = "Ahmed", Email = "abc@gmail.com" };
            var cat = new Category { Category_Id = 1, Name = "Electronics", Created_Date = DateTime.Now, User = user, User_Id = user.User_Id };
            var subCat = new SubCategory { SubCateogy_Id = 1, Name = "Mobiles", Description = "", Created_Date = DateTime.Now, Category = cat, Category_Id = cat.Category_Id };
            var brand = new Brand { Brand_Id = 1, Name = "Samsung", Created_Date = DateTime.Now, SubCategory = subCat, SubCategory_Id = subCat.SubCateogy_Id};
            var prod = new Product { Product_Id = 1, Name = "S8", Brand = brand, Brand_Id = brand.Brand_Id, Created_Date = DateTime.Now, Description = "", SubCategory = subCat, SubCategory_Id = subCat.SubCateogy_Id };
            var prodPics = new ProductPics { Product = prod, Product_Id = prod.Product_Id, Picture_id = 1, Picture_URL = "" };
            var prodSpecs = new ProductSpecs { Spec_Id = 1, Color = Colors.Blue, Product = prod, Product_Id = prod.Product_Id };
            context.Users.AddOrUpdate(u => u.User_Id,user);
            context.Categories.AddOrUpdate(c => c.Category_Id, cat);
            context.SubCategories.AddOrUpdate(s => s.SubCateogy_Id, subCat);
            context.Brands.AddOrUpdate(b => b.Brand_Id, brand);
            context.Products.AddOrUpdate(p => p.Product_Id, prod);
            context.ProductPics.AddOrUpdate(pp => pp.Picture_id, prodPics);
            context.ProductSpecs.AddOrUpdate(ps=> ps.Spec_Id, prodSpecs);
            context.SaveChanges();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
