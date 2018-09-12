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
    public class CargoManager
    {
        #region obtenerCargos
        public List<vwCargos> obtenerCargos()
        {
            System.Text.StringBuilder _html = new System.Text.StringBuilder();

            using (EnercableConexion db = new EnercableConexion())
            {
                var menu = from usu in db.vwCargos
                           select usu;


                return menu.OrderBy(x => x.Cargo).ToList();

            }
        }
        #endregion
        #region Resgistrar Cargo

        public string agregarCargo(Cargos cargo, long idSesion)
        {
            try
            {
                using (EnercableConexion db = new EnercableConexion())
                {
                    int _x = db.GuardarCargo(cargo.IdCargo, cargo.Cargo, cargo.IdEstatus, idSesion);
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