using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StudentClientMVC.Models;

namespace StudentClientMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        try
        {
            return View("Index");
        }
        catch (System.Exception ex)
        {
             _logger.LogError(ex.Message);
            return View("Error");
        }
        
    }

}
