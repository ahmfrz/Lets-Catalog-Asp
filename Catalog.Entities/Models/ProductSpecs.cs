using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtilities;
using System.ComponentModel.DataAnnotations;

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
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// The color of the product
        /// </summary>
        public Colors Color { get; set; }

        ///// <summary>
        ///// The foreign key of the product specification
        ///// </summary>
        //[Required]
        //[ScaffoldColumn(false)]
        //public int Product_Id { get; set; }

        ///// <summary>
        ///// The product of the product specification
        ///// </summary>
        //[Required]
        //[ScaffoldColumn(false)]
        //public Product Product { get; set; }
    }
}
