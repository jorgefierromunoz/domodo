using DOMODO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOMODO.Controllers
{
    public class HomeController : Controller
    {
        private bd700doEntities db = new bd700doEntities();

        public ActionResult Index()
        {
            int idpersona = 1;
            List<Persona_Central> listcentral = db.Persona_Central.Where(x=>x.IdPersona == idpersona).ToList();
            return View(listcentral);
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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