using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Kmabb.Models;

namespace Kmabb.Controllers;

public class CommentController : Controller
{
    private readonly ILogger<CommentController> _logger;

    private MyContext DB;


    public CommentController(ILogger<CommentController> logger, MyContext context)
    {
        _logger = logger;
        DB = context;
    }


    // // -----------------------------Create Comment
    // [HttpPost("comment/create/{mId}")]
    // public IActionResult CreateComment(Comment newComment, int mId)
    // {
    //     if(!ModelState.IsValid)
    //     {
    //         return RedirectToAction("Index", "Message");
    //     }
    //     int? UID = HttpContext.Session.GetInt32("UserId");
    //     if (UID != null)
    //     {
    //         newComment.UserId = (int)UID;
    //         newComment.MessageId = mId;
    //         DB.Comments.Add(newComment);
    //         DB.SaveChanges();
    //     }
    //     return RedirectToAction("Index", "Message");
    // }

    [HttpPost("comments/{cId}/destroy")]
    public IActionResult DeleteComment(int cId)
    {
        Comment? exisiting = DB.Comments.FirstOrDefault(c => c.CommentId == cId);
        if (exisiting != null)
        {
            DB.Comments.Remove(exisiting);
            DB.SaveChanges();
        }
        return RedirectToAction("Index", "Message");
    }

    // public IActionResult Privacy()
    // {
    //     return View();
    // }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
