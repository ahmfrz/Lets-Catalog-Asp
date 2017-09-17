using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int Product_Id { get; set; }

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
        public DateTime Created_Date { get; set; }

        /// <summary>
        /// The subcategory id foreign key of the product
        /// </summary>
        [Required]
        public int SubCategory_Id { get; set; }

        /// <summary>
        /// The subcategory of the product
        /// </summary>
        [Required]
        public SubCategory SubCategory { get; set; }

        /// <summary>
        /// The brand id foreign key of the product
        /// </summary>
        [Required]
        public int BrandId { get; set; }

        /// <summary>
        /// The brand of the product
        /// </summary>
        [Required]
        public Brand Brand { get; set; }

        /// <summary>
        /// The user id foreign key of the product
        /// </summary>
        [Required]
        public int User_Id { get; set; }

        /// <summary>
        /// The user who created the product
        /// </summary>
        [Required]
        public User User { get; set; }
    }
}
