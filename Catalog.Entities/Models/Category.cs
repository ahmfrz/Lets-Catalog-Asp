using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Models.Entities
{
    /// <summary>
    /// The category entity to hold category information
    /// </summary>
    public class Category
    {
        /// <summary>
        /// The primary key of the category
        /// </summary>
        [Key]
        public int Category_Id { get; set; }

        /// <summary>
        /// The name of the category
        /// </summary>
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        /// <summary>
        /// The created date of the category
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        public DateTime Created_Date { get; set; }

        /// <summary>
        /// The user id foreign key of the category
        /// </summary>
        [Required]
        public int User_Id { get; set; }

        /// <summary>
        /// The user who created the category
        /// </summary>
        [Required]
        public User User { get; set; }
    }
}
