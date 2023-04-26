using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Valkimia_App.Controllers;
using Valkimia_App.Models;
using Valkimia_challenge.Models;

namespace Valkimia_App.Filters
{
    public class VerificacionLogin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)//sobreescribo el metodo 
        {
            var objCliente = (Clientes)HttpContext.Current.Session["User"]; //para obtener la session
            if (objCliente == null) //Si es nulo es porque no tengo session
            {
                if (filterContext.Controller is LoginController == false) //se redirige al login, de esta forma no puede acceder a otras vistas.
                {
                    filterContext.HttpContext.Response.Redirect("~/Login/Index");
                }
            }
            else
            {
                if (filterContext.Controller is LoginController == true) //si el usuario ya se logueo no le permite volver al login.
                {
                    filterContext.HttpContext.Response.Redirect("~/Home/Index");
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}