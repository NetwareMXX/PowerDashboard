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
    public class UnidadController : Controller
    {
        // GET: Unidad
        #region Catalogo
        public ActionResult Registrar()
        {

            StringBuilder _html = new StringBuilder();
            SeguridadManager _seguridad = new SeguridadManager();
            PerfilManager _perfilMan = new PerfilManager();

            string _nombre = string.Empty, _correo = string.Empty;
            if (User.Identity.Name == string.Empty)
            {
                return RedirectToAction("LogIn", "Usuario");

            }

            _seguridad.getDataUsuario(User.Identity.Name, ref _nombre, ref _correo);
            ViewBag.NombreUsuario = _nombre;
            ViewBag.Correo = _correo;
            ViewBag.Menu = _seguridad.getMenu(User.Identity.Name, "Catalogos", "Unidades");
            if (HttpContext.Session["IdSesion"] == null || string.IsNullOrEmpty(HttpContext.Session["IdSesion"].ToString()))
            {
                return RedirectToAction("LogIn", "Usuario");

            }
            ViewBag.Sesion = HttpContext.Session["IdSesion"].ToString();
            List<SelectListItem> myNivel = new List<SelectListItem>();
            List<SelectListItem> myStatus = new List<SelectListItem>();
            List<Perfiles> _perfiles = _perfilMan.obtenerPerfiles();
            List<Estatus> _estatus = _perfilMan.obtenerStatus();

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

            ViewBag.Niveles = myNivel;
            ViewBag.Estatus = myStatus;

            return View();
        }

        [HttpGet]
        public JsonResult ObtenerUnidades()
        {
            var seguridad = new UnidadManager();
            return Json(seguridad.obtenerUnidades(), JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult agregarUnidad(Unidades unidad)
        {
            var seguridad = new UnidadManager();

            return Json(new
            {
                Success = "OK",
                Result = seguridad.agregarUnidad(
                 unidad,
                  (long)HttpContext.Session["IdSesion"]
                 )
            });
        }


        #endregion
    }
}