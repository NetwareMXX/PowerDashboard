using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using EnerCable.Models.ViewModel;
using EnerCable.Models.EntityManager;
using EnerCable.Models.DB;
using System.Web.Security;
using System.IO;

namespace EnerCable.Controllers
{
    public class FolioController : Controller
    {
        #region Catalogo
        public ActionResult Registrar()
        {

            StringBuilder _html = new StringBuilder();
            SeguridadManager _seguridad = new SeguridadManager();
            PerfilManager _perfilMan = new PerfilManager();

            PersonaManager _perMan = new PersonaManager();
            ProveedorManager _provMan = new ProveedorManager();
            DepartamentoManager _depMan = new DepartamentoManager();
            ArchivoManager _arMan = new ArchivoManager();

            ServicioManager _serMan = new ServicioManager();
            UnidadManager _uMan = new UnidadManager();


            string _nombre = string.Empty, _correo = string.Empty;
            if (User.Identity.Name == string.Empty)
            {
                return RedirectToAction("LogIn", "Usuario");

            }

            _seguridad.getDataUsuario(User.Identity.Name, ref _nombre, ref _correo);
            ViewBag.NombreUsuario = _nombre;
            ViewBag.Correo = _correo;
            ViewBag.Menu = _seguridad.getMenu(User.Identity.Name, "Folios", "Folios");
            if (HttpContext.Session["IdSesion"] == null || string.IsNullOrEmpty(HttpContext.Session["IdSesion"].ToString()))
            {
                return RedirectToAction("LogIn", "Usuario");

            }
            ViewBag.Sesion = HttpContext.Session["IdSesion"].ToString();
            List<SelectListItem> myNivel = new List<SelectListItem>();
            List<Perfiles> _perfiles = _perfilMan.obtenerPerfiles();

            List<vwPersonas> _lsTecnicos = _perMan.obtenerPersonasPorCargo(PersonaManager.enumCargos.Tecnico);
            List<vwPersonas> _lsSupervisores = _perMan.obtenerPersonasPorCargo(PersonaManager.enumCargos.Supervisor);
            List<vwPersonas> _lsDespachadores = _perMan.obtenerPersonasPorCargo(PersonaManager.enumCargos.Atiende);
            List<vwProveedores> _lsProveedores = _provMan.obtenerProveedoresValidos();



            List<SelectListItem> _tecnicos = new List<SelectListItem>();
            List<SelectListItem> _supervisores = new List<SelectListItem>();
            List<SelectListItem> _despachadores = new List<SelectListItem>();
            List<SelectListItem> _proveedores = new List<SelectListItem>();

            List<SelectListItem> myDepto = new List<SelectListItem>();
            List<vwDepartamentos> _departamentos = _depMan.obtenerDepartamentos();

            List<SelectListItem> myTipos = new List<SelectListItem>();
            List<TipoArchivo> _tipos = _arMan.obtenerTiposArchivos();

            List<SelectListItem> myCarpetas = new List<SelectListItem>();
            List<Carpetas> _carpetas = _arMan.obtenerCarpetas();

            List<SelectListItem> myUnidades = new List<SelectListItem>();
            List<vwUnidades> _unidades = _uMan.obtenerUnidades();
            List<SelectListItem> myClaves = new List<SelectListItem>();
            List<vwServicios> _claves = _serMan.obtenerServicios();



            foreach (Perfiles nivel in _perfiles)
            {
                if (nivel.Perfil == "Administrador")
                    myNivel.Add(new SelectListItem() { Text = nivel.Perfil, Value = nivel.IdPerfil.ToString(), Selected = true });
                else
                    myNivel.Add(new SelectListItem() { Text = nivel.Perfil, Value = nivel.IdPerfil.ToString() });
            }

            foreach (vwPersonas nivel in _lsTecnicos)
            {

                _tecnicos.Add(new SelectListItem() { Text = nivel.Paterno + " " + nivel.Materno + " " + nivel.Nombre, Value = nivel.IdPersona.ToString() });

            }

            foreach (vwPersonas nivel in _lsSupervisores)
            {

                _supervisores.Add(new SelectListItem() { Text = nivel.Paterno + " " + nivel.Materno + " " + nivel.Nombre, Value = nivel.IdPersona.ToString() });

            }

            foreach (vwPersonas nivel in _lsDespachadores)
            {

                _despachadores.Add(new SelectListItem() { Text = nivel.Paterno + " " + nivel.Materno + " " + nivel.Nombre, Value = nivel.IdPersona.ToString() });

            }

            foreach (vwProveedores nivel in _lsProveedores)
            {

                _proveedores.Add(new SelectListItem() { Text = nivel.Proveedor, Value = nivel.IdProveedor.ToString() });

            }
            int _conta = 0;
            foreach (vwDepartamentos nivel in _departamentos)
            {
                if (_conta == 0) myDepto.Add(new SelectListItem() { Text = nivel.Departamento, Value = nivel.IdDepartamento.ToString(), Selected = true });
                else myDepto.Add(new SelectListItem() { Text = nivel.Departamento, Value = nivel.IdDepartamento.ToString() });
                ++_conta;

            }
            _conta = 0;
            foreach (TipoArchivo nivel in _tipos)
            {
                if (_conta == 0) myTipos.Add(new SelectListItem() { Text = nivel.TipoArchivo1, Value = nivel.IdTipoArchivo.ToString(), Selected = true });
                else myTipos.Add(new SelectListItem() { Text = nivel.TipoArchivo1, Value = nivel.IdTipoArchivo.ToString() });
                ++_conta;

            }
            _conta = 0;
            foreach (Carpetas nivel in _carpetas)
            {
                if (_conta == 0) myCarpetas.Add(new SelectListItem() { Text = nivel.RutaCarpeta, Value = nivel.IdCarpeta.ToString(), Selected = true });
                else myCarpetas.Add(new SelectListItem() { Text = nivel.RutaCarpeta, Value = nivel.IdCarpeta.ToString() });
                ++_conta;

            }
            _conta = 0;
            foreach (vwUnidades unidad in _unidades)
            {
                if (_conta == 0) myUnidades.Add(new SelectListItem() { Text = unidad.Unidad, Value = unidad.IdUnidad.ToString(), Selected = true });
                else myUnidades.Add(new SelectListItem() { Text = unidad.Unidad, Value = unidad.IdUnidad.ToString() });
                ++_conta;

            }
            _conta = 0;
            foreach (vwServicios servicio in _claves)
            {
                if (_conta == 0) myClaves.Add(new SelectListItem() { Text = servicio.Clave, Value = servicio.IdServicio.ToString(), Selected = true });
                else myClaves.Add(new SelectListItem() { Text = servicio.Clave, Value = servicio.IdServicio.ToString() });
                ++_conta;

            }





            ViewBag.Niveles = myNivel;
            ViewBag.Tecnicos = _tecnicos;
            ViewBag.Supervisores = _supervisores;
            ViewBag.Despachos = _despachadores;
            ViewBag.Proveedores = _proveedores;
            ViewBag.Departamentos = myDepto;
            ViewBag.Tipos = myTipos;
            ViewBag.Carpetas = myCarpetas;
            ViewBag.Claves = myClaves;
            ViewBag.Unidades = myUnidades;
            return View();
        }

