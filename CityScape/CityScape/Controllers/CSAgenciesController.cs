using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CityScape.Models;

namespace CityScape.Controllers
{
    public class CSAgenciesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CSAgencies
        public ActionResult Index()
        {
            return View(db.CSAgencies.ToList());
        }

        // GET: CSAgencies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CSAgency cSAgency = db.CSAgencies.Find(id);
            if (cSAgency == null)
            {
                return HttpNotFound();
            }
            return View(cSAgency);
        }

        // GET: CSAgencies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CSAgencies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Agency,Name")] CSAgency cSAgency)
        {
            if (ModelState.IsValid)
            {
                db.CSAgencies.Add(cSAgency);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cSAgency);
        }

        // GET: CSAgencies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CSAgency cSAgency = db.CSAgencies.Find(id);
            if (cSAgency == null)
            {
                return HttpNotFound();
            }
            return View(cSAgency);
        }

        // POST: CSAgencies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Agency,Name")] CSAgency cSAgency)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cSAgency).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cSAgency);
        }

        // GET: CSAgencies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CSAgency cSAgency = db.CSAgencies.Find(id);
            if (cSAgency == null)
            {
                return HttpNotFound();
            }
            return View(cSAgency);
        }

        // POST: CSAgencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CSAgency cSAgency = db.CSAgencies.Find(id);
            db.CSAgencies.Remove(cSAgency);
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
