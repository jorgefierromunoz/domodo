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
    public class Persona_CentralController : Controller
    {
        private bd700doEntities db = new bd700doEntities();

        // GET: Persona_Central
        public ActionResult Index()
        {
            var persona_Central = db.Persona_Central.Include(p => p.Central).Include(p => p.Persona);
            return View(persona_Central.ToList());
        }

        // GET: Persona_Central/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona_Central persona_Central = db.Persona_Central.Find(id);
            if (persona_Central == null)
            {
                return HttpNotFound();
            }
            return View(persona_Central);
        }

        // GET: Persona_Central/Create
        public ActionResult Create()
        {
            ViewBag.IdCentral = new SelectList(db.Central, "IdCentral", "Nombre");
            ViewBag.IdPersona = new SelectList(db.Persona, "IdPersona", "Nombre");
            return View();
        }

        // POST: Persona_Central/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPersonaCentral,IdPersona,IdCentral,Fecha")] Persona_Central persona_Central)
        {
            if (ModelState.IsValid)
            {
                db.Persona_Central.Add(persona_Central);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCentral = new SelectList(db.Central, "IdCentral", "Nombre", persona_Central.IdCentral);
            ViewBag.IdPersona = new SelectList(db.Persona, "IdPersona", "Nombre", persona_Central.IdPersona);
            return View(persona_Central);
        }

        // GET: Persona_Central/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona_Central persona_Central = db.Persona_Central.Find(id);
            if (persona_Central == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCentral = new SelectList(db.Central, "IdCentral", "Nombre", persona_Central.IdCentral);
            ViewBag.IdPersona = new SelectList(db.Persona, "IdPersona", "Nombre", persona_Central.IdPersona);
            return View(persona_Central);
        }

        // POST: Persona_Central/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPersonaCentral,IdPersona,IdCentral,Fecha")] Persona_Central persona_Central)
        {
            if (ModelState.IsValid)
            {
                db.Entry(persona_Central).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCentral = new SelectList(db.Central, "IdCentral", "Nombre", persona_Central.IdCentral);
            ViewBag.IdPersona = new SelectList(db.Persona, "IdPersona", "Nombre", persona_Central.IdPersona);
            return View(persona_Central);
        }

        // GET: Persona_Central/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona_Central persona_Central = db.Persona_Central.Find(id);
            if (persona_Central == null)
            {
                return HttpNotFound();
            }
            return View(persona_Central);
        }

        // POST: Persona_Central/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Persona_Central persona_Central = db.Persona_Central.Find(id);
            db.Persona_Central.Remove(persona_Central);
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
