using System;
using System.Linq;
using EnerCable.Models.DB;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using EnerCable.Security;
using EnerCable.Models.ViewModel;
using System.Web;
using System.Web.Mvc;
namespace EnerCable.Models.EntityManager
{
    public class FolioManager
    {
        #region obtenerCargos
        public List<vwFolios> obtenerFolios()
        {
            System.Text.StringBuilder _html = new System.Text.StringBuilder();

            using (EnercableConexion db = new EnercableConexion())
            {
                var menu = from usu in db.vwFolios
                           select usu;


                return menu.OrderByDescending(x => x.IdFolio).ToList();

            }
        }
        #endregion
        #region Resgistrar Folio

        public string agregarFolio(FoliosView folio, long idSesion)
        {
            try
            {
                using (EnercableConexion db = new EnercableConexion())
                {
                    int _x = db.GuardarFolio(folio.IdFolio, folio.Folio, folio.Ciudad, folio.Estado, folio.Cluster, folio.OLT,
                        folio.ClientesAfectados, folio.FallaReportada, folio.FechaYHoraDeAsignacionDespacho, folio.FechaYHoraDeAsignacionProvista,
                        folio.HoraDeLlegadaALaZona, folio.HoraDeLaPrimeraMedicion, folio.UbicacionDePrimerSegundoNivelYDerivacionConSusFibras,
                        folio.CausaDelDano, folio.UbicacionDelDano, folio.CoordenadasDelDanoX, folio.CoordenadasDelDanoY, folio.DescripcionDeActividades,
                        folio.PotencialInicia, folio.PotencialFinal, folio.FechaHoraFinalReparacion, folio.IdPersona_TecnicoAtiende, folio.IdProveedor,
                        folio.IdPersona_Supervisor, folio.IdPersona_AtendioDespacho, folio.JustificacionDeSLA, false, folio.TrabajosRealizados, false, folio.CoordenadasCab, idSesion,folio.IdFormato
                        );

                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        #endregion
        #region obtenerTrabajosRealizados
        public string obtenerTrabajosRealizados(int idFolio)
        {
            StringBuilder _html = new StringBuilder();
            List<MiPersonaCargo> _lista = new List<MiPersonaCargo>();
            List<MiPersonaCargo> _lista2 = new List<MiPersonaCargo>();
            using (EnercableConexion db = new EnercableConexion())
            {

                //Estatus _es = db.Estatus.Where(o => o.Estatus1.ToLower().Equals("activo"))?.FirstOrDefault();
                //Usuarios SU = db.Usuarios.Where(o => o.Usuario.ToLower().Equals(loginName) && o.IdEstatus == _es.IdEstatus)?.FirstOrDefault();
                var _mimenu = (

                                    from t in db.vwTrabajosRealizados
                                    where t.IdFolio.Equals(idFolio)
                                    select t);
                List<vwTrabajosRealizados> _trabajos = _mimenu.ToList();
                return this.PintarHtmlTrabajos(_trabajos);
            }
        }
        public string PintarHtmlTrabajos(List<vwTrabajosRealizados> _niveles)
        {
            StringBuilder _html = new StringBuilder();


            if (_niveles.Count == 0) return "<b>Sin Trabajos Realizados</b>";



            _html.Append("<table class='table'>");
            _html.Append("      <thead>");
            _html.Append("          <tr>");
            _html.Append("              <th>#</th>");
            _html.Append("              <th>Clave</th>");
            _html.Append("              <th>Descripcion</th>");
            _html.Append("              <th>Cantidad</th>");
            _html.Append("              <th>P.U.</th>");
            _html.Append("              <th>Total</th>");
            _html.Append("              <th>Unidad</th>");
            _html.Append("              <th></th>");
            _html.Append("              <th></th>");

            _html.Append("                        </tr>");
            _html.Append("</thead>");
            _html.Append("<tbody>");

            int _contado = 1;

            foreach (vwTrabajosRealizados _nivel in _niveles)
            {
                _html.Append("<tr>");

                _html.Append("<td>" + _nivel.Consecutivo + "</td>");
                _html.Append("<td>" + _nivel.Clave + "</td>");
                _html.Append("<td>" + _nivel.Descripcion + "</td>");
                _html.Append("<td>" + _nivel.Cantidad + "</td>");
                _html.Append("<td>" + _nivel.PrecioUnitario + "</td>");
                _html.Append("<td>" + _nivel.Total + "</td>");
                _html.Append("<td>" + _nivel.Unidad + "</td>");
                _html.Append("<td><a href='#' onclick='return loadTrabajo("+_nivel.IdTrabajoRealizado+");'>Editar</a></td>");
                _html.Append("<td><a href='#' onclick='return eliminarTrabajo(" + _nivel.IdTrabajoRealizado+","+_nivel.IdFolio + ");'>Eliminar</a></td>");
                



                _html.Append("</tr>");
                ++_contado;

            }


            _html.Append("</tbody>");
            _html.Append("</table>");
            return _html.ToString();

        }

        #endregion


        #region obtenerTrabajosRealizados
        public vwTrabajosRealizados obtenerTrabajosRealizadoById(int idTrabajo)
        {
            StringBuilder _html = new StringBuilder();
            List<MiPersonaCargo> _lista = new List<MiPersonaCargo>();
            List<MiPersonaCargo> _lista2 = new List<MiPersonaCargo>();
            using (EnercableConexion db = new EnercableConexion())
            {

                //Estatus _es = db.Estatus.Where(o => o.Estatus1.ToLower().Equals("activo"))?.FirstOrDefault();
                //Usuarios SU = db.Usuarios.Where(o => o.Usuario.ToLower().Equals(loginName) && o.IdEstatus == _es.IdEstatus)?.FirstOrDefault();
                var _mimenu = (

                                    from t in db.vwTrabajosRealizados
                                    where t.IdTrabajoRealizado.Equals(idTrabajo)
                                    select t).FirstOrDefault();
                vwTrabajosRealizados _trabajo =(vwTrabajosRealizados) _mimenu;
                return _trabajo;
            }
        }
        #endregion
        #region guardarTrabajoRealizado

        public string guardarTrabajoRealizado(TrabajosRealizados trabajo,long idsesion)
        {
            try
            {
                using (EnercableConexion db = new EnercableConexion())
                {
                    int _x = db.GuardarTrabajoRealizado(trabajo.IdFolio,trabajo.IdServicio,trabajo.Cantidad,trabajo.IdUnidad,trabajo.PrecioUnitario,trabajo.Descripcion,idsesion,trabajo.IdTrabajoRealizado,trabajo.Consecutivo
                        );

                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        #endregion
        #region EliminarTrabajo

        public string EliminarTrabajo(int idTrabajo, long idSesion)
        {
            try
            {
                using (EnercableConexion db = new EnercableConexion())
                {
                    int _x = db.EliminarTrabajo(idTrabajo, idSesion
                        );

                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        #endregion
        #region LigarDepto

        public string LigarDepto(int idFolio, int idDepto, long idSesion)
        {
            try
            {
                using (EnercableConexion db = new EnercableConexion())
                {
                    int _x = db.GuardarDepartamentoFolio(idFolio, idDepto, idSesion
                        );

                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        #endregion
        #region LigarArchivos

        public string LigarArchivos(int idFolio, int idtipoArchivo, int idCarpeta, string Nombre, string extension, long idSesion,int idevento,int idorden)
        {
            try
            {
                using (EnercableConexion db = new EnercableConexion())
                {
                    int _x = db.GuardarArchivo(
                        idFolio, idtipoArchivo, idCarpeta, Nombre, extension,
                        idSesion,idorden,idevento
                        );

                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        #endregion
    }
}