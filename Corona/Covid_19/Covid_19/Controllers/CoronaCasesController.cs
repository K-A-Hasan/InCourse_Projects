using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Covid_19.Models;

namespace Covid_19.Controllers
{
    [Authorize]
    public class CoronaCasesController : Controller
    {
        private CoronaDbContext db = new CoronaDbContext();

        // GET: CoronaCases
        [AllowAnonymous]
        public ActionResult Index()
        {
            var coronaCases = db.CoronaCases.Include(c => c.Country);
            return View(coronaCases.ToList());
        }

        // GET: CoronaCases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoronaCase coronaCase = db.CoronaCases.Find(id);
            if (coronaCase == null)
            {
                return HttpNotFound();
            }
            return View(coronaCase);
        }

        // GET: CoronaCases/Create
        public ActionResult Create()
        {
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "Country_Name");
            return View();
        }

        // POST: CoronaCases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CoronaCaseId,Date_reported,New_Cases,New_deaths,CountryId")] CoronaCase coronaCase)
        {
            if (ModelState.IsValid)
            {
                db.CoronaCases.Add(coronaCase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "Country_Name", coronaCase.CountryId);
            return View(coronaCase);
        }

        // GET: CoronaCases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoronaCase coronaCase = db.CoronaCases.Find(id);
            if (coronaCase == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "Country_Name", coronaCase.CountryId);
            return View(coronaCase);
        }

        // POST: CoronaCases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CoronaCaseId,Date_reported,New_Cases,New_deaths,CountryId")] CoronaCase coronaCase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coronaCase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "Country_Name", coronaCase.CountryId);
            return View(coronaCase);
        }

        // GET: CoronaCases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoronaCase coronaCase = db.CoronaCases.Find(id);
            if (coronaCase == null)
            {
                return HttpNotFound();
            }
            return View(coronaCase);
        }

        // POST: CoronaCases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CoronaCase coronaCase = db.CoronaCases.Find(id);
            db.CoronaCases.Remove(coronaCase);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
