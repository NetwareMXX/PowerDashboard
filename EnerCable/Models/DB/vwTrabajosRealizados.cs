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
    
    public partial class vwTrabajosRealizados
    {
        public int IdTrabajoRealizado { get; set; }
        public int IdFolio { get; set; }
        public int IdServicio { get; set; }
        public int Consecutivo { get; set; }
        public int Cantidad { get; set; }
        public int IdUnidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }
        public System.DateTime FechaSistema { get; set; }
        public long IdSesion { get; set; }
        public string Folio { get; set; }
        public string Unidad { get; set; }
        public string Descripcion { get; set; }
        public string Clave { get; set; }
        public string DescripcionFolio { get; set; }
    }
}
