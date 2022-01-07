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
      ViewBag.LocationId = new SelectList(_db.Locations, "LocationId", "Location");
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
      ViewBag.LocationId = new SelectList(_db.Locations, "LocationId", "Location");
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