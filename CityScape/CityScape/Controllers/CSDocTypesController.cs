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
    public class CSDocTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CSDocTypes
        public ActionResult Index()
        {
            return View(db.CSDocTypes.ToList());
        }

        // GET: CSDocTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CSDocType cSDocType = db.CSDocTypes.Find(id);
            if (cSDocType == null)
            {
                return HttpNotFound();
            }
            return View(cSDocType);
        }

        // GET: CSDocTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CSDocTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DocType,Name")] CSDocType cSDocType)
        {
            if (ModelState.IsValid)
            {
                db.CSDocTypes.Add(cSDocType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cSDocType);
        }

        // GET: CSDocTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CSDocType cSDocType = db.CSDocTypes.Find(id);
            if (cSDocType == null)
            {
                return HttpNotFound();
            }
            return View(cSDocType);
        }

        // POST: CSDocTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocType,Name")] CSDocType cSDocType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cSDocType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cSDocType);
        }

        // GET: CSDocTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CSDocType cSDocType = db.CSDocTypes.Find(id);
            if (cSDocType == null)
            {
                return HttpNotFound();
            }
            return View(cSDocType);
        }

        // POST: CSDocTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CSDocType cSDocType = db.CSDocTypes.Find(id);
            db.CSDocTypes.Remove(cSDocType);
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
