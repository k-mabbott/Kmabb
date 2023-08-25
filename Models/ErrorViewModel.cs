namespace Kmabb.Models;
using System.ComponentModel.DataAnnotations;
#pragma warning disable CS8618
using System.Text.RegularExpressions;

public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}

public class NoBadWords : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        Console.WriteLine(value);

        if (value == null)
        {
            return new ValidationResult("Can not be empty");
        }

        var badWords = new[] { "fuck", "shit", "bitch", "fag", "nigger", "asshole", "cunt" };
        if (CheckText((string)value, badWords))
        {
            return new ValidationResult("No Profanity Please...");
        }
        return ValidationResult.Success;
        
        bool CheckText(string content, string[] badWords)
        {
            foreach (var badWord in badWords)
            {
                var regex = new Regex("(^|[\\?\\.,\\s])" + badWord + "([\\?\\.,\\s]|$)");
                if (regex.IsMatch(content.ToLower())) return true;
            }
            return false;
        }
    }
}
