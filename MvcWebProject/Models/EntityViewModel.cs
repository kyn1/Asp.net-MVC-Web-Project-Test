using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcWebProject.Models
{
    /// <summary>
    /// Represents a view model for an entity.
    /// </summary>
    public class EntityViewModel
    {
        /// <summary>
        /// Gets or sets the name of the entity.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the surname of the entity.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the age of the entity.
        /// </summary>
        public int Age { get; set; }
    }
}
