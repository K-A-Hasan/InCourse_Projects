using Covid_19.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Covid_19.Controllers
{
    public class CountriesController : Controller
    {
        CoronaDbContext db = new CoronaDbContext();
        // GET: Countries
        public ActionResult Index()
        {

            var data = db.Regions.Include(t => t.Countries.Select(q => q.CoronaCases));
            return View(data);
        }
        public ActionResult Cases()
        {
            var p = db.CoronaCases.Include(x => x.Country);
            return View(p);
        }
        public ActionResult Add()
        {
            
            ViewBag.Countries = db.Countries.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Add(Country C)
        {
            if (ModelState.IsValid)
            {
                db.Countries.Add(C);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Countries = db.Countries.ToList();
            return View(C);
        }
    }
}