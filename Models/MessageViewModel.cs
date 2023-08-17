#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kmabb.Models;


public class MessageViewModel
{
    public List<Message> Messages {get; set;} = new List<Message>();

    public Comment Comment {get; set;} 
    public Message Message {get; set;} 
}