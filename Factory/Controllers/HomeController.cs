using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Factory.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Factory.Controllers
{
  public class HomeController : Controller
  {

    private readonly FactoryContext _db;
    public HomeController(FactoryContext db)
    {
      _db = db;
    }

    [HttpGet("/")]
    public ActionResult Index()
    {
      List<Location> locations = _db.Locations.ToList();
      ViewData.Add("locations", locations);
      List<Engineer> engineers = _db.Engineers.ToList();
      ViewData.Add("engineers", engineers);
      List<Machine> machines = _db.Machines.ToList();
      ViewData.Add("machines", machines);
      return View();
    }
  }
}