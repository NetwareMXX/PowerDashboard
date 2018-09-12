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
    public class DepartamentoManager
    {
        #region obtenerUnidades
        public List<vwDepartamentos> obtenerDepartamentos()
        {
            System.Text.StringBuilder _html = new System.Text.StringBuilder();

            using (EnercableConexion db = new EnercableConexion())
            {
                var menu = from usu in db.vwDepartamentos
                           select usu;


                return menu.OrderBy(x => x.Departamento).ToList();

            }
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
        #endregion
    }
}