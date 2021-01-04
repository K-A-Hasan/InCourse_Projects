using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Apple.Controllers
{
    public class ProductAndFeaturesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}