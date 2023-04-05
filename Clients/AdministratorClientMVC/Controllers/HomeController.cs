using Microsoft.AspNetCore.Mvc;

namespace AdministratorClientMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    [HttpGet()]
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
