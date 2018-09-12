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
    public class ArchivoManager
    {
        #region obtenerTiposArchivos
        public List<TipoArchivo> obtenerTiposArchivos()
        {
            System.Text.StringBuilder _html = new System.Text.StringBuilder();

            using (EnercableConexion db = new EnercableConexion())
            {
                var menu = from usu in db.TipoArchivo
                           select usu;


                return menu.OrderBy(x => x.TipoArchivo1).ToList();

            }
        }

        #endregion

        #region obtenerCarpetas
        public List<Carpetas> obtenerCarpetas()
        {
            System.Text.StringBuilder _html = new System.Text.StringBuilder();

            using (EnercableConexion db = new EnercableConexion())
            {
                var menu = from usu in db.Carpetas
                           select usu;


                return menu.OrderBy(x => x.RutaCarpeta).ToList();

            }
        }

        #endregion
        #region obtenerArchivos
        private List<vwArchivos> obtenerArchivos(int idFolio)
        {
            System.Text.StringBuilder _html = new System.Text.StringBuilder();

            using (EnercableConexion db = new EnercableConexion())
            {
                var menu = from usu in db.vwArchivos
                           where usu.IdFolio == idFolio
                           select usu;


                return menu.OrderBy(x => x.RutaCarpeta).OrderBy(x => x.Nombre).ToList();

            }
        }

        public string pintarHtml(int idfolio)
        {
            StringBuilder _html = new StringBuilder();


            List<vwArchivos> _lista = this.obtenerArchivos(idfolio);


            if (_lista.Count <= 0) return "<p>No Existen Archivos Cargados</p>";

            _html.Append("<table class='table'>");
            _html.Append("      <thead>");
            _html.Append("          <tr>");
            _html.Append("              <th>Carpeta</th>");
            _html.Append("              <th>Tipo Archivo</th>");
            _html.Append("              <th>Nombre</th>");
            _html.Append("              <th>Eliminar</th>");

            _html.Append("                        </tr>");
            _html.Append("</thead>");
            _html.Append("<tbody>");

            int _contado = 1;

            foreach (vwArchivos _nivel in _lista)
            {
                _html.Append("<tr>");

                _html.Append("<td>" + _nivel.RutaCarpeta + "</td>");
                _html.Append("<td>" + _nivel.TipoArchivo + "</td>");
                _html.Append("<td>" + _nivel.Nombre + "</td>");
                _html.Append("<td>" + "<a href='#' onclick='return eliminarFolio(" + _nivel.IdArchivo + "," + _nivel.IdFolio + "," + _nivel.IdCarpeta + ");'>Eliminar</a></td>");

                _html.Append("</tr>");
                ++_contado;

            }


            _html.Append("</tbody>");
            _html.Append("</table>");
            return _html.ToString();

        }
        #endregion
        #region Resgistrar Unidad

        public string agregarDepartamentos(Departamentos departamento, long idSesion)
        {
            try
            {
                using (EnercableConexion db = new EnercableConexion())
                {
                    int _x = db.GuardarDepartamento(departamento.IdDepartamento, departamento.Departamento, departamento.NombreGerente, departamento.Correo, departamento.Telefono, departamento.IdEstatus, idSesion);
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string eliminarArchivo(int IdArchivo, long idSesion)
        {
            try
            {
                using (EnercableConexion db = new EnercableConexion())
                {
                    int _x = db.EliminarArchivo(IdArchivo, idSesion);
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }

        #endregion
        #region obtenerNombreArchivos
        public string obtenerNombreArchivos(int IdArchivo)
        {
            try
            {
                using (EnercableConexion db = new EnercableConexion())
                {
                    var menu = from usu in db.Archivos
                               where usu.IdArchivo == IdArchivo
                               select usu;

                    return menu.FirstOrDefault().Nombre;



                }
            }
            catch (Exception ex) { return ""; }
        }

        #endregion

    }
}