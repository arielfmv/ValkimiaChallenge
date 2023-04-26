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
    public class FacturasController : ApiController
    {
        // POST: api/Facturas
        [ResponseType(typeof(Facturas))]
        public IHttpActionResult PostFacturas(Facturas model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (Models.Valkimia_challengeEntities db = new Valkimia_challengeEntities())
            {
                var objFactura = new Models.Facturas();
                Guid newGuid = Guid.NewGuid();
                objFactura.Id = newGuid;
                objFactura.IdCliente = model.IdCliente;
                objFactura.Fecha = model.Fecha;
                objFactura.Detalle = model.Detalle;
                objFactura.Importe = Math.Truncate(model.Importe * 100) / 100;
                db.Facturas.Add(objFactura);
                db.SaveChanges();
            }            

            return CreatedAtRoute("DefaultApi", new { id = model.Id }, model);
        } 
        
    }
}