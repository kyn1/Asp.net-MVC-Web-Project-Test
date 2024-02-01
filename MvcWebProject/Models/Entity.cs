using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcWebProject.Models
{
    /// <summary>
    /// Represents an entity with properties for identification, name, surname, and age.
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Gets or sets the unique identifier of the entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the entity.
        /// </summary>
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the surname of the entity.
        /// </summary>
        [Required(ErrorMessage = "Surname is required!")]
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the age of the entity.
        /// </summary>
        [Required(ErrorMessage = "Age required! ")]
        public int Age { get; set; }
    }

}
