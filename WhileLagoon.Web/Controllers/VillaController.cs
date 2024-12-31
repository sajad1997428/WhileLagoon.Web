using Microsoft.AspNetCore.Mvc;
using WhileLagoon.Appliction.Common.Interfaces;
using WhileLagoon.Domian.Entities;
using WhileLagoon.infrastructur.Data;

namespace WhileLagoon.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IUnitOfWork unit;
        public VillaController(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        public IActionResult Index()
        {
            var villas=unit.Villa.GetAll();
            return View(villas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Villa obj)
        {
            if(obj.Name==obj.Description)
            {
                ModelState.AddModelError("", "The description cannot exactly match the Name");
            }
            if(ModelState.IsValid)
            {
                unit.Villa.Add(obj);
                unit.save();
                TempData["success"] = "The villa has been create successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int id)
        {
            Villa? obj= unit.Villa.Get(x => x.Id == id);
            if(obj==null)
            {
                return RedirectToAction("Error","Home");
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(Villa obj)
        {
           
            if (ModelState.IsValid &&  obj.Id>0)
            {
                unit.Villa.update(obj);
                unit.save();
                TempData["success"] = "The villa has been Edit successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            Villa? obj = unit.Villa.Get(x => x.Id == id);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
            Villa? villa = unit.Villa.Get(v => v.Id == obj.Id);
            if (villa is not null)
            {
                unit.Villa.Remove(villa);
                unit.save();
                TempData["success"] = "The villa has been delete successfully.";
                return RedirectToAction("Index");
            }
            TempData["error"] = "The villa could not be delete";
            return View();
        }
    }
}
