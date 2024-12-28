using Microsoft.AspNetCore.Mvc;
using WhileLagoon.Domian.Entities;
using WhileLagoon.infrastructur.Data;

namespace WhileLagoon.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly CLSDbContext db;
        public VillaController(CLSDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var villas=db.Villas.ToList();
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
                db.Villas.Add(obj);
                db.SaveChanges();
                TempData["success"] = "The villa has been create successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int id)
        {
            Villa? obj=db.Villas.FirstOrDefault(x => x.Id == id);
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
                db.Villas.Update(obj);
                db.SaveChanges();
                TempData["success"] = "The villa has been Edit successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            Villa? obj = db.Villas.FirstOrDefault(x => x.Id == id);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
            Villa? villa = db.Villas.FirstOrDefault(v => v.Id == obj.Id);
            if (villa is not null)
            {
                db.Villas.Remove(villa);
                db.SaveChanges();
                TempData["success"] = "The villa has been delete successfully.";
                return RedirectToAction("Index");
            }
            TempData["error"] = "The villa could not be delete";
            return View();
        }
    }
}
