#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kmabb.Models;


public class Message
{

    [Key]
    public int MessageId { get; set; }

    [Required]
    [Display(Name ="Post a message")]
    [MaxLength(255)]
    public string MessageText { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    // -------------- Nav and FK's

    [Required]
    public int UserId { get; set; }

    public User? Creator { get; set; }

    public List<Comment> Comments  { get; set; } = new List<Comment>();


}
