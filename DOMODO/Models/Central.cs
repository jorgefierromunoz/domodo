//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DOMODO.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Central
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Central()
        {
            this.Persona_Central = new HashSet<Persona_Central>();
            this.Dispositivo = new HashSet<Dispositivo>();
        }
    
        public int IdCentral { get; set; }
        public string Nombre { get; set; }
        public string Version { get; set; }
        public string Color { get; set; }
        public string Identificador { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Persona_Central> Persona_Central { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dispositivo> Dispositivo { get; set; }
    }
}
