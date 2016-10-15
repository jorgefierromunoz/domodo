using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOMODO.Models
{
    public class PeticionesPendientes
    {
        public int idpeticion { get; set; }
        public string iddispositivo { get; set; }
        public string nombre_sensor { get; set; }
        public string tipo_sensor { get; set; }
        public string valor_sensor { get; set; }
        public string pin_sensor { get; set; }
        public string accion { get; set; }
        public int idsensor { get; set; }
    }
}