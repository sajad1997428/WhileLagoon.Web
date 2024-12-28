using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WhileLagoon.Domian.Entities;
using WhileLagoon.infrastructur.Data;
using WhileLagoon.Web.ViewModel;

namespace WhileLagoon.Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly CLSDbContext db;
        public VillaNumberController(CLSDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var villanumber=db.VillaNubmers.ToList();
            return View(villanumber);
        }
        public IActionResult Create()
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = db.Villas.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),

                })

            };
            
            return View(villaNumberVM);
        }
        [HttpPost]
        public IActionResult Create(VillaNumber obj)
        {
            
            if (ModelState.IsValid)
            {
                db.VillaNubmers.Add(obj);
                db.SaveChanges();
                TempData["success"] = "The villa number has been create successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int id)
        {
            Villa? obj = db.Villas.FirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(Villa obj)
        {

            if (ModelState.IsValid && obj.Id > 0)
            {
                db.Villas.Update(obj);
                db.SaveChanges();
                TempData["success"] = "The villa has been Edit successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
