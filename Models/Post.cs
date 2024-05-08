using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Galbaat.Data;
using Microsoft.EntityFrameworkCore;

namespace Galbaat.Models
{
    public class Post
    {

        public int Id { get; set; }

        [Required]
        public string Content {get;set;}
        public DateTime TimeStamp { get; set; }
        [Required]
        public string AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public ICollection<Like>? Likes {get;set;}

        
    }
}