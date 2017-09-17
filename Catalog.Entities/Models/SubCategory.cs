using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int SubCateogy_Id { get; set; }

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
        public DateTime Created_Date { get; set; }

        /// <summary>
        /// The category id foreign key of the subcategory
        /// </summary>
        [Required]
        public int Category_Id { get; set; }

        /// <summary>
        /// The category of the subcategory
        /// </summary>
        [Required]
        public Category Category { get; set; }

        /// <summary>
        /// The user id foreign key of the subcategory
        /// </summary>
        [Required]
        public int User_Id { get; set; }

        /// <summary>
        /// The user who created the subcategory
        /// </summary>
        [Required]
        public User User { get; set; }
    }
}
