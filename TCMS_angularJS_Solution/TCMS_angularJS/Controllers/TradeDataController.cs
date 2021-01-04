using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TCMS_angularJS.Models;

namespace TCMS_angularJS.Controllers
{
    [Produces("application/json")]
    public class TradeDataController : Controller
    {
        CourseDbContext db;
        public TradeDataController(CourseDbContext db) { this.db = db; }
        public IActionResult Index()
        {
            return View();
        }
       
        [HttpGet]
        public IActionResult TradesWithCourse()
        {
            var data = db.Trades.Include(x=> x.Courses).ToList();
           
            return Json(data);

        }
        [HttpGet]
        public IActionResult TradesWithCourseById(int id)
        {
            var data = db.Trades.Include(x => x.Courses).First(x=> x.TradeId== id);

            return Json(data);

        }
        [HttpPost]
        public IActionResult InsertTradesWithCourse([FromBody]Trade t)
        {
            db.Trades.Add(t);
            db.SaveChanges();

            return Json(t);

        }
        [HttpPut]
        public IActionResult UpdateTradesWithCourse(int id,[FromBody]Trade t)
        {
            var original = db.Trades.Include(x => x.Courses).First(x => x.TradeId == id);
            original.TradeName = t.TradeName;
            original.Description = t.Description;
            original.FemaleAllowed = t.FemaleAllowed;
            if (t.Courses != null && t.Courses.Count > 0)
            {
                var crs = t.Courses.ToArray();
                for (var i = 0; i < crs.Length; i++)
                {
                    var temp = original.Courses.FirstOrDefault(c => c.CourseId == crs[i].CourseId);
                    if (temp != null)
                    {
                        temp.CourseName = crs[i].CourseName;
                        temp.Duration = crs[i].Duration;
                    }
                    else
                    {
                        original.Courses.Add(crs[i]);
                    }
                }
                crs = original.Courses.ToArray();
                for (var i = 0; i < crs.Length; i++)
                {
                    var temp = t.Courses.FirstOrDefault(x => x.CourseId == crs[i].CourseId);
                    if (temp == null)
                        db.Entry(crs[i]).State = EntityState.Deleted;
                }
            }
    
            db.SaveChanges();

            return Json(t);

        }
        [HttpDelete]
        public IActionResult DeleteTrade(int id)
        {
            var original = db.Trades.First(x => x.TradeId == id);
            db.Trades.Remove(original);
            db.SaveChanges();
            return Json(original);
        }
    }
}