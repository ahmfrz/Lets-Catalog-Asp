using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Models.Entities
{
    public class ActionLog
    {
        /// <summary>
        /// The primary key of the ActionLog
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime Time { get; set; }
    }
}
