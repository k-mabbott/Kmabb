using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Kmabb.Models;

namespace Kmabb.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private MyContext DB;


    public UserController(ILogger<UserController> logger, MyContext context)
    {
        _logger = logger;
        DB = context;
    }
    // -----------------------------INDEX PAGE
    [HttpGet("login/register")]
    public IActionResult Index()
    {
        return View();
    }

    // ----------------------------- CREATE USER
    [HttpPost("users/create")]
    public IActionResult Create(User newUser)
    {
        if (!ModelState.IsValid)
        {
            return View("Index");
        }
        PasswordHasher<User> Hasher = new PasswordHasher<User>();
        newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
        DB.Add(newUser);
        DB.SaveChanges();
        HttpContext.Session.SetInt32("UserId", newUser.UserId);
        HttpContext.Session.SetString("UserName", newUser.FirstName);
        return RedirectToAction("Index", "Message");
    }

    // ----------------------------- LOGIN USER
    [HttpPost("users/login")]
    public IActionResult Login(LoginUser userSubmission)
    {
        if (!ModelState.IsValid)
        {
            return View("Index");
        }

        User? userInDb = DB.Users.FirstOrDefault(u => u.Email == userSubmission.LoginEmail);

        if (userInDb == null)
        {
            ModelState.AddModelError("LoginEmail", "Invalid Credentials");
            return View("Index");
        }
        PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
        var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.LoginPassword);
        if (result == 0)
        {
            ModelState.AddModelError("LoginEmail", "Invalid Credentials");
            return View("Index");
        }
        HttpContext.Session.SetInt32("UserId", userInDb.UserId);
        HttpContext.Session.SetString("UserName", userInDb.FirstName);
        return RedirectToAction("Index", "Message");
    }


    // ----------------------------- LOGOUT USER
    [HttpPost("users/logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }





    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}