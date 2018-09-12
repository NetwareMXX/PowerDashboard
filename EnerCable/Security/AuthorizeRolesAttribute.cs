using System.Web;
using System.Web.Mvc;
using EnerCable.Models.DB;
using EnerCable.Models.EntityManager;
namespace EnerCable.Security
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        private readonly string[] userAssignedRoles;
        public AuthorizeRolesAttribute(params string[] roles)
        {
            this.userAssignedRoles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = true;
            using (EnercableConexion db = new EnercableConexion())
            {
                SeguridadManager UM = new SeguridadManager();
                foreach (var roles in userAssignedRoles)
                {
                    authorize = UM.IsUserInRole(httpContext.User.Identity.Name, roles);
                    if (authorize)
                        return authorize;
                }
            }
            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Usuario/SinPermiso");
        }
        public static string Nodo()
        {
            if (HttpContext.Current.Request.Url.Host == "localhost")
            {
                //return "/Enercable";
                return "";
            }
            else
            {
                return "/Enercable";
            }
        }
    }
}