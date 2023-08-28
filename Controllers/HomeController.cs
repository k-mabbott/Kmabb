using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Kmabb.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.IO.Compression;

namespace Kmabb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private MyContext DB;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        DB = context;
    }
// -----------------------------------Home Page
    [HttpGet("/")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("/photos")]
    public IActionResult PhotoGallery()
    {
        return View();
    }
// ----------------------------------- View Quotes
    [SessionCheck]
    [HttpGet("/quotes")]
    public IActionResult Quotes()
    {
        QuoteViewModel QuoteModel = new QuoteViewModel();
        QuoteModel.AllQuotes = DB.Quotes.OrderByDescending(q => q.CreatedAt).ToList();
        return View(QuoteModel);
    }
// ----------------------------------- Create Quote / POST
    [HttpPost("/create/quote")]
    public IActionResult CreateQuote(Quote newQuote)
    {
        if (!ModelState.IsValid)
        {
            return View("Index");
        }

        DB.Quotes.Add(newQuote);
        DB.SaveChanges();

        return RedirectToAction("Index");
    }
// ----------------------------------- Update Quote / POST
    [HttpPost("/update/{qID}/quote/")]
    public IActionResult UpdateQuote(Quote editQuote, int qID)
    {
        Quote? existingQuote = DB.Quotes.FirstOrDefault(q => q.QuoteId == qID);
        if (existingQuote == null)
        {
            return RedirectToAction("Quotes");
        }
        Console.WriteLine("*******************"+existingQuote.Message+"*******************");
        
        existingQuote.Replied = editQuote.Replied;
        existingQuote.Completed = editQuote.Completed;
        existingQuote.StartDate = editQuote.StartDate;

        DB.Quotes.Update(existingQuote);
        DB.SaveChanges();

        return RedirectToAction("Quotes");
    }

// ----------------------------------- Currently not used
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
