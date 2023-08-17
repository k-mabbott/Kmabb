using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Kmabb.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Kmabb.Controllers;

[SessionCheck]
public class MessageController : Controller
{
    private readonly ILogger<MessageController> _logger;

    private MyContext DB;


    public MessageController(ILogger<MessageController> logger, MyContext context)
    {
        _logger = logger;
        DB = context;
    }


    // -----------------------------INDEX PAGE
    [HttpGet("messages")]
    public IActionResult Index()
    {

        MessageViewModel MessagesModel = new MessageViewModel();

        MessagesModel.Messages = DB.Messages.OrderByDescending(m => m.CreatedAt).Include(a => a.Creator).Include(m => m.Comments).ThenInclude(c => c.Commentor).ToList();
        // MessagesModel.Messages = DB.Messages.OrderByDescending(m => m.CreatedAt).Include(a => a.Creator).Include(m => m.Comments).ThenInclude(c => c.Commentor).OrderByDescending(m => m.CreatedAt).ToList();

        return View(MessagesModel);
    }

    [HttpPost("messages/create")]
    public IActionResult CreateMessage(Message newMessage)
    {
        if (!ModelState.IsValid)
        {
            MessageViewModel MessagesModel = new MessageViewModel();
            MessagesModel.Messages = DB.Messages.Include(a => a.Creator).Include(m => m.Comments).ThenInclude(c => c.Commentor).ToList();
            return View("Index", MessagesModel);
        };
        int? UID = HttpContext.Session.GetInt32("UserId");
        if(UID != null)
        {
            newMessage.UserId = (int)UID;
            DB.Messages.Add(newMessage);
            DB.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    [HttpPost("messages/{mId}/destroy")]
    public IActionResult DeleteMessage(int mId)
    {
        Message? exisiting = DB.Messages.FirstOrDefault( m => m.MessageId == mId);
        if(exisiting != null)
        {
            DB.Messages.Remove(exisiting);
            DB.SaveChanges();
        }
        return RedirectToAction("Index");
    }

        // -----------------------------Create Comment
    [HttpPost("comment/create/{mId}")]
    public IActionResult CreateComment(Comment newComment, int mId)
    {
        if(!ModelState.IsValid)
        {
            MessageViewModel MessagesModel = new MessageViewModel();
            MessagesModel.Messages = DB.Messages.Include(a => a.Creator).Include(m => m.Comments).ThenInclude(c => c.Commentor).ToList();
            return View("Index", MessagesModel);
        }
        int? UID = HttpContext.Session.GetInt32("UserId");
        if (UID != null)
        {
            newComment.UserId = (int)UID;
            newComment.MessageId = mId;
            DB.Comments.Add(newComment);
            DB.SaveChanges();
        }
        return RedirectToAction("Index", "Message");
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}







public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        int? userId = context.HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            context.Result = new RedirectToActionResult("Index", "User", null);
        }
    }
}