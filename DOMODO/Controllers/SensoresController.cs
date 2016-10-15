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
    public class SensoresController : Controller
    {
        private bd700doEntities db = new bd700doEntities();

        // GET: Sensores
        public ActionResult Index()
        {
            var sensores = db.Sensores.Include(s => s.Dispositivo);
            return View(sensores.ToList());
        }

        // GET: Sensores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sensores sensores = db.Sensores.Find(id);
            if (sensores == null)
            {
                return HttpNotFound();
            }
            return View(sensores);
        }

        // GET: Sensores/Create
        public ActionResult Create()
        {
            ViewBag.IdDispositivo = new SelectList(db.Dispositivo, "IdDispositivo", "Nombre");
            return View();
        }

        [HttpPost]
        public JsonResult CrearSensor(int iddispositivo, string nombresensor,string identificador,string pin,string tiposensor) {
            Sensores obj = new Sensores();
            obj.IdDispositivo = iddispositivo;
            obj.Nombre = nombresensor;
            obj.Pin = pin;
            obj.Identificador = identificador;
            obj.TipoSensor = tiposensor;
            db.Sensores.Add(obj);
            db.SaveChanges();
            return Json(new { Respuesta =true});
        }

        // POST: Sensores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSensores,IdDispositivo,Nombre,Pin,TipoSensor,Identificador")] Sensores sensores)
        {
            if (ModelState.IsValid)
            {
                db.Sensores.Add(sensores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdDispositivo = new SelectList(db.Dispositivo, "IdDispositivo", "Nombre", sensores.IdDispositivo);
            return View(sensores);
        }

        // GET: Sensores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sensores sensores = db.Sensores.Find(id);
            if (sensores == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDispositivo = new SelectList(db.Dispositivo, "IdDispositivo", "Nombre", sensores.IdDispositivo);
            return View(sensores);
        }

        // POST: Sensores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSensores,IdDispositivo,Nombre,Pin,TipoSensor,Identificador")] Sensores sensores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sensores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdDispositivo = new SelectList(db.Dispositivo, "IdDispositivo", "Nombre", sensores.IdDispositivo);
            return View(sensores);
        }

        // GET: Sensores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sensores sensores = db.Sensores.Find(id);
            if (sensores == null)
            {
                return HttpNotFound();
            }
            return View(sensores);
        }

        // POST: Sensores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sensores sensores = db.Sensores.Find(id);
            db.Sensores.Remove(sensores);
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
