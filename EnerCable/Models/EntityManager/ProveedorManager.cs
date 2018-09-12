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
    public class ProveedorManager
    {
        #region obtenerProveedor
        public List<vwProveedores> obtenerProveedores()
        {
            System.Text.StringBuilder _html = new System.Text.StringBuilder();

            using (EnercableConexion db = new EnercableConexion())
            {
                var menu = from usu in db.vwProveedores
                           select usu;


                return menu.OrderBy(x => x.Proveedor).ToList();

            }
        }
        public List<vwProveedores> obtenerProveedoresValidos()
        {
            System.Text.StringBuilder _html = new System.Text.StringBuilder();

            using (EnercableConexion db = new EnercableConexion())
            {
                var menu = from usu in db.vwProveedores
                           where usu.IdEstatus == 1
                           select usu;


                return menu.OrderBy(x => x.Proveedor).ToList();

            }
        }
        public int obtenerIdProveedoresValidos(string nombre)
        {
            System.Text.StringBuilder _html = new System.Text.StringBuilder();

            using (EnercableConexion db = new EnercableConexion())
            {
                var menu = from usu in db.vwProveedores
                           
                           where  usu.IdEstatus == 1 &&
                           (usu.Proveedor.ToUpper().Trim()) ==
                           nombre.ToUpper().Replace("\r", "").Replace("\n", "").TrimStart().TrimEnd()
                           select usu;

                List<vwProveedores> _personas = menu.ToList();
                if (_personas.Count > 0) return _personas[0].IdProveedor;
                else return 0;

            }
        }
        #endregion
        #region Resgistrar Proveedor

        public string agregarProveedor(Proveedores proveedor, long idSesion)
        {
            try
            {
                using (EnercableConexion db = new EnercableConexion())
                {
                    int _x = db.GuardarProveedor(proveedor.IdProveedor, proveedor.Proveedor, proveedor.IdEstatus, idSesion);
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