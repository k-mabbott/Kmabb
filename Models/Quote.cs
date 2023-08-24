#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kmabb.Models;


public class Quote
{

    [Key]
    public int QuoteId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [Required]
    [MaxLength(255)]
    [EmailAddress]
    public string Email { get; set; }

    [Required (ErrorMessage="The Phone field is required")]
    [MinLength(10, ErrorMessage="Must be a valid phone number with areacode")]
    [MaxLength(18, ErrorMessage="Must be a valid phone number with areacode")]
    [RegularExpression(@"^(\+\d{1,2}\s?)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$", ErrorMessage = "Invalid Phone Number")]
    public string Phone { get; set; }

    [Required]
    [Display(Name="City / Town")]
    [MaxLength(45)]
    public string City { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Message { get; set; }

    public bool Replied { get; set; }

    public bool Completed { get; set; }

    [Display(Name="Start Date and Time:")]
    public DateTime StartDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

}