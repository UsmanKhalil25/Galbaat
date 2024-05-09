using System.ComponentModel.DataAnnotations;
using Galbaat.Models;
namespace Galbaat.ViewModels;

public class UserDetailsViewModel
{
    public AppUser AppUser { get; set; } 
    public List<Post> Posts { get; set; } 
}
