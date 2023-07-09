using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RGR.Models;

public partial class Post
{

    private readonly DbForRgrContext db = new();


    public int IdPost { get; set; }

    public byte[]? Image { get; set; }

    [Required]
    [StringLength(2000, MinimumLength = 5, ErrorMessage = "The field must be between 5 and 2000 characters.")]
    public string? Description { get; set; }

    public int? IdAccount { get; set; }

    public DateTime? Time { get; set; }


    //Navigation dont work
    public virtual Account? IdAccountNavigation { get; set; } = null!;

    public string getName()
    {
        return db.Accounts.FirstOrDefault(a => a.IdAccount == IdAccount).Nickname;
    }
    
    public byte[] getAvatarka()
    {
        if (db.Accounts.FirstOrDefault(a => a.IdAccount == IdAccount).Avatarka != null)
        {
            return db.Accounts.FirstOrDefault(a => a.IdAccount == IdAccount).Avatarka;
        }
        else
        {
            return null;
        }
    }
}
