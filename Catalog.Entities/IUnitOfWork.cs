using Catalog.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Entities
{
    public interface IUnitOfWork:IDisposable
    {
        GenericRepository<Category> CategoryRepository { get; }
        GenericRepository<SubCategory> SubCategoryRepository { get; }
        GenericRepository<Brand> BrandRepository { get; }
        GenericRepository<User> UserRepository { get; }
        GenericRepository<Product> ProductRepository { get; }
        GenericRepository<ProductPics> ProductPicsRepository { get; }
        GenericRepository<ProductSpecs> ProductSpecsRepository { get; }
        GenericRepository<ActionLog> ActionLogsRepository { get; }

        void Save();
    }
}
