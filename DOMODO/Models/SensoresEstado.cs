using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOMODO.Models
{
    public class DispositivosEstado
    {
        public int iddispositivo { get; set; }
        public List<SensoresEstado> Lista{ get; set; }
    }
    public class SensoresEstado
    {
        public int IdSensor { get; set; }
        public int IdDispositivo { get; set; }
        public string NombreSensor { get; set; }
        public string Pin { get; set; }
        public string TypoSensor { get; set; }
        public string IdentificadorSensor { get; set; }
        public int IdRegistro { get; set; }
        public string Detalle { get; set; }
        public int Valor { get; set; }
    }
}