using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Galbaat.Models
{
    public class UserFollow
    {   
        public int Id { get; set; }
        [Required]
        public string FollowerId { get; set; }
        public AppUser? Follower { get; set; }
        [Required]
        public string FollowedId { get; set; }
        public AppUser? Followed { get; set; }
    }
}