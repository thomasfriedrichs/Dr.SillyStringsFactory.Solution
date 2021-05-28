using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;

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
      return View(_db.Machines.ToList());
    }

    public ActionResult Create()
    {
      ViewBag.MechanicId = new SelectList(_db.Mechanics, "MechanicId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Machine machine, int MechanicId)
    {
      _db.Machines.Add(machine);
      _db.SaveChanges();
      if (MechanicId != 0)
      {
        _db.MechanicMachine.Add(new MechanicMachine() { MechanicId = MechanicId, MachineId = machine.MachineId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisMachine = _db.Machines
          .Include(machine => machine.JoinEntities)
          .ThenInclude(join => join.Mechanic)
          .FirstOrDefault(machine => machine.MachineId == id);
      return View(thisMachine);
    }

    public ActionResult Edit(int id)
    {
      var thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      ViewBag.MechanicId = new SelectList(_db.Mechanics, "MechanicId", "Name");
      return View(thisMachine);
    }

    [HttpPost]
    public ActionResult Edit(Machine machine, int MechanicId)
    {
      if (MechanicId != 0)
      {
        _db.MechanicMachine.Add(new MechanicMachine() { MechanicId = MechanicId, MachineId = machine.MachineId });
      }
      _db.Entry(machine).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddMechanic(int id)
    {
      var thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      ViewBag.MechanicId = new SelectList(_db.Mechanics, "MechanicId", "Name");
      return View(thisMachine);
    }

    [HttpPost]
    public ActionResult AddMechanic(Machine machine, int MechanicId)
    {
      if (MechanicId != 0)
      {
      _db.MechanicMachine.Add(new MechanicMachine() { MechanicId = MechanicId, MachineId = machine.MachineId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      return View(thisMachine);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      _db.Machines.Remove(thisMachine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteMechanic(int joinId)
    {
      var joinEntry = _db.MechanicMachine.FirstOrDefault(entry => entry.MechanicMachineId == joinId);
      _db.MechanicMachine.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}