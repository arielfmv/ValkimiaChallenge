using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Valkimia_App.Models.ViewModels;
using Valkimia_challenge.Models;

namespace Valkimia_challenge.Controllers
{
    public class FacturaController : Controller
    {
        // GET: Factura
        [HttpGet]
        public async Task<ActionResult> AltaFactura()
        {
            using (var httpClient = new HttpClient())//Creo variable para manejar peticiones HTTP
            {
                using (var response = await httpClient.GetAsync("https://localhost:44381/api/Clientes")) //para enviar sol. Get 0
                {
                    string apiResponse = await response.Content.ReadAsStringAsync(); //almaceno la respuesta http.
                    var data = JsonConvert.DeserializeObject<List<Clientes>>(apiResponse); //Convierto JSON en una lista de objetos.
                    var selectList = data.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Email }).ToList(); //lleno mi list
                    ViewBag.ListaClientes = selectList; //almaceno en viewbag para hacer uso posterior
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AltaFactura(FacturaViewModel model)
        {            
            using (var httpClient = new HttpClient())  
            {
                using (var response = await httpClient.GetAsync("https://localhost:44381/api/Clientes"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<Clientes>>(apiResponse);
                    var selectList = data.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Email }).ToList();
                    ViewBag.ListaClientes = selectList;
                }
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var httpClient = new HttpClient())
            {
                var requestUri = "https://localhost:44381/api/Facturas";
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"); //crea un obj que representa los datos que se envian en sol http
                var response = await httpClient.PostAsync(requestUri, content); //almaceno la respuesta http 
                if (!response.IsSuccessStatusCode)
                {
                    return View("Error");
                }
            }            
            return Redirect(Url.Content("~/Home/Index"));
        }
    }
}