        [HttpGet]
        public JsonResult obtenerFolios()
        {
            var seguridad = new FolioManager();

            /*
            var jsonResult = Json(new
            {
                Success = _success == "" ? "OK" : _success,
                Data = _htm == null ? "<p>Ocurrio El Error: " + _success + "</p><p> Consulte Al Administrador del Sistema</p>" : _htm
            }, JsonRequestBehavior.AllowGet);
            */

            var jsonResult = Json(
            seguridad.obtenerFolios(), JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = Int32.MaxValue;


            return jsonResult;

        }


        //[HttpPost]
        //public JsonResult agregarFolios(Servicios servicio)
        //{
        //    var seguridad = new UsuarioManager();

        //    return Json(new
        //    {
        //        Success = "OK",
        //        Result = seguridad.agregarUsuario(
        //         servicio,
        //          (long)HttpContext.Session["IdSesion"]
        //         )
        //    });
        //}


        #endregion
        #region Texto

        private string findValue(string[] _array, string nombre)
        {
            string _retorno = "";
            for (int i = 0; i < _array.Count(); i++)
            {
                if (_array[i].Split(':')[0].Trim() == nombre)
                {
                    _retorno = _array[i].Split(':')[1].Trim();

                }
            }
            return _retorno;
        }
        private int findPositionConReplace(string[] _array, string nombre, string _remplazar, string _remplazo)
        {
            int _retorno = -1;
            for (int i = 0; i < _array.Count(); i++)
            {
                if (_array[i].Replace(_remplazar, _remplazo).Split(Convert.ToChar(_remplazo))[0].Trim() == nombre)
                {

                    _retorno = i;

                }
            }
            return _retorno;
        }
        private int findPositionWithoutReplace(string[] _array, string nombre)
        {
            int _retorno = -1;
            for (int i = 0; i < _array.Count(); i++)
            {
                if (_array[i].Split(':')[0].Trim() == nombre)
                {

                    _retorno = i;

                }
            }
            return _retorno;
        }

        [HttpPost]
        public JsonResult obtenerObjeto(FoliosView Fol)
        {
            FoliosView _folio = new FoliosView();
            string Cadena = Fol.Folio;
            StringBuilder _html1 = new StringBuilder();
            _html1.Append("<table><thead><tr><th>Campo</th><th>Error</th></tr></thead><tbody>");
            StringBuilder _html = new StringBuilder();
            string _valor = "";
            string _trabajosRealizados = string.Empty;
            int _posicion = -1;
            try
            {

                string[] _cadena = Cadena.Trim().Split('|');
                _valor = this.findValue(_cadena, "FOLIO");
                if (_valor == "") _html.Append("<tr><td>Folio</td><td>No Encontrado</td></tr>");
                else _folio.Folio = _valor;
                _valor = this.findValue(_cadena, "CIUDAD");
                if (_valor == "") _html.Append("<tr><td>Ciudad</td><td>No Encontrado</td></tr>");
                else _folio.Ciudad = _valor;

                _valor = this.findValue(_cadena, "ESTADO");
                if (_valor == "") _html.Append("<tr><td>Estado</td><td>No Encontrado</td></tr>");
                else _folio.Estado = _valor;

                _valor = this.findValue(_cadena, "CLUSTER");
                if (_valor == "") _html.Append("<tr><td>cluster</td><td>No Encontrado</td></tr>");
                else _folio.Cluster = _valor;

                _valor = this.findValue(_cadena, "OLT");
                if (_valor == "") _html.Append("<tr><td>olt</td><td>No Encontrado</td></tr>");
                else _folio.OLT = _valor;

                _valor = this.findValue(_cadena, "CLIENTES_AFECTADOS");
                if (_valor == "") _html.Append("<tr><td>Clientes Afectados</td><td>No Encontrado</td></tr>");
                else _folio.ClientesAfectados = _valor;

                _valor = this.findValue(_cadena, "FALLA_REPORTADA");
                if (_valor == "") _html.Append("<tr><td>Falla Reportada</td><td>No Encontrado</td></tr>");
                else _folio.FallaReportada = _valor;

                _posicion = this.findPositionWithoutReplace(_cadena, "FECHA _Y_HR_DE_ASG_DESP");
                if (_posicion == -1) _html.Append("<tr><td>FECHA _Y_HR_DE_ASG_DESP</td><td>No Encontrado</td></tr>");
                else
                {
                    _folio.FechaYHoraDeAsignacionDespacho = _cadena[_posicion].Split(':')[1].Trim() + ":" + _cadena[_posicion].Split(':')[2].Trim();
                }




                _posicion = this.findPositionWithoutReplace(_cadena, "FECHA _Y_HR_DE_ASG_PROV");
                if (_posicion == -1) _html.Append("<tr><td>FECHA _Y_HR_DE_ASG_PROV</td><td>No Encontrado</td></tr>");
                else _folio.FechaYHoraDeAsignacionProvista = _cadena[_posicion].Split(':')[1].Trim() + ":" + _cadena[_posicion].Split(':')[2].Trim();


                _posicion = this.findPositionWithoutReplace(_cadena, "HR_DE_LLEGADA_ A_LA_ZONA");
                if (_posicion == -1) _html.Append("<tr><td>HR_DE_LLEGADA_ A_LA_ZONA</td><td>No Encontrado</td></tr>");
                else _folio.HoraDeLlegadaALaZona = _cadena[_posicion].Split(':')[1].Trim() + ":" + _cadena[_posicion].Split(':')[2].Trim();

                _posicion = this.findPositionWithoutReplace(_cadena, "HR_DE_LA _1er_MEDICION");
                if (_posicion == -1) _html.Append("<tr><td>HR_DE_LA _1er_MEDICION</td><td>No Encontrado</td></tr>");
                else _folio.HoraDeLaPrimeraMedicion = _cadena[_posicion].Split(':')[1].Trim() + ":" + _cadena[_posicion].Split(':')[2].Trim();

                _valor = this.findValue(_cadena, "UBICACIÓN _DE_1er_2N");
                if (_valor == "") _html.Append("<tr><td>UBICACIÓN _DE_1er_2N</td><td>No Encontrado</td></tr>");
                else _folio.UbicacionDePrimerSegundoNivelYDerivacionConSusFibras = _valor;

                _valor = this.findValue(_cadena, "CAUSA_DEL_DAÑO");
                if (_valor == "") _html.Append("<tr><td>CAUSA_DEL_DAÑO</td><td>No Encontrado</td></tr>");
                else _folio.CausaDelDano = _valor;

                _valor = this.findValue(_cadena, "UBICACIÓN_DEL_DAÑO");
                if (_valor == "") _html.Append("<tr><td>UBICACIÓN_DEL_DAÑO</td><td>No Encontrado</td></tr>");
                else _folio.UbicacionDelDano = _valor;


                _valor = this.findValue(_cadena, "CORDENADAS_DE_EL_DAÑO");
                if (_valor == "") _html.Append("<tr><td>CORDENADAS_DE_EL_DAÑO</td><td>No Encontrado</td></tr>");
                else
                {
                    _folio.CoordenadasDelDanoX = _valor.Split(',')[1];
                    _folio.CoordenadasDelDanoY = _valor.Split(',')[0];
                }

                _valor = this.findValue(_cadena, "DESCRIPCION_DE_ACTIVIDADES");
                if (_valor == "") _html.Append("<tr><td>DESCRIPCION_DE_ACTIVIDADES</td><td>No Encontrado</td></tr>");
                else _folio.DescripcionDeActividades = _valor;


                _valor = this.findValue(_cadena, "POTENCIA_INICIAL");
                if (_valor == "") _html.Append("<tr><td>POTENCIA_INICIAL</td><td>No Encontrado</td></tr>");
                else _folio.PotencialInicia = _valor;

                _valor = this.findValue(_cadena, "POTENCIA_FINAL");
                if (_valor == "") _html.Append("<tr><td>POTENCIA_FINAL</td><td>No Encontrado</td></tr>");
                else _folio.PotencialFinal = _valor;

                ///REVISAR
                ///


                _posicion = this.findPositionWithoutReplace(_cadena, "FECHA_ HR_FINAL_DE_REPARACION");
                if (_posicion == -1) _html.Append("<tr><td>FECHA_ HR_FINAL_DE_REPARACION</td><td>No Encontrado</td></tr>");
                else _folio.FechaHoraFinalReparacion = _cadena[_posicion].Split(':')[1].Trim() + ":" + _cadena[_posicion].Split(':')[2].Trim();



                _valor = this.findValue(_cadena, "ATIENDE_NOM_TEC");
                if (_valor == "") _html.Append("<tr><td>ATIENDE_NOM_TEC</td><td>No Encontrado</td></tr>");
                else _folio.Persona_TecnicoAtiende = _valor;

                _valor = this.findValue(_cadena, "PROVEDOR");
                if (_valor == "") _html.Append("<tr><td>PROVEDOR</td><td>No Encontrado</td></tr>");
                else _folio.Proveedor = _valor;

                _valor = this.findValue(_cadena, "SUPERVISOR TPE");
                if (_valor == "") _html.Append("<tr><td>SUPERVISOR TPE</td><td>No Encontrado</td></tr>");
                else _folio.Persona_Supervisor = _valor;

                _valor = this.findValue(_cadena, "ATENDIÓ_DESP");
                if (_valor == "") _html.Append("<tr><td>ATENDIÓ_DESP</td><td>No Encontrado</td></tr>");
                else _folio.Persona_AtendioDespacho = _valor;

                _valor = this.findValue(_cadena, "JUSTIFICACION _DE_SLA");
                if (_valor == "") _html.Append("<tr><td>JUSTIFICACION _DE_SLA</td><td>No Encontrado</td></tr>");
                else
                {
                    _folio.JustificacionDeSLA = _valor;
                    if (_folio.JustificacionDeSLA == string.Empty) _folio.JustificacionDeSLA = " ";
                }

                PersonaManager _perMan = new PersonaManager();
                _folio.IdPersona_TecnicoAtiende = _perMan.obtenerIdPersonasPorCargoConNombre(PersonaManager.enumCargos.Tecnico, _folio.Persona_TecnicoAtiende);
                _folio.IdPersona_Supervisor = _perMan.obtenerIdPersonasPorCargoConNombre(PersonaManager.enumCargos.Supervisor, _folio.Persona_Supervisor);
                _folio.IdPersona_AtendioDespacho = _perMan.obtenerIdPersonasPorCargoConNombre(PersonaManager.enumCargos.Atiende, _folio.Persona_AtendioDespacho);
                ProveedorManager _provMan = new ProveedorManager();
                _folio.IdProveedor = _provMan.obtenerIdProveedoresValidos(_folio.Proveedor);



                _posicion = this.findPositionConReplace(_cadena, "TRABAJOS _REALIZADOS(CONCEPTOS", "):", "%");
                if (_posicion > -1)
                {
                    string[] _servicios = _cadena[_posicion].Replace("):", "%").Split('%')[1].Replace("\n", "_").Replace("\r\n", "_").Split('_');
                    List<TrabajoRealizadoView> _trabajos = new List<TrabajoRealizadoView>();
                    if (_servicios.Count() > 0)
                    {
                        int _contador = 1;
                        foreach (string _servi in _servicios)
                        {
                            if (_servi != string.Empty)
                            {
                                string[] _cadenas = _servi.Split(':');

                                //REPLACE METROS
                                int _idservicio = new ServicioManager().obtenerIdServiciosByClave(_cadenas[0].Trim());
                                if (_idservicio > 0)
                                {
                                    _cadenas[1] = _cadenas[1].Replace("MTS", "").Trim();
                                    _trabajos.Add(new TrabajoRealizadoView() { IdConcepto = _idservicio, IdOrden = _contador, Cantidad = int.Parse(_cadenas[1].Trim()) });
                                    ++_contador;
                                }
                            }
                        }
                    }
                    _trabajosRealizados = string.Empty;
                    if (_trabajos.Count > 0)
                    {
                        foreach (TrabajoRealizadoView _trabajo in _trabajos)
                        {
                            _trabajosRealizados += _trabajo.IdOrden + ";" + _trabajo.IdConcepto + ";" + _trabajo.Cantidad + "|";
                        }
                        _trabajosRealizados += "x";
                        _trabajosRealizados = _trabajosRealizados.Replace("|x", "");
                        _folio.TrabajosRealizados = _trabajosRealizados;

                    }
                }
                else _html.Append("<tr><td>TRABAJOS _REALIZADOS</td><td>No Encontrado</td></tr>");



                _valor = this.findValue(_cadena, "COORDENADAS _DEL_CAB-024");
                if (_valor == "") _html.Append("<tr><td>COORDENADAS _DEL_CAB-024</td><td>No Encontrado</td></tr>");
                else
                {

                    string[] _coordenaasCab = _valor.Replace("\n", "_").Replace("\r\n", "_").Split('_');
                    List<CoordenadasCabView> _coordenas = new List<CoordenadasCabView>();
                    if (_coordenaasCab.Count() > 0)
                    {
                        foreach (string _coor in _coordenaasCab)
                        {
                            if (_coor.Trim() != string.Empty)
                            {
                                string[] _cadenas = _coor.Replace(".-", "_").Replace(". ", "_").Split('_');

                                //SEPARAR POR .- PERO TAMBIEN CONTEMPLAR SI NO TRAE .-
                                _coordenas.Add(new CoordenadasCabView()
                                {
                                    IdOrden = int.Parse(_cadenas[0]),
                                    X = _cadenas[1].Split(',')[0].Trim()
                                  ,
                                    Y = _cadenas[1].Split(',')[1].Trim()
                                });

                            }
                        }
                    }
                    _trabajosRealizados = string.Empty;
                    if (_coordenas.Count > 0)
                    {
                        foreach (CoordenadasCabView _trabajo in _coordenas)
                        {
                            _trabajosRealizados += _trabajo.IdOrden + ";" + _trabajo.X + ";" + _trabajo.Y + "|";
                        }
                        _trabajosRealizados += "x";
                        _trabajosRealizados = _trabajosRealizados.Replace("|x", "");
                        _folio.CoordenadasCab = _trabajosRealizados;
                    }
                }

                if (_html.ToString() != "") _html1.Append(_html + "</tbody></table>");

                #region Comentado
                /*
                _folio.Folio = _cadena[1].Split(':')[1].Trim();
                _folio.Ciudad = _cadena[2].Split(':')[1].Trim();
                _folio.Estado = _cadena[3].Split(':')[1].Trim();
                _folio.Cluster = _cadena[4].Split(':')[1].Trim();
                _folio.OLT = _cadena[5].Split(':')[1].Trim();
                _folio.ClientesAfectados = _cadena[6].Split(':')[1].Trim();
                _folio.FallaReportada = _cadena[7].Split(':')[1].Trim();
                _folio.FechaYHoraDeAsignacionDespacho = _cadena[8].Split(':')[1].Trim() + ":" + _cadena[8].Split(':')[2].Trim();
                _folio.FechaYHoraDeAsignacionProvista = _cadena[9].Split(':')[1].Trim() + ":" + _cadena[9].Split(':')[2].Trim();
                _folio.HoraDeLlegadaALaZona = _cadena[10].Split(':')[1].Trim();
                _folio.HoraDeLaPrimeraMedicion = _cadena[11].Split(':')[1].Trim();
                _folio.UbicacionDePrimerSegundoNivelYDerivacionConSusFibras = _cadena[12].Split(':')[1].Trim();
                _folio.CausaDelDano = _cadena[13].Split(':')[1].Trim();
                _folio.UbicacionDelDano = _cadena[14].Split(':')[1].Trim();
                _folio.CoordenadasDelDanoX = _cadena[15].Split(':')[1].Trim().Split(',')[1];
                _folio.CoordenadasDelDanoY = _cadena[15].Split(':')[1].Trim().Split(',')[0];
                _folio.DescripcionDeActividades = _cadena[16].Split(':')[1].Trim();
                _folio.PotencialInicia = _cadena[17].Split(':')[1].Trim();
                _folio.PotencialFinal = _cadena[18].Split(':')[1].Trim();

                _folio.FechaHoraFinalReparacion = _cadena[21].Split(':')[1].Trim() + ":" + _cadena[21].Split(':')[2].Trim();
                _folio.Persona_TecnicoAtiende = _cadena[22].Split(':')[1].Trim();
                _folio.Proveedor = _cadena[23].Split(':')[1].Trim();
                _folio.Persona_Supervisor = _cadena[24].Split(':')[1].Trim();
                _folio.Persona_AtendioDespacho = _cadena[25].Split(':')[1].Trim();
                _folio.JustificacionDeSLA = _cadena[26].Split(':')[1].Trim();
                if (_folio.JustificacionDeSLA == string.Empty) _folio.JustificacionDeSLA = " ";
                PersonaManager _perMan = new PersonaManager();
                _folio.IdPersona_TecnicoAtiende = _perMan.obtenerIdPersonasPorCargoConNombre(PersonaManager.enumCargos.Tecnico, _folio.Persona_TecnicoAtiende);
                _folio.IdPersona_Supervisor = _perMan.obtenerIdPersonasPorCargoConNombre(PersonaManager.enumCargos.Supervisor, _folio.Persona_Supervisor);
                _folio.IdPersona_AtendioDespacho = _perMan.obtenerIdPersonasPorCargoConNombre(PersonaManager.enumCargos.Atiende, _folio.Persona_AtendioDespacho);
                ProveedorManager _provMan = new ProveedorManager();
                _folio.IdProveedor = _provMan.obtenerIdProveedoresValidos(_folio.Proveedor);
                string[] _servicios = _cadena[19].Replace("):", "%").Split('%')[1].Replace("\n", "_").Replace("\r\n", "_").Split('_');
                List<TrabajoRealizadoView> _trabajos = new List<TrabajoRealizadoView>();
                if (_servicios.Count() > 0)
                {
                    int _contador = 1;
                    foreach (string _servi in _servicios)
                    {
                        if (_servi != string.Empty)
                        {
                            string[] _cadenas = _servi.Split(':');

                            //REPLACE METROS
                            int _idservicio = new ServicioManager().obtenerIdServiciosByClave(_cadenas[0].Trim());
                            if (_idservicio > 0)
                            {
                                _cadena[1] = _cadena[1].Replace("MTS", "").Trim();
                                _trabajos.Add(new TrabajoRealizadoView() { IdConcepto = _idservicio, IdOrden = _contador, Cantidad = int.Parse(_cadenas[1].Trim()) });
                                ++_contador;
                            }
                        }
                    }
                }

                string _trabajosRealizados = string.Empty;
                if (_trabajos.Count > 0)
                {
                    foreach (TrabajoRealizadoView _trabajo in _trabajos)
                    {
                        _trabajosRealizados += _trabajo.IdOrden + ";" + _trabajo.IdConcepto + ";" + _trabajo.Cantidad + "|";
                    }
                    _trabajosRealizados += "x";
                    _trabajosRealizados = _trabajosRealizados.Replace("|x", "");
                    _folio.TrabajosRealizados = _trabajosRealizados;
                }


                string[] _coordenaasCab = _cadena[20].Split(':')[1].Replace("\n", "_").Replace("\r\n", "_").Split('_');
                List<CoordenadasCabView> _coordenas = new List<CoordenadasCabView>();
                if (_coordenaasCab.Count() > 0)
                {
                    foreach (string _coor in _coordenaasCab)
                    {
                        if (_coor.Trim() != string.Empty)
                        {
                            string[] _cadenas = _coor.Replace(".-", "_").Replace(". ","_").Split('_');

                            //SEPARAR POR .- PERO TAMBIEN CONTEMPLAR SI NO TRAE .-
                            _coordenas.Add(new CoordenadasCabView()
                            {
                                IdOrden = int.Parse(_cadenas[0]),
                                X = _cadenas[1].Split(',')[0].Trim()
                              ,
                                Y = _cadenas[1].Split(',')[1].Trim()
                            });

                        }
                    }
                }
                _trabajosRealizados = string.Empty;
                if (_coordenas.Count > 0)
                {
                    foreach (CoordenadasCabView _trabajo in _coordenas)
                    {
                        _trabajosRealizados += _trabajo.IdOrden + ";" + _trabajo.X + ";" + _trabajo.Y + "|";
                    }
                    _trabajosRealizados += "x";
                    _trabajosRealizados = _trabajosRealizados.Replace("|x", "");
                    _folio.CoordenadasCab = _trabajosRealizados;
                }*/
                #endregion
                return Json(new
                {
                    Success = "OK",
                    Result = _folio,
                    Tabla = (_html.ToString() != "" ? _html1.ToString() : "<p>No Se Encontraron Errores</p>")
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Success = "WRONG",
                    Result = ex.Message,
                    Tabla = "<p>Ocurrion El Siguiente Error: " + ex.Message
                });
            }



            //foreach (string _renglon in _cadena) {
            //    if (_renglon.Contains("TRABAJOS_REALIZADOS(CONCEPTOS)"))
            //    { }
            //    else
            //    {
            //    }

            //}

        }
        #endregion
        #region CatalogosArchivos
        [HttpPost]
        public JsonResult agregarFolios(FoliosView folio)
        {
            var seguridad = new FolioManager();

            return Json(new
            {
                Success = "OK",
                Result = seguridad.agregarFolio(
                 folio, (long)HttpContext.Session["IdSesion"]
                 )
            });
        }
        [HttpPost]
        public JsonResult agregarTraabjo(TrabajosRealizados trabajo)
        {
            var seguridad = new FolioManager();

            return Json(new
            {
                Success = "OK",
                Result = seguridad.guardarTrabajoRealizado(
                 trabajo, (long)HttpContext.Session["IdSesion"]
                 )
            });
        }
        [HttpPost]
        public JsonResult eliminarTrabajo(int idTrabajo)
        {
            var seguridad = new FolioManager();

            return Json(new
            {
                Success = "OK",
                Result = seguridad.EliminarTrabajo(
                 idTrabajo, (long)HttpContext.Session["IdSesion"]
                 )
            });
        }
        [HttpGet]
        public JsonResult obtenerTrabajos(int IdFolio)
        {
            var seguridad = new FolioManager();


            return Json(seguridad.obtenerTrabajosRealizados(IdFolio), JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public JsonResult obtenerServiciosById(int Id)
        {
            var seguridad = new ServicioManager();


            return Json(seguridad.obtenerServicioById(Id), JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public JsonResult obtenerTrabajoById(int IdTrabajo)
        {
            var seguridad = new FolioManager();


            return Json(seguridad.obtenerTrabajosRealizadoById(IdTrabajo), JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public JsonResult obtenerArchivos(int IdFolio)
        {
            var seguridad = new ArchivoManager();


            return Json(seguridad.pintarHtml(IdFolio), JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult agregarDepto(int IdFolio, int Depto)
        {
            var seguridad = new FolioManager();

            return Json(new
            {
                Success = "OK",
                Result = seguridad.LigarDepto(
                 IdFolio, Depto, (long)HttpContext.Session["IdSesion"]
                 )
            });
        }

        #endregion
        #region Archivos
        [HttpPost]
        public JsonResult eliminarFolio(int IdArchivo, int IdFolio, int IdCarpeta)
        {
            var archivoman = new ArchivoManager();
            string RutaCopiado = System.Configuration.ConfigurationManager.AppSettings["rutaCopiado"].ToString();
            List<Carpetas> _carpeta = archivoman.obtenerCarpetas();
            Carpetas _x = _carpeta.Where(c => c.IdCarpeta == IdCarpeta).FirstOrDefault();
            string _archivo = archivoman.obtenerNombreArchivos(IdArchivo);

            if (_archivo != "")
            {
                var physicalPath2 = Path.Combine(RutaCopiado + _x.RutaCarpeta + @"\" + IdFolio, _archivo);
                FileInfo _info = new FileInfo(physicalPath2);
                if (_info.Exists) _info.Delete();
            }
            return Json(new
            {
                Success = "OK",
                Result = archivoman.eliminarArchivo(
                 IdArchivo, (long)HttpContext.Session["IdSesion"]
                 )
            });
        }
        [HttpPost]

        public ActionResult Save(IEnumerable<HttpPostedFileBase> files, int idFolio, int tipo, int carpeta,int evento,int orden)
        {

            if (idFolio > 0)
            {
                string _resultado = "";
                string _directorio = "Importar";
                string RutaCopiado = System.Configuration.ConfigurationManager.AppSettings["rutaCopiado"].ToString();
                foreach (var file in files)
                {

                    var fileName = Path.GetFileName(file.FileName);

                    var physicalPath = Path.Combine(Server.MapPath("~/App_Data/" + _directorio), fileName);


                    var _dir = new DirectoryInfo(Server.MapPath("~/App_Data/" + _directorio));
                    if (!_dir.Exists) _dir.Create();
                    file.SaveAs(physicalPath);
                    var seguridad = new FolioManager();
                    var archivoman = new ArchivoManager();
                    FileInfo _info = new FileInfo(physicalPath);
                    _resultado = seguridad.LigarArchivos(idFolio, tipo, carpeta, fileName, _info.Extension, (long)HttpContext.Session["IdSesion"],evento,orden);

                    //MOVER A C
                    List<Carpetas> _carpeta = archivoman.obtenerCarpetas();
                    Carpetas _x = _carpeta.Where(c => c.IdCarpeta == carpeta).FirstOrDefault();
                    _dir = new DirectoryInfo(RutaCopiado + _x.RutaCarpeta + @"\" + idFolio);
                    if (!_dir.Exists) _dir.Create();
                    var physicalPath2 = Path.Combine(RutaCopiado + _x.RutaCarpeta + @"\" + idFolio, fileName);
                    this.EliminarArchivos(physicalPath2);
                    System.IO.File.Copy(physicalPath, physicalPath2, true);
                    //MOVER A C
                    this.EliminarArchivos(physicalPath);
                    return Json(new { Archivo = fileName, Evento = (_resultado == "OK" ? "OK" : "Error"), status = _resultado, Folio = idFolio }, "text/plain");
                }
            }
            return Json(new { Filas = 0, Archivo = "", Evento = "", status = "", Folio = idFolio }, "text/plain");

        }
        public void EliminarArchivos(string ruta)
        {
            // The parameter of the Remove action must be called "fileNames"


            if (System.IO.File.Exists(ruta))
            {
                // The files are not actually removed in this demo
                System.IO.File.Delete(ruta);
            }



        }
        #endregion
    }

}