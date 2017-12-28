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
    public class CSDocsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CSDocs
        public ActionResult Index()
        {
            var cSDocs = db.CSDocs.Include(c => c.CSAgency).Include(c => c.CSDocType);
            return View(cSDocs.ToList());
        }

        // GET: CSDocs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CSDoc cSDoc = db.CSDocs.Find(id);
            if (cSDoc == null)
            {
                return HttpNotFound();
            }
            return View(cSDoc);
        }

        // GET: CSDocs/Create
        public ActionResult Create()
        {
            ViewBag.Agency = new SelectList(db.CSAgencies, "Agency", "Name");
            ViewBag.DocType = new SelectList(db.CSDocTypes, "DocType", "Name");
            return View();
        }

        // POST: CSDocs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DocId,Date,Name,Agency,DocType,Project,Source,Filename,Extension")] CSDoc cSDoc)
        {
            if (ModelState.IsValid)
            {
                db.CSDocs.Add(cSDoc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Agency = new SelectList(db.CSAgencies, "Agency", "Name", cSDoc.Agency);
            ViewBag.DocType = new SelectList(db.CSDocTypes, "DocType", "Name", cSDoc.DocType);
            return View(cSDoc);
        }

        // GET: CSDocs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CSDoc cSDoc = db.CSDocs.Find(id);
            if (cSDoc == null)
            {
                return HttpNotFound();
            }
            ViewBag.Agency = new SelectList(db.CSAgencies, "Agency", "Name", cSDoc.Agency);
            ViewBag.DocType = new SelectList(db.CSDocTypes, "DocType", "Name", cSDoc.DocType);
            return View(cSDoc);
        }

        // POST: CSDocs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocId,Date,Name,Agency,DocType,Project,Source,Filename,Extension")] CSDoc cSDoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cSDoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Agency = new SelectList(db.CSAgencies, "Agency", "Name", cSDoc.Agency);
            ViewBag.DocType = new SelectList(db.CSDocTypes, "DocType", "Name", cSDoc.DocType);
            return View(cSDoc);
        }

        // GET: CSDocs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CSDoc cSDoc = db.CSDocs.Find(id);
            if (cSDoc == null)
            {
                return HttpNotFound();
            }
            return View(cSDoc);
        }

        // POST: CSDocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CSDoc cSDoc = db.CSDocs.Find(id);
            db.CSDocs.Remove(cSDoc);
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
