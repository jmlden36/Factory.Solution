using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Factory.Models;

namespace Factory.Controllers
{
  public class LocationsController : Controller
  {
    private readonly FactoryContext _db;
    public LocationsController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Locations.ToList());
    }

    public ActionResult Details(int id)
    {
      Location thisLocation = _db.Locations
        .Include(location => location.Machines)
        .Include(location => location.Engineers)
        .FirstOrDefault(location => location.LocationId == id);
      int totalCompletedCount = 0;
      foreach(Engineer engineer in thisLocation.Engineers)
      {
        totalCompletedCount += engineer.MachinesCompletedCounter();
      }
      ViewBag.CompletedCount = totalCompletedCount;
      return View(thisLocation);
    }
    
    public ActionResult Create()
    {
      return View();
    }
    
    [HttpPost]
    public ActionResult Create(Location location)
    {
      _db.Locations.Add(location);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Edit (int id)
    {
      Location thisLocation = _db.Locations.FirstOrDefault(location => location.LocationId ==id);
      return View(thisLocation);
    }

    [HttpPost]
    public ActionResult Edit(Location location)
    {
      _db.Entry(location).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Location thisLocation = _db.Locations.FirstOrDefault(location => location.LocationId == id);
      return View(thisLocation);
    }



    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Location thisLocation = _db.Locations.FirstOrDefault(location => location.LocationId == id);
      _db.Locations.Remove(thisLocation);
      _db.SaveChanges();
      return RedirectToAction("Index"); 
    }
  }
}