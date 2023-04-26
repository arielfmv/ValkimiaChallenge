using System.Linq;
using System.Web;
using System.Web.Mvc;
using Valkimia_App.Models;
using Valkimia_App.Helper;
using System.Web.Security;
using System.Text.RegularExpressions;
using Valkimia_challenge.Models;
using System;

namespace Valkimia_App.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Loguearse(string user, string pass)
        {
            try
            {
                using (Valkimia_challengeEntities db = new Valkimia_challengeEntities())
                {
                    string passEncriptada = EncryptPassword.GetSHA256(pass); //Encripto la pass ingresada por el user
                    var lstClientes = from c in db.Clientes where c.Email == user && c.Password == passEncriptada && c.Habilitado == true select c; //comparo el cliente ingresado con los de la bdd
                    if (lstClientes.Count() > 0) //Si es mayor a 0 es porque coinciden, si es 0 es porque las credenciales que ingreso el cliente no coinciden con las guardadas en BDD
                    {
                        Clientes oCliente = lstClientes.First(); //obtengo el cliente
                        Session["User"] = oCliente; //me trae el objeto user.
                        Session["UsuarioAutenticado"] = true;//para desplegar otras opciones del menu
                        Session["Id"] = oCliente.Id; //Guardo el id
                        return Content("1"); //inicio de sesion exitoso, devuelve 1
                    }
                    else
                    {
                        return Content("Usuario y/o contraseña incorrecta, por favor verifique sus credenciales de inicio de sesion"); //usuario/contraseña incorrecta
                    }
                }

            }
            catch (Exception ex)
            {
                return Content("Error al iniciar sesion, intentelo de nuevo.", ex.Message);
            }
        }
        public ActionResult Logout()
        {
            Session.Abandon();//Cierre de sesion
            return RedirectToAction("Index", "Login");
        }
    }
}