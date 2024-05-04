using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Post
{
    public int Id { get; set; }
    public required string Content { get; set; }
    public DateTime Timestamp { get; set; }   
    [Required]
    public string UserId { get; set; }
    public User User {get;set;}
}
