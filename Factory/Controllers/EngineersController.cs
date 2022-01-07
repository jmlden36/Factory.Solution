using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Factory.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Factory.Controllers
{
  public class EngineersController : Controllers
  {
    private readonly FactoryContext _db;
    public EngineersController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Location> locations = _db.Locations.ToList();
      ViewData.Add("locations", locations);
      List<Engineer> model = _db.Engineers.Include(engineer => engineer.Location).ToList();
      return View(model);
    }
  }
}