using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RGR.Models;

public partial class Account
{
    public int IdAccount { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "The field must be between 5 and 50 characters.")]
    public string Nickname { get; set; } = null!;

    [Required]
    [StringLength(30, MinimumLength = 5, ErrorMessage = "The field must be between 5 and 30 characters.")]
    public string Password { get; set; } = null!;

    [Required]
    [StringLength(40, MinimumLength = 5, ErrorMessage = "The field must be between 5 and 40 characters.")]
    public string Email { get; set; } = null!;

    public byte[]? Avatarka { get; set; }

    [StringLength(4000, MinimumLength = 10, ErrorMessage = "Description must be between 5 and 40 characters.")]
    public string? Description { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
