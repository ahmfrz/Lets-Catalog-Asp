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
    /// The category entity to hold category information
    /// </summary>
    public class Category
    {
        /// <summary>
        /// The primary key of the category
        /// </summary>
        [Key]
        public int ID { get; set; }

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
        [DisplayFormat(DataFormatString ="dd-mm-yyyy")]
        [ScaffoldColumn(false)]
        public DateTime Created_Date { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual ICollection<SubCategory> SubCategories { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual User User { get; set; }

        ///// <summary>
        ///// The user id foreign key of the category
        ///// </summary>
        //[Required]
        //[ScaffoldColumn(false)]
        //public int User_Id { get; set; }

        ///// <summary>
        ///// The user who created the category
        ///// </summary>
        //[Required]
        //[ScaffoldColumn(false)]
        //public User User { get; set; }
    }
}
