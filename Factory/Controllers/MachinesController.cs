using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Factory.Models;

namespace Factory.Controllers
{
  public class MachinesController : Controller
  {
    private readonly FactoryContext _db;
    public MachinesController(FactoryContext db)
    {
      _db = db;
    }
    
    public ActionResult Index()
    {
      List<Location> locations = _db.Locations.ToList();
      ViewData.Add("locations", locations);
      List<Machine> model = _db.Machines.Include(machine => machine.Location).ToList();
      return View(_db.Machines.ToList());
    }

    public ActionResult Details(int id)
    {
      Machine thisMachine = _db.Machines
        .Include(machine => machine.JoinEntities)
        .ThenInclude(join => join.Engineer)
        .FirstOrDefault(Machine => Machine.MachineId == id);


        // Location thisLocation = _db.Locations
        // .Include(location => location.Machines)
        // .Include(location => location.Engineers)
        // .FirstOrDefault(location => location.LocationId == id);
        
      // int totalCompletedCount = 0;
      // foreach(Engineer engineer in thisLocation.Engineers)
      // {
      //   totalCompletedCount += engineer.MachinesCompletedCounter();
      // }
      // ViewBag.CompletedCount = totalCompletedCount;

      // _________________
      
      // List<Engineer> engineers = new List<Engineer>{};
      // foreach(EngineerMachine engineerMachine in thisMachine.JoinEntities)
      // {
      //   if (engineerMachine.EngineerId == _db.Engineer.EngineerId)
      //   {
      //     ???add engineer to engineers list???
      //   }
      // }
      // int totalCompletedCount = 0;
      // foreach(Engineer engineer in engineers)
      // {
      //   totalCompletedCount += engineer.MachinesCompletedCounter();
      // }
      // ViewBag.CompletedCount = totalCompletedCount;
      return View(thisMachine);
    }

    public ActionResult Create()
    {
      ViewBag.LocationId = new SelectList(_db.Locations, "LocationId", "LocationName");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Machine Machine)
    {
      _db.Machines.Add(Machine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
      ViewBag.LocationId = new SelectList(_db.Locations, "LocationId", "LocationName");
      Machine thisMachine = _db.Machines.FirstOrDefault(Machine => Machine.MachineId == id);
      return View(thisMachine);
    }

    [HttpPost]
    public ActionResult Edit(Machine Machine)
    {
      _db.Entry(Machine).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddEngineer(int id)
    {
      List<Engineer> engineers = _db.Engineers.ToList();
      ViewData.Add("engineers", engineers);
      var thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "Name");
      return View(thisMachine);
    }

    [HttpPost]
    public ActionResult AddEngineer(Machine machine, int EngineerId)
    {
      bool alreadyExists = _db.EngineerMachine.Any(engineerMachine => engineerMachine.MachineId == machine.MachineId && engineerMachine.EngineerId == EngineerId);
      if (EngineerId != 0 && !alreadyExists)
      {
        _db.EngineerMachine.Add(new EngineerMachine() { EngineerId = EngineerId, MachineId = machine.MachineId });
      }
      _db.SaveChanges();
      if (alreadyExists)
      {
        return RedirectToAction("AddEngineerError", new { id = machine.MachineId });
      }
      return RedirectToAction("Details", new { id = machine.MachineId });
    }

    public ActionResult AddEngineerError(int id)
    {
      var thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      return View(thisMachine);
    }

    [HttpPost]
    public ActionResult DeleteEngineer(int joinId)
    {
      var joinEntry = _db.EngineerMachine.FirstOrDefault(entry => entry.EngineerMachineId == joinId);
      _db.EngineerMachine.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = joinEntry.MachineId });
    }

    public ActionResult Delete(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(Machine => Machine.MachineId == id);
      return View(thisMachine);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(Machine => Machine.MachineId == id);
      _db.Machines.Remove(thisMachine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}