#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kmabb.Models;


public class Comment
{

    [Key]
    public int CommentId { get; set; }

    [NoBadWords]
    [Required]
    [Display(Name ="Post a comment")]
    [MaxLength(255)]
    public string CommentText { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    // -------------- Nav and FK's

    [Required]
    public int UserId { get; set; }

    public User? Commentor { get; set; }

    [Required]
    public int MessageId { get; set; }

    public Message? Message  { get; set; }


}