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
namespace EnerCable.Controllers
{
    public class ServicioController : Controller
    {
        #region Catalogo
        public ActionResult Registrar()
        {

            StringBuilder _html = new StringBuilder();
            SeguridadManager _seguridad = new SeguridadManager();
            PerfilManager _perfilMan = new PerfilManager();
            ServicioManager _servicioMan = new ServicioManager();
            ClasificacionServiciosManager _clasiMan = new ClasificacionServiciosManager();
            string _nombre = string.Empty, _correo = string.Empty;
            if (User.Identity.Name == string.Empty)
            {
                return RedirectToAction("LogIn", "Usuario");

            }

            _seguridad.getDataUsuario(User.Identity.Name, ref _nombre, ref _correo);
            ViewBag.NombreUsuario = _nombre;
            ViewBag.Correo = _correo;
            ViewBag.Menu = _seguridad.getMenu(User.Identity.Name, "Catalogos", "Servicios");
            if (HttpContext.Session["IdSesion"] == null || string.IsNullOrEmpty(HttpContext.Session["IdSesion"].ToString()))
            {
                return RedirectToAction("LogIn", "Usuario");

            }
            ViewBag.Sesion = HttpContext.Session["IdSesion"].ToString();
            List<SelectListItem> myNivel = new List<SelectListItem>();
            List<SelectListItem> myStatus = new List<SelectListItem>();
            List<SelectListItem> myUnidades = new List<SelectListItem>();
            List<SelectListItem> myClasificaciones = new List<SelectListItem>();
            List<Perfiles> _perfiles = _perfilMan.obtenerPerfiles();
            List<Estatus> _estatus = _perfilMan.obtenerStatus();
            List<Unidades> _unidades = _servicioMan.obtenerUnidades();
            List<vwClasificacionServicios> _clasificaciones = _clasiMan.obtenerClasificacion();
            foreach (Perfiles nivel in _perfiles)
            {
                if (nivel.Perfil == "Administrador")
                    myNivel.Add(new SelectListItem() { Text = nivel.Perfil, Value = nivel.IdPerfil.ToString(), Selected = true });
                else
                    myNivel.Add(new SelectListItem() { Text = nivel.Perfil, Value = nivel.IdPerfil.ToString() });
            }
            foreach (Estatus nivel in _estatus)
            {
                if (nivel.Estatus1 == "Activo")
                    myStatus.Add(new SelectListItem() { Text = nivel.Estatus1, Value = nivel.IdEstatus.ToString(), Selected = true });
                else
                    myStatus.Add(new SelectListItem() { Text = nivel.Estatus1, Value = nivel.IdEstatus.ToString() });
            }
            foreach (Unidades nivel in _unidades)
            {
                if (nivel.Unidad == "PZA")
                    myUnidades.Add(new SelectListItem() { Text = nivel.Unidad, Value = nivel.IdUnidad.ToString(), Selected = true });
                else
                    myUnidades.Add(new SelectListItem() { Text = nivel.Unidad, Value = nivel.IdUnidad.ToString() });
            }
            var _contador = 0;
            foreach (vwClasificacionServicios clasificacion in _clasificaciones)
            {
                if (_contador == 0)
                    myClasificaciones.Add(new SelectListItem() { Text = clasificacion.Clasificacion, Value = clasificacion.IdClasificacionServicio.ToString(), Selected = true });
                else
                    myClasificaciones.Add(new SelectListItem() { Text = clasificacion.Clasificacion, Value = clasificacion.IdClasificacionServicio.ToString() });
                ++_contador;
            }
            ViewBag.Niveles = myNivel;
            ViewBag.Estatus = myStatus;
            ViewBag.Unidades = myUnidades;
            ViewBag.Clasificaciones = myClasificaciones;
            return View();
        }

        [HttpGet]
        public JsonResult ObtenerServicios()
        {
            var seguridad = new ServicioManager();
            return Json(seguridad.obtenerServicios(), JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult agregarServicio(Servicios servicio)
        {
            var seguridad = new ServicioManager();

            return Json(new
            {
                Success = "OK",
                Result = seguridad.agregarServicio(
                 servicio,
                  (long)HttpContext.Session["IdSesion"]
                 )
            });
        }


        #endregion
    }
}