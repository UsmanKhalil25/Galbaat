using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

public class User : IdentityUser
{
    public ICollection<Post> Posts { get; set; }
}
