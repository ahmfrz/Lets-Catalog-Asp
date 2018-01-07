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
    /// The subcategory entity to hold subcategory information
    /// </summary>
    public class SubCategory
    {
        /// <summary>
        /// The primary key of the subcategory
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// The name of the subcategory
        /// </summary>
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        /// <summary>
        /// The description of the subcategory
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The created date of the subcategory
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [ScaffoldColumn(false)]
        public DateTime Created_Date { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual ICollection<Product> Products { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual ICollection<Brand> Brands { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual Category Category { get; set; }

        ///// <summary>
        ///// The category id foreign key of the subcategory
        ///// </summary>
        //[Required]
        //[ScaffoldColumn(false)]
        //public int Category_Id { get; set; }

        ///// <summary>
        ///// The category of the subcategory
        ///// </summary>
        //[Required]
        //[ScaffoldColumn(false)]
        //public Category Category { get; set; }
    }
}
