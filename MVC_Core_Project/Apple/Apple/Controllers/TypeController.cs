using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apple.Model;
using Apple.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Apple.Controllers
{
    public class TypeController : Controller
    {
        IProductType repo;
        public TypeController(IProductType repo) { this.repo = repo; }
        public IActionResult Index(int page = 1)
        {
            int size = 5;
            var data = repo.GetAll ()
                .ToPagedList(page, size);
            return View(data);
        }
        public IActionResult CreateAll()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateAll([FromBody]ProductType type)
        {
            if (ModelState.IsValid)
            {
                if (repo.Insert(type))
                    return Json(new { success = true });
                else
                    return Json(new { success = false });
            }
            else
            {
                return Json(new { success = false });
            }
        }
        public IActionResult Create(string postBack = "")
        {
            if (postBack != "")
            {
                ViewBag.PostBack = "Success";
            }
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductType pt)
        {
            if (ModelState.IsValid)
            {
                if (repo.Insert(pt))
                {
                    return RedirectToAction("Create", new { postBack = "postBack" });
                }
            }
            return View(pt);
        }
        public IActionResult MultipleEdit(int id)
        {
            var data = repo.GetWithChildren(id);
            if (data == null)
                return NotFound();
            return View(data);
        }
        [HttpPost]
        public IActionResult MultipleEdit([FromBody]ProductType t)
        {
            if (ModelState.IsValid)
            {
                repo.Update(t);
                return Json(new { success = true });

            }
            return Json(new { success = false });

        }
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                repo.Delete(id);
                return RedirectToAction("Index", new { postBack = "postBack" });

            }
            return View(id);

        }
    }
   
}