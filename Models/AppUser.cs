using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Galbaat.Models;

public class AppUser : IdentityUser
{
    [StringLength(100)]
    [MaxLength(100)]
    [Required]
    public string? Name { get; set; }

    public ICollection<Post>? Posts { get; set; }
    public ICollection<UserFollow>? Followers { get; set; } // Collection of users who follow this user
    public ICollection<UserFollow>? Followeds { get; set; } // Collection of users whom this user follows
}
