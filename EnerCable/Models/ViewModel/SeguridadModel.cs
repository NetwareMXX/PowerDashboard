using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnerCable.Models.ViewModel
{
    public class ResultadoSesionView {
        public string Resultado { set; get; }
        public Int64 IdSesion { set; get; }
    }
    public class UsuarioSeguridadView
    {
        [Key]
        public int CveUsuario { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Usuario")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Apellido Paterno")]
        public string Paterno { get; set; }
        [Display(Name = "Apellido Materno")]
        public string Materno { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
        //public bool Activo { get; set; }
        public string Correo { get; set; }
        public Nullable<long> CveSesionRegistra { get; set; }
        public Nullable<System.DateTime> FechaSistemaRegistra { get; set; }
        public Nullable<int> CveUsuarioRegistra { get; set; }

        public bool Activo { set; get; }
        public IEnumerable<PermisoSeguridadView> Permisos { get; set; }

        public string CadenaPermisos { set; get; }
        //public virtual IEnumerable<SesionSeguridadView> Sesiones { get; set; }
    }
    public class UsuarioLoginView
    {
        [Key]
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Usuario")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }


    public class PermisoSeguridadView
    {
        [Key]
        public int CvePermiso { get; set; }
        public int CveMenu { get; set; }
        public int CveUsuario { get; set; }
        public System.DateTime FechaSistema { get; set; }


        public virtual UsuarioSeguridadView Usuarios { get; set; }

    }
    public class MenuSeguridadView
    {
        [Key]
        public int CveMenu { get; set; }
        public short Grupo { get; set; }
        public short Nivel { get; set; }
        public string Nombre { get; set; }
        public string Link { get; set; }
        public string Icono { get; set; }


        public virtual IEnumerable<PermisoSeguridadView> Permisos { get; set; }
    }
    public class SesionSeguridadView
    {
        [Key]
        public long CveSesion { get; set; }
        public int CveUsuario { get; set; }
        public string Ip { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaSistema { get; set; }

        public virtual IEnumerable<UsuarioSeguridadView> Usuarios { get; set; }
    }
    public class BitacoraSeguridadView
    {
        public System.DateTime Fecha { get; set; }
        public string NombreUsuario { get; set; }
        public string Accion { get; set; }
        public string IP { get; set; }
        public string Sistema { get; set; }
        public int Id { get; set; }
    }



    /*=========================================================================VISTA DE USUARIOS===================================================================================*/
    public class UserDataView
    {
        public IEnumerable<UsuarioSeguridadView> UsuarioPerfil { get; set; }
        public MenuSeguridadView UsuarioMenus { get; set; }

    }
    /*=========================================================================VISTA DE USUARIOS===================================================================================*/

   
    public class FoliosView
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
        public string FechaSistema { get; set; }

        public string Persona_TecnicoAtiende { get; set; }
        public string Proveedor { get; set; }
        public string Persona_Supervisor { get; set; }
        public string Persona_AtendioDespacho { get; set; }

        public string Servicios { set; get; }
        public string CoordenadasCab { set; get; }

        public long IdSesion { get; set; }

        public string TrabajosRealizados { set; get; }

    }
    public class TrabajoRealizadoView {
        public int IdOrden { set; get; }
        public int IdConcepto { set; get; }
        public int Cantidad { set; get; }
    }
    public class CoordenadasCabView
    {
        public int IdOrden { set; get; }
        public string X { set; get; }
        public string Y { set; get; }
    }
    public class MiMenuNivel
    {
        public int CveMenu { get; set; }
        public int Grupo { get; set; }
        public int Nivel { get; set; }
        public string Nombre { get; set; }
        public string Cabecera { get; set; }

        public bool EnRol { set; get; }
        public string NivelLetra { set; get; }
    }
    public class MiPersonaCargo
    {
        public int CveCargo { get; set; }
        public string Cargo { get; set; }
        public int CvePersona { get; set; }
        public string Persona { get; set; }
        
        public bool EnRol { set; get; }
        public string NivelLetra { set; get; }
        public int Nivel { get; set; }
    }
}