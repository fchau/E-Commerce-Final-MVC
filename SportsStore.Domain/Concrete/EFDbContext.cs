using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsStore.Domain.Entities;
using System.Data.Entity;

namespace SportsStore.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
       
        public DbSet<Product> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<States> States { get; set; }
    }
}
