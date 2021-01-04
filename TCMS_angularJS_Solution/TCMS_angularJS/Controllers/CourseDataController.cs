﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TCMS_angularJS.Models;

namespace TCMS_angularJS.Controllers
{
    public class CourseDataController : Controller
    {
        CourseDbContext db;
        public CourseDataController(CourseDbContext db) { this.db = db; }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetTradesForDropDown()
        {
            return Json(db.Trades.ToList());
        }
        [HttpGet]
        public IActionResult GetCourses()
        {
            return Json(db.Courses.ToList());
        }
        [HttpGet]
        public IActionResult GetCourseById(int id)
        {
            return Json(db.Courses.First(x => x.CourseId == id));
        }
        [HttpPost]
        public IActionResult InsertCourse([FromBody]Course c)
        {
            db.Courses.Add(c);
            db.SaveChanges();
            return Json(c);
        }
        [HttpPut]
        public IActionResult UpdateCourse(int id, [FromBody] Course c)
        {
            var original = db.Courses.First(x => x.CourseId == id);
            original.CourseName = c.CourseName;
            original.Duration = c.Duration;
            original.TradeId = c.TradeId;
            db.SaveChanges();
            return Json(c);
        }
        [HttpDelete]
        public IActionResult DeleteCourse(int id)
        {
            var original = db.Courses.First(x => x.CourseId == id);
            db.Remove(original);
            db.SaveChanges();
            return Json(original);
        }
    }
}