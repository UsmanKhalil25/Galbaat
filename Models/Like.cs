using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Galbaat.Models
{
    public class Like
    {   
        public int Id { get; set; }
        [Required]
        public string AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        [Required]
        public int PostId { get; set; }
        public Post? Post { get; set; }
    }
}