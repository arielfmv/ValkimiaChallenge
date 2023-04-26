using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Valkimia_App.Models;
using Valkimia_App.Models.ViewModels;
using Valkimia_App.Helper;
using Valkimia_challenge.Models;

namespace Valkimia_App.Controllers
{
    public class ClienteController : Controller
    {
        [AllowAnonymous]
        // GET: Cliente
        [HttpGet]
        public ActionResult AltaCliente()
        {
            var db = new Valkimia_challengeEntities(); // Creo la instancia
            var datos = db.Ciudades.Select(e => new { e.Id, e.Nombre }).ToList(); // Selecciona los datos que necesitas y los almacena en una lista
            var listaItems = new List<SelectListItem>(); //declaro mi DDL
            foreach (var c in datos)
            {
                listaItems.Add(new SelectListItem() { Value = c.Id.ToString(), Text = c.Nombre }); //Asigno elemento a elemento
            }            
            ViewBag.ddlCiudades = listaItems; //Guardo mi DDL en un ViewBag
            return View();
        }
        [HttpPost]
        public ActionResult AltaCliente(ClienteViewModel model) //Recibo como parametro mi modelo
        {
            var db = new Valkimia_challengeEntities();
            var datos = db.Ciudades.Select(e => new { e.Id, e.Nombre }).ToList(); // Selecciona los datos que necesitas y los almacena en una lista
            var listaItems = new List<SelectListItem>();
            foreach (var c in datos)
            {
                listaItems.Add(new SelectListItem() { Value = c.Id.ToString(), Text = c.Nombre });
            }
            ViewBag.ddlCiudades = listaItems;
            var lstClientes = from c in db.Clientes where c.Email == model.Email select c; //comparo el cliente ingresado con los de la bdd


            if (!ModelState.IsValid || lstClientes.Count() > 0) //Verifica que el modelo sea valido y ademas que el correo con el que se registra no este en BDD
            {
                if (lstClientes.Count() > 0)
                {
                    TempData["Mensaje"] = "El correo electronico ya existe en la base de datos, por favor ingrese otro correo.";
                }
                return View(model);
            }
            using (db) //hago uso de mi contexto
            {
                Guid newGuid = Guid.NewGuid(); //Generar un nuevo Guid que sera mi nuevo id en la tabla clientes al momento de la insercion
                Clientes objCliente = new Clientes(); //Creo mi objeto de tipo Clientes.
                objCliente.Id = newGuid;
                objCliente.Nombre = model.Nombre;
                objCliente.Apellido = model.Apellido;
                objCliente.Domicilio = model.Domicilio;
                objCliente.Email = model.Email;
                objCliente.Password = EncryptPassword.GetSHA256(model.Password); //Uso sha256 para realizar la encriptacion de la contraseña
                objCliente.IdCiudad = model.IdCiudad;
                objCliente.Habilitado = true; //Por defecto cuando se crea un nuevo cliente se pone en true el habilitado.
                db.Clientes.Add(objCliente); //Agrego mi objeto cliente
                db.SaveChanges(); //Guardo los cambios
            }
            return Redirect(Url.Content("~/Home/Index")); //Redirecciono al Home
        }


