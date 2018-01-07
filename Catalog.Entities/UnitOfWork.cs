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
    public class UnitOfWork : IUnitOfWork
    {
        #region Private Members
        /// <summary>
        /// The context
        /// </summary>
        private CatalogContext context;

        /// <summary>
        /// To detect redundant calls
        /// </summary>
        private bool disposedValue = false;

        /// <summary>
        /// The category repository
        /// </summary>
        private GenericRepository<Category> categoryRepository;

        /// <summary>
        /// The sub-category repository
        /// </summary>
        private GenericRepository<SubCategory> subcategoryRepository;

        /// <summary>
        /// The user repository
        /// </summary>
        private GenericRepository<User> userRepository;

        /// <summary>
        /// The brand repository
        /// </summary>
        private GenericRepository<Brand> brandRepository;

        /// <summary>
        /// The product repository
        /// </summary>
        private GenericRepository<Product> productRepository;

        /// <summary>
        /// The product pictures repository
        /// </summary>
        private GenericRepository<ProductPics> productPicsRepository;

        /// <summary>
        /// The product specifications repository
        /// </summary>
        private GenericRepository<ProductSpecs> productSpecsRepository;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new instance of UnitOfWork
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWork()
        {
            this.context = new CatalogContext();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the category repository
        /// </summary>
        public GenericRepository<Category> CategoryRepository
        {
            get
            {
                if (categoryRepository == null)
                {
                    categoryRepository = new GenericRepository<Category>(context);
                }

                return categoryRepository;
            }
        }

        /// <summary>
        /// Gets the sub-category repository
        /// </summary>
        public GenericRepository<SubCategory> SubCategoryRepository
        {
            get
            {
                if (subcategoryRepository == null)
                {
                    subcategoryRepository = new GenericRepository<SubCategory>(context);
                }

                return subcategoryRepository;
            }
        }

        /// <summary>
        /// Gets the user repository
        /// </summary>
        public GenericRepository<User> UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new GenericRepository<User>(context);
                }
                return userRepository;
            }
        }

        /// <summary>
        /// Gets the brand repository
        /// </summary>
        public GenericRepository<Brand> BrandRepository
        {
            get
            {
                if (brandRepository == null)
                {
                    brandRepository = new GenericRepository<Brand>(context);
                }

                return brandRepository;
            }
        }

        /// <summary>
        /// Gets the product repository
        /// </summary>
        public GenericRepository<Product> ProductRepository
        {
            get
            {
                if (productRepository == null)
                {
                    productRepository = new GenericRepository<Product>(context);
                }

                return productRepository;
            }
        }

        /// <summary>
        /// Gets the product pictures repository
        /// </summary>
        public GenericRepository<ProductPics> ProductPicsRepository
        {
            get
            {
                if (productPicsRepository == null)
                {
                    productPicsRepository = new GenericRepository<ProductPics>(context);
                }

                return productPicsRepository;
            }
        }

        /// <summary>
        /// Gets the product specifications repository
        /// </summary>
        public GenericRepository<ProductSpecs> ProductSpecsRepository
        {
            get
            {
                if (productSpecsRepository == null)
                {
                    productSpecsRepository = new GenericRepository<ProductSpecs>(context);
                }

                return productSpecsRepository;
            }
        }
        #endregion

        #region IDisposable Support
        /// <summary>
        ///
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (context != null)
                    {
                        context.Dispose();
                    }
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Save changes
        /// </summary>
        public void Save()
        {
            context.SaveChanges();
        }
        #endregion
    }
}
