using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Key]
        public int ID { get; set; }

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

        ///// <summary>
        ///// The product id foreign key of the product picture
        ///// </summary>
        //[Required]
        //[ScaffoldColumn(false)]
        //public int Product_Id { get; set; }

        ///// <summary>
        ///// The product of the product picture
        ///// </summary>
        //[Required]
        //[ScaffoldColumn(false)]
        //public Product Product { get; set; }
    }
}
