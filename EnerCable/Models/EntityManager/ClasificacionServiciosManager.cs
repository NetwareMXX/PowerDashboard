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
    public class ClasificacionServiciosManager
    {
        #region obtenerUnidades
        public List<vwClasificacionServicios> obtenerClasificacion()
        {
            System.Text.StringBuilder _html = new System.Text.StringBuilder();

            using (EnercableConexion db = new EnercableConexion())
            {
                var menu = from usu in db.vwClasificacionServicios
                           select usu;


                return menu.OrderBy(x => x.Clasificacion).ToList();

            }
        }
        #endregion
        #region Resgistrar Unidad

        public string agregarClasificacion(ClasificacionServicios clasificacion, long idSesion)
        {
            try
            {
                using (EnercableConexion db = new EnercableConexion())
                {
                    int _x = db.GuardarClasificacionServicio(clasificacion.IdClasificacionServicio,clasificacion.Clasificacion ,clasificacion.IdEstatus, idSesion);
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