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
    
    public partial class vwFolios
    {
        public int IdFolio { get; set; }
        public string Folio { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string Cluster { get; set; }
        public string OLT { get; set; }
        public string ClientesAfectados { get; set; }
        public string FallaReportada { get; set; }
        public string FechaYHoraDeAsignacionDespacho { get; set; }
        public string FechaYHoraDeAsignacionProvista { get; set; }
        public string HoraDeLlegadaALaZona { get; set; }
        public string HoraDeLaPrimeraMedicion { get; set; }
        public string UbicacionDePrimerSegundoNivelYDerivacionConSusFibras { get; set; }
        public string CausaDelDano { get; set; }
        public string UbicacionDelDano { get; set; }
        public string CoordenadasDelDanoX { get; set; }
        public string CoordenadasDelDanoY { get; set; }
        public string DescripcionDeActividades { get; set; }
        public string PotencialInicia { get; set; }
        public string PotencialFinal { get; set; }
        public string FechaHoraFinalReparacion { get; set; }
        public int IdPersona_TecnicoAtiende { get; set; }
        public int IdProveedor { get; set; }
        public int IdPersona_Supervisor { get; set; }
        public int IdPersona_AtendioDespacho { get; set; }
        public string JustificacionDeSLA { get; set; }
        public System.DateTime FechaSistema { get; set; }
        public long IdSesion { get; set; }
        public string Tecnico { get; set; }
        public string Supervisor { get; set; }
        public string Atendio { get; set; }
        public string Proveedor { get; set; }
        public int IdDepartamento { get; set; }
        public string Departamento { get; set; }
        public int IdFormato { get; set; }
        public string Formato { get; set; }
    }
}
