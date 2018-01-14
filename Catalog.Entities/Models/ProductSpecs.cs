using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Models.Entities
{
    /// <summary>
    /// The product specification entity to hold product specifications
    /// </summary>
    public class ProductSpecs
    {
        /// <summary>
        /// The primary key of product specification
        /// </summary>
        [Key, ForeignKey("Product")]
        public int ProductID { get; set; }

        /// <summary>
        /// The color of the product
        /// </summary>
        [Display(Name ="Color")]
        public Colors Color { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual Product Product { get; set; }
    }
}
