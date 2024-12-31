using Microsoft.AspNetCore.Mvc;
using WhileLagoon.Appliction.Common.Interfaces;
using WhileLagoon.Domian.Entities;
using WhileLagoon.infrastructur.Data;

namespace WhileLagoon.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaRepsitory villaRe;
        public VillaController(IVillaRepsitory villaRe)
        {
            this.villaRe = villaRe;
        }
        public IActionResult Index()
        {
            var villas=villaRe.GetAll();
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
                villaRe.Add(obj);
                villaRe.save();
                TempData["success"] = "The villa has been create successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int id)
        {
            Villa? obj=villaRe.Get(x => x.Id == id);
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
                villaRe.update(obj);
                villaRe.save();
                TempData["success"] = "The villa has been Edit successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            Villa? obj = villaRe.Get(x => x.Id == id);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
            Villa? villa = villaRe.Get(v => v.Id == obj.Id);
            if (villa is not null)
            {
               villaRe.Remove(villa);
                villaRe.save();
                TempData["success"] = "The villa has been delete successfully.";
                return RedirectToAction("Index");
            }
            TempData["error"] = "The villa could not be delete";
            return View();
        }
    }
}
