using Catalog.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsCatalog.Models
{
    /// <summary>
    /// The Catalog repository class
    /// </summary>
    public class CatalogDb: DbContext
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
    }
}
