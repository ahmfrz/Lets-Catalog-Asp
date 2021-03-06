﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Models.Entities
{
    /// <summary>
    /// The brand entity to hold brand information
    /// </summary>
    public class Brand
    {
        /// <summary>
        /// The primary key of the brand
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// The name of the brand
        /// </summary>
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        /// <summary>
        /// The created date of the brand
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [ScaffoldColumn(false)]
        public DateTime Created_Date { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual SubCategory SubCategory { get; set; }
    }
}
