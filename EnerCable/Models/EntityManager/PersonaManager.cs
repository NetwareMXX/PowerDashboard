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
    public class PersonaManager
    {
        public enum enumCargos
        {
            Tecnico = 1,
            Supervisor = 2,
            Atiende = 4
        }
        #region obtenerPersonas
        public List<vwPersonas> obtenerPersonas()
        {
            System.Text.StringBuilder _html = new System.Text.StringBuilder();

            using (EnercableConexion db = new EnercableConexion())
            {
                var menu = from usu in db.vwPersonas

                           select usu;


                return menu.OrderBy(x => x.Paterno).ToList();

            }
        }
        public List<vwPersonas> obtenerPersonasPorCargo(enumCargos _cargo)
        {
            System.Text.StringBuilder _html = new System.Text.StringBuilder();

            object val = Convert.ChangeType(_cargo, _cargo.GetTypeCode());
            int _idcargo = Convert.ToInt32(val);

            using (EnercableConexion db = new EnercableConexion())
            {
                var menu = from usu in db.vwPersonas
                           join car in db.CargosPersonas on usu.IdPersona equals (car.IdPersona)
                           where car.IdCargo == _idcargo && usu.IdEstatus == 1
                           select usu;


                return menu.OrderBy(x => x.Paterno).ToList();

            }
        }
        public List<vwPersonas> obtenerPersonasPorCargoTodos(int cargo)
        {


            using (EnercableConexion db = new EnercableConexion())
            {
                var menu = from usu in db.vwPersonas
                           join car in db.CargosPersonas on usu.IdPersona equals (car.IdPersona)
                           where car.IdCargo == cargo && usu.IdEstatus == 1
                           select usu;


                return menu.OrderBy(x => x.Paterno).ToList();

            }
        }
        public int obtenerIdPersonasPorCargoConNombre(enumCargos _cargo, string _nombre)
        {
            System.Text.StringBuilder _html = new System.Text.StringBuilder();

            object val = Convert.ChangeType(_cargo, _cargo.GetTypeCode());
            int _idcargo = Convert.ToInt32(val);

            using (EnercableConexion db = new EnercableConexion())
            {
                var menu = from usu in db.vwPersonas
                           join car in db.CargosPersonas on usu.IdPersona equals (car.IdPersona)
                           where car.IdCargo == _idcargo && usu.IdEstatus == 1 &&
                           (usu.Nombre.ToUpper().Trim() + " " + usu.Paterno.ToUpper().Trim() + " " + usu.Materno.ToUpper().Trim()) ==
                           _nombre.ToUpper().Replace("\r", "").Replace("\n", "").TrimStart().TrimEnd()
                           select usu;

                List<vwPersonas> _personas = menu.ToList();
                if (_personas.Count > 0) return _personas[0].IdPersona;
                else return 0;


            }
        }
        #endregion
        #region Resgistrar Persona

        public string agregarPersona(Personas persona, long idSesion)
        {
            try
            {
                using (EnercableConexion db = new EnercableConexion())
                {
                    int _x = db.GuardarPersona(persona.IdPersona, persona.Nombre, persona.Paterno, persona.Materno, persona.IdEstatus, idSesion);
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        #endregion
        #region obtenerCargosPorPersona
        public string obtenerCargosPorPersona(int idPersona)
        {
            StringBuilder _html = new StringBuilder();
            List<MiPersonaCargo> _lista = new List<MiPersonaCargo>();
            List<MiPersonaCargo> _lista2 = new List<MiPersonaCargo>();
            using (EnercableConexion db = new EnercableConexion())
            {

                //Estatus _es = db.Estatus.Where(o => o.Estatus1.ToLower().Equals("activo"))?.FirstOrDefault();
                //Usuarios SU = db.Usuarios.Where(o => o.Usuario.ToLower().Equals(loginName) && o.IdEstatus == _es.IdEstatus)?.FirstOrDefault();
                var _mimenu = (

                                    from car in db.Cargos
                                    join es in db.Estatus on car.IdEstatus equals es.IdEstatus
                                    join carper in db.CargosPersonas on car.IdCargo equals carper.IdCargo
                                    where carper.IdPersona.Equals(idPersona)
                                    select new ViewModel.MiPersonaCargo()
                                    {
                                        CveCargo = car.IdCargo,
                                        Cargo = car.Cargo,
                                        EnRol = false,
                                        CvePersona = idPersona,
                                        Nivel = idPersona,
                                        NivelLetra = es.Estatus1
                                    });
                var _mimenu2 =
          (
         from
            car in db.Cargos
         join es in db.Estatus on car.IdEstatus equals es.IdEstatus

         select new ViewModel.MiPersonaCargo()
         {
             CveCargo = car.IdCargo,
             Cargo = car.Cargo,
             EnRol = false,
             CvePersona = idPersona,
             Nivel = 0,
             NivelLetra = es.Estatus1
         });

                _lista = _mimenu.ToList();
                _lista2 = _mimenu2.ToList();
                _lista = _lista.Where(x => x.Nivel == idPersona).ToList();

                _lista2 = _lista2.Where(x =>

                 !_lista.Select(z => z.CveCargo).Distinct().Contains(

                               x.CveCargo

                   )).ToList();

                _lista.ForEach(c => c.EnRol = true);

                _lista = _lista.Union(_lista2).ToList();
                _lista =
                            (
                                from x in _lista
                                orderby x.Cargo
                                select x
                            ).ToList();
                //_lista.OrderBy(m => m.Cabecera,mbox => m.nombre).ToList();
                //_lista = _lista.OrderBy(m => m.Nombre).ToList();
            }
            return this.PintarHtmlNiveles(_lista);
        }
        public string PintarHtmlNiveles(List<ViewModel.MiPersonaCargo> _niveles)
        {
            StringBuilder _html = new StringBuilder();


            List<ViewModel.MiPersonaCargo> _lista = _niveles.Where(x => x.EnRol == true).ToList();



            _html.Append("<table class='table'>");
            _html.Append("      <thead>");
            _html.Append("          <tr>");
            _html.Append("<th><input id='cchMain'  type='checkbox' " + (_niveles.Count == _lista.Count ? "checked='checked'" : "") + " onclick='checkTodo(this);' class='chkMain filled-in'><label for='cchMain'>Seleccionar Todos</label></th>");
            _html.Append("              <th>Cargo</th>");
            _html.Append("              <th>Estatus Cargo</th>");
            //_html.Append("              <th>Permiso</th>");
            _html.Append("                        </tr>");
            _html.Append("</thead>");
            _html.Append("<tbody>");

            int _contado = 1;

            foreach (ViewModel.MiPersonaCargo _nivel in _niveles)
            {
                _html.Append("<tr>");
                _html.Append("<td scope='row'><input id='chk" + _contado + "' valor='" + _nivel.CveCargo + "' type='checkbox' " + (_nivel.EnRol ? "checked='checked'" : "") + " class='chkPer filled-in'><label for='chk" + _contado + "'>" + "</label></td>");
                _html.Append("<td>" + _nivel.Cargo + "</td>");
                _html.Append("<td>" + _nivel.NivelLetra + "</td>");



                _html.Append("</tr>");
                ++_contado;

            }


            _html.Append("</tbody>");
            _html.Append("</table>");
            return _html.ToString();

        }
        #endregion
        #region Guardar Cargo
        public string guardarCargo(string cargo, int idpersona, long idsesion)
        {
            try
            {
                using (EnercableConexion db = new EnercableConexion())
                {

                    int _x = 0;

                    _x = db.GuardarCargoPersona(cargo, idpersona, idsesion);


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