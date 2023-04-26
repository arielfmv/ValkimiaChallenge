using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Valkimia_challenge.Models;

namespace Valkimia_challenge.Controllers
{
    public class ClientesController : ApiController
    {
        private Valkimia_challengeEntities db = new Valkimia_challengeEntities();

        // GET: api/Clientes
        public IHttpActionResult GetLista()
        {            
            var lista = db.Clientes.ToList();
            return Ok(lista); // Devolver la lista en formato JSON como respuesta de la API
        }
    }
}