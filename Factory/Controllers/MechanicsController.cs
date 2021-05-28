using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;

namespace Factory.Controllers
{
  public class MechanicsController : Controller
  {
    private readonly FactoryContext _db;

    public MechanicsController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Mechanic> model = _db.Mechanics.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Mechanic mechanic)
    {
      _db.Mechanics.Add(mechanic);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisMechanic = _db.Mechanics
          .Include(mechanic => mechanic.JoinEntities)
          .ThenInclude(join => join.Machine)
          .FirstOrDefault(mechanic => mechanic.MechanicId == id);
      return View(thisMechanic);
    }
    public ActionResult Edit(int id)
    {
      var thisMechanic = _db.Mechanics.FirstOrDefault(mechanic => mechanic.MechanicId == id);
      return View(thisMechanic);
    }

    [HttpPost]
    public ActionResult Edit(Mechanic mechanic)
    {
      _db.Entry(mechanic).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisMechanic = _db.Mechanics.FirstOrDefault(mechanic => mechanic.MechanicId == id);
      return View(thisMechanic);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisMechanic = _db.Mechanics.FirstOrDefault(mechanic => mechanic.MechanicId == id);
      _db.Mechanics.Remove(thisMechanic);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}