using Covid_19.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Covid_19.Controllers
{
    public class HomeController : Controller
    {
        CoronaDbContext db = new CoronaDbContext();
        // GET: Home
        public ActionResult Index()
        {
            return View(db.Regions.ToList());
        }
    }
}