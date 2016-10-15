using DOMODO.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DOMODO.Controllers
{
    public class ApidomoController : Controller
    {
        private bd700doEntities db = new bd700doEntities();
        // GET: Apidomo
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult PeticionesPendientes(string idcentral)
        {
            List<PeticionesPendientes> listpeticiones = new List<PeticionesPendientes>();
            Central central = db.Central.Where(x => x.Identificador == idcentral).FirstOrDefault();
            if (central == null)
            {
                return Json(new { Peticiones = listpeticiones}, JsonRequestBehavior.AllowGet);
            }
            foreach (var dispositivo in central.Dispositivo)
            {
                if (dispositivo.Sensores != null)
                {
                    foreach (var sensor in dispositivo.Sensores)
                    {
                        if (sensor.Peticion != null)
                        {
                            foreach (var peticion in sensor.Peticion)
                            {
                                listpeticiones.Add(new PeticionesPendientes
                                {
                                    idsensor = sensor.IdSensores,
                                    idpeticion = peticion.IdPeticion,
                                    iddispositivo = dispositivo.Identificador,
                                    nombre_sensor = sensor.Nombre,
                                    tipo_sensor = sensor.TipoSensor,
                                    valor_sensor = peticion.Valor.Trim(),
                                    pin_sensor = sensor.Pin,
                                    accion = peticion.Accion,
                                });
                            }
                        }
                    }
                }
            }
            return Json(new { Peticiones = listpeticiones }, JsonRequestBehavior.AllowGet);
        }

        
        [HttpPost]
        public JsonResult RevisaRespuestasPendientes(int idcentral) {
            List<DispositivosEstado> resp = new List<DispositivosEstado>();
            var central = db.Central.Where(x=>x.IdCentral == idcentral).FirstOrDefault();
            foreach (var dispositivo in central.Dispositivo)
            {
                DispositivosEstado listadisp = new DispositivosEstado();

                List<SensoresEstado> listasensor = new List<SensoresEstado>();
                if (dispositivo.Sensores != null) {
                    foreach (var sensor in dispositivo.Sensores)
                    {
                        int valor = 0;
                        var maxregistro = db.Registro.Where(x => x.IdSensor == sensor.IdSensores).OrderByDescending(x => x.IdRegistro).FirstOrDefault();
                        if (maxregistro != null)
                        {
                            if (sensor.TipoSensor.ToLower() == "analogico")
                            {
                                valor = (int)maxregistro.ValorAnalogicoActual;
                            }
                            else
                            {
                                if ((bool)maxregistro.ValorDigitalActual)
                                {
                                    valor = 1;
                                }
                                else
                                {
                                    valor = 0;
                                }
                            }
                            listasensor.Add(new SensoresEstado
                            {
                                IdSensor = sensor.IdSensores,
                                IdDispositivo =(int) sensor.IdDispositivo,
                                NombreSensor = sensor.Nombre,
                                Pin = sensor.Pin,
                                TypoSensor = sensor.TipoSensor.ToLower(),
                                IdentificadorSensor = sensor.Identificador,
                                IdRegistro = maxregistro.IdRegistro,
                                Detalle = maxregistro.Detalle,
                                Valor = valor
                            });
                        }
                        else
                        {
                            listasensor.Add(new SensoresEstado
                            {
                                IdSensor = (int)sensor.IdSensores,
                                IdDispositivo = (int)sensor.IdDispositivo,
                                NombreSensor = sensor.Nombre,
                                Pin = sensor.Pin,
                                TypoSensor = sensor.TipoSensor.ToLower(),
                                IdentificadorSensor = sensor.Identificador,
                                IdRegistro = 0,
                                Detalle = "",
                                Valor = 0

                            });
                        }
                    }

                }
                listadisp.iddispositivo = dispositivo.IdDispositivo;
                listadisp.Lista = listasensor;
                resp.Add(listadisp);
            }
            return Json(new {Lista = resp });
        }


        [HttpPost]
        public JsonResult IngresaRespuesta(string iddispositivo,string detalle, string valoranalogico, string valordigital,string pin, string idsensor, string idpeticion,string tiposensor) {
            try
            {
                int idpeti = Convert.ToInt32(idpeticion);
                Registro obj = new Registro();
                obj.IdDispositivo = Convert.ToInt32(iddispositivo);
                obj.Detalle = detalle;
                obj.ValorAnalogicoActual = Convert.ToInt32(valoranalogico);
                obj.ValorDigitalActual = Convert.ToBoolean(valordigital);
                obj.Pin = pin;
                obj.IdSensor = Convert.ToInt32(idsensor);
                obj.TypoSensor = tiposensor;
                db.Registro.Add(obj);
                //busca peticion para eliminarla
                var peticion = db.Peticion.Where(x => x.IdPeticion == idpeti).FirstOrDefault();
                if (peticion != null)
                {
                    db.Peticion.Remove(peticion);
                }
                db.SaveChanges();
                return Json(new { Respuesta = true });
            }
            catch (Exception ex)
            {
                return Json(new { Error = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CambiarSensorDispositivo(int idsensor, string valor)
        {
            try
            {
                var exist = db.Peticion.Where(x => x.IdSensor == idsensor && x.Estado == "pendiente" && x.Valor == valor && x.Accion == "grabar").FirstOrDefault();
                if (exist == null)
                {
                    db.Peticion.Add(new Peticion { IdSensor = idsensor, Estado = "pendiente", NuevoVal = valor, Valor = valor, Accion = "grabar" });
                    db.SaveChanges();
                }
                return Json(new { Respuesta = true });
            }
            catch (Exception ex)
            {
                return Json(new { Error = ex.Message });
            }
        }
    }
}