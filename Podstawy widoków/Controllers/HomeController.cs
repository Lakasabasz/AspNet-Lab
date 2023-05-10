using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Podstawy_widoków.Models;
using Podstawy_widoków.Services;
using Podstawy_widoków.ViewModels;

namespace Podstawy_widoków.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDatabaseInMemory _db;

    public HomeController(ILogger<HomeController> logger, ApplicationDatabaseInMemory db)
    {
        _logger = logger;
        _db = db;
    }

    public IActionResult Index()
    {
        return View();
    }

    public JsonResult PrintDb()
    {
        return new JsonResult(_db.Users.ToList());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}