#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kmabb.Models;


public class QuoteViewModel
{

    public Quote Quote {get; set;} 
    public List<Quote> AllQuotes {get; set;} = new List<Quote>();
}