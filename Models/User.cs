using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

public class GalbaatUser : IdentityUser
{
    public ICollection<Post> Posts { get; set; }
}
