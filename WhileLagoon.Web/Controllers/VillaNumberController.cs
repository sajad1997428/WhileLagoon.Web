using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WhileLagoon.Appliction.Common.Interfaces;
using WhileLagoon.Domian.Entities;
using WhileLagoon.infrastructur.Data;
using WhileLagoon.Web.ViewModel;

namespace WhileLagoon.Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IUnitOfWork unit;
        public VillaNumberController(IUnitOfWork unit)
        {
            this.unit=unit;
        }
        public IActionResult Index()
        {
            var villanumber=unit.VillaNumber.GetAll(indcludeProerties:"Villa");
            return View(villanumber);
        }
        public IActionResult Create()
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = unit.Villa.GetAll().Select(u => new SelectListItem
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
            bool roomNumberExists=unit.VillaNumber.Any(u=>u.Villa_Number == obj.VillaNumber.Villa_Number);
            
            if (ModelState.IsValid &&!roomNumberExists)
            {
                unit.VillaNumber.Add(obj.VillaNumber);
                unit.save();
                TempData["success"] = "The villa number has been create successfully.";
                return RedirectToAction(nameof(Index));
            }
            if (roomNumberExists)
            {
                TempData["error"] = "The villa number already exists.";
            }
            obj.VillaList = unit.Villa.GetAll().Select(u => new SelectListItem
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
                VillaList=unit.Villa.GetAll().Select(u=>new SelectListItem
                {
                    Text=u.Name,
                    Value=u.Id.ToString(),
                })
               ,VillaNumber=unit.VillaNumber.Get(u=>u.Villa_Number==id)
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
                unit.VillaNumber.update(obj.VillaNumber);
                unit.save();
                TempData["success"] = "The villa number has been create successfully.";
                return RedirectToAction(nameof(Index));             


            }
           
            obj.VillaList = unit.Villa.GetAll().Select(u => new SelectListItem
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
                VillaList = unit.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                })
               ,
                VillaNumber = unit.VillaNumber.Get(u => u.Villa_Number == id)
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
            VillaNumber? villa = unit.VillaNumber.
                Get(v => v.Villa_Number == obj.VillaNumber.Villa_Number);
            if (villa is not null)
            {
               unit.VillaNumber.Remove(villa);
                unit.save();
                TempData["success"] = "The villa has been delete successfully.";
                return RedirectToAction(nameof (Index));
            }
            TempData["error"] = "The villa could not be delete";
            return View();
        }
    }
}
