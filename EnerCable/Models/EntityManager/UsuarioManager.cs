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
    public class UsuarioManager
    {
        #region obtenerUsuarios

        public List<vwUsuarios> obtenerUsuarios()
        {
            System.Text.StringBuilder _html = new System.Text.StringBuilder();

            using (EnercableConexion db = new EnercableConexion())
            {
                var menu = from usu in db.vwUsuarios
                           select usu;


                return menu.OrderBy(x => x.Nombre).ToList();

            }
        }
        #endregion
        #region Resgistrar Usuario

        public string agregarUsuario(Usuarios usuario, long idSesion)
        {
            try
            {
                using (EnercableConexion db = new EnercableConexion())
                {
                    int _x = db.GuardarUsuario(usuario.IdUsuario, usuario.Usuario, usuario.Password, usuario.IdPerfil, usuario.Nombre, usuario.Paterno, usuario.Materno  == null ? "":usuario.Materno, usuario.Correo, usuario.Telefono ==null ?"":usuario.Telefono, usuario.IdEstatus, idSesion);
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