        public ActionResult ModificaCliente()
        {
            var db = new Valkimia_challengeEntities(); // Crea una instancia de tu DbContext
            var datos = db.Ciudades.Select(e => new { e.Id, e.Nombre }).ToList(); // Selecciona los datos que necesitas y los almacena en una lista
            var listaItems = new List<SelectListItem>();
            foreach (var c in datos)
            {
                listaItems.Add(new SelectListItem() { Value = c.Id.ToString(), Text = c.Nombre });
            }            
            ViewBag.ddlCiudades = listaItems;
            string idCliente = Convert.ToString(Session["Id"]);
            ClienteViewModel model = new ClienteViewModel();
            Guid id = Guid.Parse(idCliente);
            using (db)
            {
                var lstClientes = from c in db.Clientes where (c.Id) == id && c.Habilitado == true select c;
                Clientes oCliente = lstClientes.First();
                model.Id = oCliente.Id;
                model.Nombre = oCliente.Nombre;
                model.Apellido = oCliente.Apellido;
                model.Domicilio = oCliente.Domicilio;
                model.Email = oCliente.Email;               
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult ModificaCliente(ClienteViewModel model)
        {
            var db = new Valkimia_challengeEntities();
            var datos = db.Ciudades.Select(e => new { e.Id, e.Nombre }).ToList(); // Selecciona los datos que necesitas y los almacena en una lista
            var listaItems = new List<SelectListItem>();
            foreach (var c in datos)
            {
                listaItems.Add(new SelectListItem() { Value = c.Id.ToString(), Text = c.Nombre });
            }            
            ViewBag.ddlCiudades = listaItems;
            var lstClientes = from c in db.Clientes where c.Email == model.Email select c; //comparo el cliente ingresado con los de la bdd


            if (!ModelState.IsValid || lstClientes.Count() > 1)
            {
                if (lstClientes.Count() > 1)
                {
                    TempData["Mensaje"] = "El correo electronico ya existe en la base de datos, por favor ingrese otro correo.";
                }
                return View(model);
            }
            using (db)
            {
                var objCliente = db.Clientes.Find(model.Id); //encuentro el cliente
                objCliente.Nombre = model.Nombre;
                objCliente.Apellido = model.Apellido;
                objCliente.Domicilio = model.Domicilio;
                objCliente.Email = model.Email;
                objCliente.Password = EncryptPassword.GetSHA256(model.Password);
                objCliente.IdCiudad = model.IdCiudad;
                objCliente.Habilitado = true;
                db.Entry(objCliente).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Redirect(Url.Content("~/Home/Index"));
        }
        [HttpGet]
        public ActionResult EliminaCliente()
        {
            string idCliente = Convert.ToString(Session["Id"]); //Traigo el id del cliente
            ClienteViewModel model = new ClienteViewModel(); //Creo mi modelo
            Guid id = Guid.Parse(idCliente); //Casteo el id
            using (Valkimia_challengeEntities db = new Valkimia_challengeEntities())
            {
                var lstClientes = from c in db.Clientes where (c.Id) == id && c.Habilitado == true select c; //Verifico que sea el cliente
                Clientes oCliente = lstClientes.First();
                model.Id = oCliente.Id;
                model.Nombre = oCliente.Nombre;
                model.Apellido = oCliente.Apellido;
                model.Domicilio = oCliente.Domicilio;
                model.Email = oCliente.Email;
                model.Password = "";
            }
            return View(model);


        }
        [HttpPost]
        public ActionResult EliminaCliente(ClienteViewModel model)
        {
            try 
            {
                Valkimia_challengeEntities db = new Valkimia_challengeEntities();
                string passEncriptada = EncryptPassword.GetSHA256(model.Password); //Aplico metodo de encriptacion para comprobar que la contraseña que ingresa sea la misma que esta en bdd
                string email = model.Email;
                var lstClientes = from c in db.Clientes where c.Email == email && c.Password == passEncriptada select c; //comparo el cliente ingresado (email y pass) con los de la bdd
                if (lstClientes.Count() == 1) //Si es que es uno es porque lo que ingreso es lo correcto y se encuentra en bdd
                {
                    using (db)
                    {
                        var objCliente = db.Clientes.Find(model.Id);
                        objCliente.Password = EncryptPassword.GetSHA256(model.Password);
                        objCliente.Habilitado = false; //Coloco false para indicar que se elimino
                        db.Entry(objCliente).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        Session.Abandon();//Cierro la sesion del usuario                                             
                        return RedirectToAction("Index", "Login"); //Redirecciono al login
                    }
                }
                else
                {
                    TempData["Mensaje"] = "Ingreso de contraseña erroneo, ingrese nuevamente su contraseña.";
                    return View();
                }
                
            }
            catch 
            {
                return View();
            }
            
        }
    }
}