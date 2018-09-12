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
    public class FormatoManager
    {
        #region obtenerFormatos
        public List<vwFormatos> obtenerFormatos()
        {
            System.Text.StringBuilder _html = new System.Text.StringBuilder();

            using (EnercableConexion db = new EnercableConexion())
            {
                var menu = from usu in db.vwFormatos
                           select usu;
                return menu.OrderBy(x => x.Formato).ToList();

            }
        }
        #endregion
        #region Resgistrar formato

        public string agregarFormato(Formatos formato, long idSesion)
        {
            try
            {
                using (EnercableConexion db = new EnercableConexion())
                {
                    int _x = db.GuardarFormato(formato.IdFormato,formato.Formato,formato.IdEstatus, idSesion);
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        #endregion
        #region obtenerServicios
        public string obtenerServicios(int idFormato)
        {
            StringBuilder _html = new StringBuilder();
            List<MiPersonaCargo> _lista = new List<MiPersonaCargo>();
            List<MiPersonaCargo> _lista2 = new List<MiPersonaCargo>();
            using (EnercableConexion db = new EnercableConexion())
            {

                //Estatus _es = db.Estatus.Where(o => o.Estatus1.ToLower().Equals("activo"))?.FirstOrDefault();
                //Usuarios SU = db.Usuarios.Where(o => o.Usuario.ToLower().Equals(loginName) && o.IdEstatus == _es.IdEstatus)?.FirstOrDefault();
                var _mimenu = (

                                    from t in db.ServiciosPorFormato
                                    join u in db.vwServicios on t.IdServicio equals u.IdServicio
                                    where t.IdFormato.Equals(idFormato)
                                    select u);
                List<vwServicios> _trabajos = _mimenu.ToList();
                return this.PintarHtmlTrabajos(_trabajos,idFormato);
            }
        }
        public string PintarHtmlTrabajos(List<vwServicios> _niveles,int idFormato)
        {
            StringBuilder _html = new StringBuilder();


            if (_niveles.Count == 0) return "<b>Formato Sin Servicios</b>";



            _html.Append("<table class='table'>");
            _html.Append("      <thead>");
            _html.Append("          <tr>");
            _html.Append("              <th>#</th>");
            _html.Append("              <th>Clave</th>");
            _html.Append("              <th>Descripcion</th>");
            _html.Append("              <th></th>");

            _html.Append("                        </tr>");
            _html.Append("</thead>");
            _html.Append("<tbody>");

            int _contado = 1;

            foreach (vwServicios _nivel in _niveles)
            {
                _html.Append("<tr>");

                _html.Append("<td>" + _nivel.IdServicio + "</td>");
                _html.Append("<td>" + _nivel.Clave + "</td>");
                _html.Append("<td>" + _nivel.Descripcion + "</td>");
                _html.Append("<td><a href='#' onclick='return eliminarTrabajo(" + _nivel.IdServicio + "," + idFormato + ");'>Eliminar</a></td>");
                _html.Append("</tr>");
                ++_contado;

            }


            _html.Append("</tbody>");
            _html.Append("</table>");
            return _html.ToString();

        }

        #endregion
        #region EliminarTrabajo

        public string EliminarTrabajo(int idServicio,int idFormato, long idSesion)
        {
            try
            {
                using (EnercableConexion db = new EnercableConexion())
                {
                    int _x = db.EliminarServicioPorFormato(idServicio,idFormato, idSesion
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
        #region Resgistrar Servicio

        public string agregarServicio(int idServicio,int IdFormato, long idSesion)
        {
            try
            {
                using (EnercableConexion db = new EnercableConexion())
                {
                    int _x = db.GuardarServicioPorFormato(IdFormato,idServicio, idSesion);
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