using Covid_19.Models;
using Covid_19.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Covid_19.Controllers
{
    [Authorize]
    public class CountriesAndCasesController : Controller
    {
        DataRepository repo = new DataRepository();
        // GET: CountriesAndCases
        [AllowAnonymous]
        public ActionResult Index()
        {
            var data = repo.GetAllRelated();
            return View(data);
        }
        public ActionResult Add()
        {
            ViewBag.Regions = repo.GetRegionsForDropdown();
            return View();
        }
        [HttpPost]
        public JsonResult AddPost(Country c)
        {
            if (ModelState.IsValid)
            {
                repo.AddCountriesAndCases(c);
                return Json(new { success = true, message = "Data saved successfully" });
            }
            return Json(new { success = false, message = "Data save failed" });

        }
    }
}