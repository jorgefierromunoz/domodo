using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DOMODO.Models;

namespace DOMODO.Controllers
{
    public class CentralController : Controller
    {
        private bd700doEntities db = new bd700doEntities();

        // GET: Central
        public ActionResult Index()
        {
            return View(db.Central.ToList());
        }

        // GET: Central/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Central central = db.Central.Find(id);
            if (central == null)
            {
                return HttpNotFound();
            }
            return View(central);
        }

        // GET: Central/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Central/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCentral,Nombre,Version,Color,Identificador")] Central central)
        {
            if (ModelState.IsValid)
            {
                db.Central.Add(central);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(central);
        }

        // GET: Central/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Central central = db.Central.Find(id);
            if (central == null)
            {
                return HttpNotFound();
            }
            return View(central);
        }

        // POST: Central/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCentral,Nombre,Version,Color,Identificador")] Central central)
        {
            if (ModelState.IsValid)
            {
                db.Entry(central).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(central);
        }

        // GET: Central/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Central central = db.Central.Find(id);
            if (central == null)
            {
                return HttpNotFound();
            }
            return View(central);
        }

        // POST: Central/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Central central = db.Central.Find(id);
            db.Central.Remove(central);
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
