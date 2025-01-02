using Microsoft.AspNetCore.Mvc;
using System.Security.Permissions;
using WhileLagoon.Appliction.Common.Interfaces;
using WhileLagoon.Domian.Entities;
using WhileLagoon.infrastructur.Data;

namespace WhileLagoon.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IUnitOfWork unit;
        private readonly IWebHostEnvironment webHostEnvironment;
        public VillaController(IUnitOfWork unit , IWebHostEnvironment webHostEnvironment)
        {
            this.unit = unit;
            this.webHostEnvironment = webHostEnvironment;
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
                if(obj.Image != null)
                {
                    string fileName=Guid.NewGuid().ToString()+Path.GetExtension(obj.Image.FileName);
                    string imagePath=Path.Combine(webHostEnvironment.WebRootPath, @"Images\VillaImage");

                    using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                    obj.Image.CopyTo(fileStream);

                    obj.ImageUel = @"\Images\VillaImage\" + fileName;
                }
                else
                {
                    obj.ImageUel = "https://placehold.co/600x400";
                }
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
                if (obj.Image != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(obj.Image.FileName);
                    string imagePath = Path.Combine(webHostEnvironment.WebRootPath, @"Images\VillaImage");
                    if (!string.IsNullOrEmpty(obj.ImageUel))
                    {
                        var oldImagePath=Path.Combine(webHostEnvironment.ContentRootPath,obj.ImageUel.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                    obj.Image.CopyTo(fileStream);

                    obj.ImageUel = @"\Images\VillaImage\" + fileName;
                }
               
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
                if (obj.Image != null)
                {
                    
                    if (!string.IsNullOrEmpty(villa.ImageUel))
                    {
                        var oldImagePath = Path.Combine(webHostEnvironment.ContentRootPath, villa.ImageUel.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }


                   
                }
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
