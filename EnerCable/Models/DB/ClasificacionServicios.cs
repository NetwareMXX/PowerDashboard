//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EnerCable.Models.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClasificacionServicios
    {
        public int IdClasificacionServicio { get; set; }
        public string Clasificacion { get; set; }
        public int IdEstatus { get; set; }
        public System.DateTime FechaSistema { get; set; }
        public long IdSesion { get; set; }
        public int Orden { get; set; }
    }
}
