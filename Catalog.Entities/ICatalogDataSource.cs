using Catalog.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Entities
{
    /// <summary>
    /// The catalog data source
    /// </summary>
    public interface ICatalogDataSource
    {
        /// <summary>
        /// The categories
        /// </summary>
        IQueryable<Category> Categories { get; }

        /// <summary>
        /// The subcategories
        /// </summary>
        IQueryable<SubCategory> SubCategories { get; }

        /// <summary>
        /// The users
        /// </summary>
        IQueryable<User> Users { get; }

        /// <summary>
        /// The brands
        /// </summary>
        IQueryable<Brand> Brands { get; }

        /// <summary>
        /// The products
        /// </summary>
        IQueryable<Product> Products { get; }

        /// <summary>
        /// The product pictures
        /// </summary>
        IQueryable<ProductPics> Product_Pics { get; }

        /// <summary>
        /// The product specifications
        /// </summary>
        IQueryable<ProductSpecs> Product_Specs { get; }
    }
}
