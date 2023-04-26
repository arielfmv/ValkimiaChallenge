using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Valkimia_App.Models.ViewModels;
using Valkimia_challenge.Models;

namespace Valkimia_challenge.Test
{
    [TestFixture]
    public class FacturaTest
    {        
        //private Valkimia_challengeEntities db;

        [Test]
        public void AgregarFacturaABDD()
        {
            string stringGuid = "80CCC26E-7C2D-45DD-B5D6-FE50B17162E1";
            Guid guid = new Guid(stringGuid);
            Facturas objFactura = new Facturas();
            var db = new Valkimia_challengeEntities();
            using (db)
            {
                objFactura.Id = Guid.NewGuid();
                objFactura.IdCliente = guid;
                objFactura.Fecha = Convert.ToDateTime("2023-04-25 00:00:00.000");
                objFactura.Detalle = "Test";
                objFactura.Importe = 1234;
                db.Facturas.Add(objFactura);
                db.SaveChanges();

            }                

            var facturaAgregada = db.Facturas.Find(objFactura.Id);
            Assert.IsNotNull(facturaAgregada);
            Assert.AreEqual(objFactura.Id, facturaAgregada.Id);
            Assert.AreEqual(objFactura.IdCliente, facturaAgregada.IdCliente);
            Assert.AreEqual(objFactura.Fecha, facturaAgregada.Fecha);
            Assert.AreEqual(objFactura.Detalle, facturaAgregada.Detalle);
            Assert.AreEqual(objFactura.Importe, facturaAgregada.Importe);
        }

    }
}