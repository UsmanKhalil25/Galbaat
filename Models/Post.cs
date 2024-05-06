using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        // Navigation property: Points back to the principal entity
        public AppUser? AppUser { get; set; }

    }
}