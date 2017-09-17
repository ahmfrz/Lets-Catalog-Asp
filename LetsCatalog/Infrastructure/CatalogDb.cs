using Catalog.Entities;
using Catalog.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsCatalog.Infrastructure
{
    /// <summary>
    /// The Catalog repository class
    /// </summary>
    public class CatalogDb: DbContext, ICatalogDataSource
    {
        /// <summary>
        /// Creates a new instance of CatalogDb class
        /// </summary>
        //public CatalogDb():base("DefaultConnection")
        //{

        //}
        /// <summary>
        /// The catalog brands table
        /// </summary>
        public DbSet<Brand> Brands { get; set; }

        /// <summary>
        /// The catalog products table
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// The product pictures table
        /// </summary>
        public DbSet<ProductPics> ProductPics { get; set; }

        /// <summary>
        /// The product specifications table
        /// </summary>
        public DbSet<ProductSpecs> ProductSpecs { get; set; }

        /// <summary>
        /// The catalog categories table
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// The catalog subcategories table
        /// </summary>
        public DbSet<SubCategory> SubCategories { get; set; }

        /// <summary>
        /// The users table
        /// </summary>
        public DbSet<User> Users { get; set; }

        #region ICatalogDataSource
        /// <summary>
        /// The categories
        /// </summary>
        IQueryable<Category> ICatalogDataSource.Categories
        {
            get
            {
                return Categories;
            }
        }

        /// <summary>
        /// The subcategories
        /// </summary>
        IQueryable<SubCategory> ICatalogDataSource.SubCategories
        {
            get
            {
                return SubCategories;
            }
        }

        /// <summary>
        /// The users
        /// </summary>
        IQueryable<User> ICatalogDataSource.Users
        {
            get
            {
                return Users;
            }
        }

        /// <summary>
        /// The brands
        /// </summary>
        IQueryable<Brand> ICatalogDataSource.Brands
        {
            get
            {
                return Brands;
            }
        }

        /// <summary>
        /// The products
        /// </summary>
        IQueryable<Product> ICatalogDataSource.Products
        {
            get
            {
                return Products;
            }
        }

        /// <summary>
        /// The product pictures
        /// </summary>
        IQueryable<ProductPics> ICatalogDataSource.Product_Pics
        {
            get
            {
                return ProductPics;
            }
        }

        /// <summary>
        /// The product specifications
        /// </summary>
        IQueryable<ProductSpecs> ICatalogDataSource.Product_Specs
        {
            get
            {
                return ProductSpecs;
            }
        }
        #endregion
    }
}
