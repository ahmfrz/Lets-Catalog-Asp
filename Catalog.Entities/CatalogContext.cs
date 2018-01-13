using Catalog.Models.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Catalog.Entities
{
    public class CatalogContext : DbContext
    {
        #region Properties
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

        /// <summary>
        /// The action logs
        /// </summary>
        public DbSet<ActionLog> Logs { get; set; }
        #endregion

        #region Public Methods
        /// <summary>
        ///
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<Course>()
            //    .HasMany(c => c.Instructors).WithMany(i => i.Courses)
            //    .Map(t => t.MapLeftKey("CourseID")
            //        .MapRightKey("PersonID")
            //        .ToTable("CourseInstructor"));
        }
        #endregion
    }
}