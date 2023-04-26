using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Valkimia_App.Models.ViewModels
{
    public class FacturaViewModel
    {
        [Key]
        [Display(Name = "Id de Factura")]
        public Guid Id { get; set; }
        [Display(Name = "Correo del Cliente")]
        public Guid IdCliente { get; set; }
        [Required(ErrorMessage = "Debe ingresar la fecha de la factura")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Range(typeof(DateTime), "01/01/2023", "31/12/2023")]
        public DateTime Fecha { get; set; }
        [Required(ErrorMessage = "Debe ingresar el detalle de la factura.")]
        public string Detalle { get; set; }
        [Required(ErrorMessage = "Debe ingresar un importe")]
        [DataType(DataType.Currency)]
        [Range(0, 1000000)]
        public decimal Importe { get; set; }
    }
}