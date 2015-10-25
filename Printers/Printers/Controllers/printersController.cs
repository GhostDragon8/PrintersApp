using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Printers.Models;

namespace Printers.Controllers
{
    public class printersController : Controller
    {
        private PrinterDBContext db = new PrinterDBContext();

        // GET: printers
        public ActionResult Index(string search)
        {
            //LINQ query to search printers
            var all = from a in db.Printer select a;

            //if condition if search is null/empty
            if (!(string.IsNullOrEmpty(search)))
            {
                all = all.Where(a => a.Room.Contains(search) || a.Model.Contains(search) || a.Server.Contains(search));
            }
            return View(all);
        }

        // GET: printers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            printers printers = db.Printer.Find(id);
            if (printers == null)
            {
                return HttpNotFound();
            }
            return View(printers);
        }

        // GET: printers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: printers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Room,Model,Toner,IPaddress,server")] printers printers)
        {
            if (ModelState.IsValid)
            {
                db.Printer.Add(printers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(printers);
        }

        // GET: printers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            printers printers = db.Printer.Find(id);
            if (printers == null)
            {
                return HttpNotFound();
            }
            return View(printers);
        }

        // POST: printers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Room,Model,Toner,IPaddress,server")] printers printers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(printers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(printers);
        }

        // GET: printers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            printers printers = db.Printer.Find(id);
            if (printers == null)
            {
                return HttpNotFound();
            }
            return View(printers);
        }

        // POST: printers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            printers printers = db.Printer.Find(id);
            db.Printer.Remove(printers);
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
