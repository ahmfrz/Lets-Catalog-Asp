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
    /// The product entity to hold product information
    /// </summary>
    public class Product
    {
        /// <summary>
        /// The primary key of the product
        /// </summary>
        [Key]
        public int ProductID { get; set; }

        /// <summary>
        /// The name of the product
        /// </summary>
        [Required]
        [StringLength(150)]

        public string Name { get; set; }

        /// <summary>
        /// The short description of the product
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The created date of the product
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [ScaffoldColumn(false)]
        public DateTime Created_Date { get; set; }

        /// <summary>
        /// The brand of the product
        /// </summary>
        [Required]
        public Brand Brand { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual ProductPics ProductPics { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual ProductSpecs ProductSpecs { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual SubCategory SubCategory { get; set; }
    }
}
