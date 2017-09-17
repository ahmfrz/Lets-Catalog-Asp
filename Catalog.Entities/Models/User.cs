using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Models.Entities
{
    /// <summary>
    /// The user entity to hold user information
    /// </summary>
    public class User
    {
        /// <summary>
        /// The primary key of the user
        /// </summary>
        [Key]
        public int User_Id { get; set; }

        /// <summary>
        /// The name of the user
        /// </summary>
        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        /// <summary>
        /// The email address of the user
        /// </summary>
        [Required]
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// The profile picture url of the user
        /// </summary>
        [Display(Name="Picture URL")]
        [DataType(DataType.ImageUrl)]
        public string Picture_URL { get; set; }
    }
}
