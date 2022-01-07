using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Factory.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Factory.Controllers
{
  public class EngineersController : Controller
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

    public ActionResult Create()
    {
      ViewBag.LocationId = new SelectList(_db.Locations, "LocationId", "LocationName");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Engineer engineer)
    {
      _db.Engineers.Add(engineer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
      ViewBag.LocationId = new SelectList(_db.Locations, "LocationId", "LocationName");
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      return View(thisEngineer);
    }

    [HttpPost]
    public ActionResult Edit(Engineer engineer)
    {
      _db.Entry(engineer).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = engineer.EngineerId });
    }

    public ActionResult Details(int id)
    {
      var thisEngineer = _db.Engineers
        .Include(engineer => engineer.JoinEntities)
        .ThenInclude(join => join.Machine)
        .FirstOrDefault(Engineer => Engineer.EngineerId == id);
      return View(thisEngineer);
    }

    public ActionResult AddMachine(int id)
    {
      List<Machine> machines = _db.Machines.ToList();
      ViewData.Add("machines", machines);
      var thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Name");
      return View(thisEngineer);
    }

    [HttpPost]
    public ActionResult AddMachine(Engineer engineer, int MachineId)
    {
      bool alreadyExists = _db.EngineerMachine.Any(engineerMachine => engineerMachine.EngineerId == engineer.EngineerId && engineerMachine.MachineId == MachineId);
      if (MachineId != 0 && !alreadyExists)
      {
        _db.EngineerMachine.Add(new EngineerMachine() { MachineId = MachineId, EngineerId = engineer.EngineerId });
      }
      _db.SaveChanges();
      if (alreadyExists)
      {
        return RedirectToAction("AddMachineError", new { id = engineer.EngineerId });
      }
      return RedirectToAction("Details", new { id = engineer.EngineerId });
    }

    public ActionResult AddMachineError(int id)
    {
      var thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      return View(thisEngineer);
    }

    [HttpPost]
    public ActionResult SwitchCompleted(int joinId)
    {
      var joinEntry = _db.EngineerMachine.FirstOrDefault(entry => entry.EngineerMachineId == joinId);
      joinEntry.Complete = !joinEntry.Complete;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = joinEntry.EngineerId });
    }

    public ActionResult CompletedDetails(int id)
    {
      var thisEngineer = _db.Engineers
        .Include(engineer => engineer.JoinEntities)
        .ThenInclude(join => join.Machine)
        .FirstOrDefault(Engineer => Engineer.EngineerId == id);
      return View(thisEngineer);
    }

    public ActionResult Delete(int id)
    {
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      return View(thisEngineer);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      _db.Engineers.Remove(thisEngineer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}