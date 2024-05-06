using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Galbaat.Data
{
    public class GalbaatContext : DbContext
    {
        public GalbaatContext (DbContextOptions<GalbaatContext> options)
            : base(options)
        {
        }
        public DbSet<Post> Post{get;set;}
        public DbSet<User> User { get; set; }
    }
}
