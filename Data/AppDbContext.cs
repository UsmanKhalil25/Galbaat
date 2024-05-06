using Galbaat.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Galbaat.Data;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<Galbaat.Models.AppUser> AppUser {get;set;}
    public DbSet<Galbaat.Models.Post> Post { get; set; } 
}
