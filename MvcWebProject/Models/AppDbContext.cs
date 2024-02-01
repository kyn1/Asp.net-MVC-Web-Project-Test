using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcWebProject.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Entity> Entities { get; set; }
    }
}