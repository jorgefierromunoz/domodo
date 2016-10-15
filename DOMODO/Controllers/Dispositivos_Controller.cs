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
    public class Dispositivos_Controller : Controller
    {
        private bd700doEntities db = new bd700doEntities();

        // GET: Dispositivos
        public ActionResult Index()
        {
            var dispositivo = db.Dispositivo.Include(d => d.Central);
            return View(dispositivo.ToList());
        }


        public ActionResult DispositivosCentral(int id) {
            List<Dispositivo> list = db.Dispositivo.Where(x => x.IdCentral == id).ToList();
            return View(list);
        }

        //id persona
        public ActionResult TodosDispositivos(int id) {
            List<Persona_Central> centralpersona = db.Persona_Central.Where(x=>x.IdPersona == id).ToList();
            return View(centralpersona);
        }

        public ActionResult CrearDispositivo(int id)
        {
           var listpersonacentral = db.Persona_Central.Where(x=>x.IdPersona == id).ToList();
            return View(listpersonacentral);
        }

        // GET: Dispositivos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dispositivo dispositivo = db.Dispositivo.Find(id);
            if (dispositivo == null)
            {
                return HttpNotFound();
            }
            return View(dispositivo);
        }

        // GET: Dispositivos/Create
        public ActionResult Create()
        {
            ViewBag.IdCentral = new SelectList(db.Central, "IdCentral", "Nombre");
            return View();
        }

        // POST: Dispositivos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "IdDispositivo,IdCentral,Nombre,Descripcion")] Dispositivo dispositivo)
        {
            if (ModelState.IsValid)
            {
                db.Dispositivo.Add(dispositivo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCentral = new SelectList(db.Central, "IdCentral", "Nombre", dispositivo.IdCentral);
            return View(dispositivo);
        }

        // GET: Dispositivos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dispositivo dispositivo = db.Dispositivo.Find(id);
            if (dispositivo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCentral = new SelectList(db.Central, "IdCentral", "Nombre", dispositivo.IdCentral);
            return View(dispositivo);
        }

        // POST: Dispositivos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDispositivo,IdCentral,Nombre,Descripcion")] Dispositivo dispositivo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dispositivo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCentral = new SelectList(db.Central, "IdCentral", "Nombre", dispositivo.IdCentral);
            return View(dispositivo);
        }

        // GET: Dispositivos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dispositivo dispositivo = db.Dispositivo.Find(id);
            if (dispositivo == null)
            {
                return HttpNotFound();
            }
            return View(dispositivo);
        }

        // POST: Dispositivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dispositivo dispositivo = db.Dispositivo.Find(id);
            db.Dispositivo.Remove(dispositivo);
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
