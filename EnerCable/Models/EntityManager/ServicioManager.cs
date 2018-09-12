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
    public class ServicioManager
    {
        #region obtenerUnidades
        public List<Unidades> obtenerUnidades()
        {
            System.Text.StringBuilder _html = new System.Text.StringBuilder();

            using (EnercableConexion db = new EnercableConexion())
            {
                var menu = from usu in db.Unidades
                           select usu;


                return menu.OrderBy(x => x.Unidad).ToList();

            }
        }
        #endregion
        #region obtenerServicios
        public List<vwServicios> obtenerServicios()
        {
            System.Text.StringBuilder _html = new System.Text.StringBuilder();

            using (EnercableConexion db = new EnercableConexion())
            {
                var menu = from usu in db.vwServicios
                           select usu;


                return menu.OrderBy(x => x.Clave).ToList();

            }
        }
        public int obtenerIdServiciosByClave(string clave)
        {
            System.Text.StringBuilder _html = new System.Text.StringBuilder();

            using (EnercableConexion db = new EnercableConexion())
            {
                var menu = from usu in db.vwServicios
                           where usu.Clave == clave
                           select usu;


                List<vwServicios> _servi = menu.OrderBy(x => x.Clave).ToList();
                if (_servi.Count > 0) return _servi[0].IdServicio;
                else return 0;

            }
        }
        public vwServicios obtenerServicioById(int Id)
        {
            
            using (EnercableConexion db = new EnercableConexion())
            {
                var menu = from usu in db.vwServicios
                           where usu.IdServicio== Id
                           select usu;


                vwServicios _servi =(vwServicios) menu.FirstOrDefault();
                return _servi;
                

            }
        }
        #endregion
        #region Resgistrar Servicio

        public string agregarServicio(Servicios servicio, long idSesion)
        {
            try
            {
                using (EnercableConexion db = new EnercableConexion())
                {
                    int _x = db.GuardarServicio(servicio.IdServicio, servicio.Clave, servicio.Descripcion, servicio.IdUnidad, servicio.PrecioUnitario, servicio.IdEstatus, idSesion,servicio.IdClasificacionServicio,servicio.MaterialNSC);
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