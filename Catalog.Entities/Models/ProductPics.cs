using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Models.Entities
{
    /// <summary>
    /// The product pics entity to hold product picture information
    /// </summary>
    public class ProductPics
    {
        /// <summary>
        /// The primary key of the product picture
        /// </summary>
        [Key, ForeignKey("Product")]
        public int ProductID { get; set; }

        /// <summary>
        /// The picture url of the product picture
        /// </summary>
        [Display(Name ="Picture URL")]
        [DataType(DataType.ImageUrl)]
        public string Picture_URL { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual Product Product { get; set; }
    }
}
