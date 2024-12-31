using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            var villanumber=db.VillaNubmers.Include(u=>u.Villa).ToList();
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
        public IActionResult Create(VillaNumberVM obj)
        {
            bool roomNumberExists=db.VillaNubmers.Any(u=>u.Villa_Number == obj.VillaNumber.Villa_Number);
            
            if (ModelState.IsValid &&!roomNumberExists)
            {
                db.VillaNubmers.Add(obj.VillaNumber);
                db.SaveChanges();
                TempData["success"] = "The villa number has been create successfully.";
                return RedirectToAction(nameof(Index));
            }
            if (roomNumberExists)
            {
                TempData["error"] = "The villa number already exists.";
            }
            obj.VillaList = db.Villas.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString(),

            });
            return View(obj);
        }
        public IActionResult Edit(int id)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList=db.Villas.ToList().Select(u=>new SelectListItem
                {
                    Text=u.Name,
                    Value=u.Id.ToString(),
                })
               ,VillaNumber=db.VillaNubmers.FirstOrDefault(u=>u.Villa_Number==id)
            };
            //Villa? obj = db.Villas.FirstOrDefault(x => x.Id == id);
            if (villaNumberVM.VillaNumber == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumberVM);
        }
        [HttpPost]
        public IActionResult Edit(VillaNumberVM obj)
        {
            
            if (ModelState.IsValid )
            {
                db.VillaNubmers.Update(obj.VillaNumber);
                db.SaveChanges();
                TempData["success"] = "The villa number has been create successfully.";
                return RedirectToAction(nameof(Index));             

);
            }
           
            obj.VillaList = db.Villas.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString(),

            });
            return View(obj);
        }
        public IActionResult Delete(int id)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = db.Villas.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                })
               ,
                VillaNumber = db.VillaNubmers.FirstOrDefault(u => u.Villa_Number == id)
            };
            
            if (villaNumberVM.VillaNumber == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumberVM);
        }
        [HttpPost]
        public IActionResult Delete(VillaNumberVM obj)
        {
            VillaNumber? villa = db.VillaNubmers.
                FirstOrDefault(v => v.Villa_Number == obj.VillaNumber.Villa_Number);
            if (villa is not null)
            {
                db.VillaNubmers.Remove(villa);
                db.SaveChanges();
                TempData["success"] = "The villa has been delete successfully.";
                return RedirectToAction(nameof (Index));
            }
            TempData["error"] = "The villa could not be delete";
            return View();
        }
    }
}
