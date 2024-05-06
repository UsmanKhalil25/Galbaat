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
}
