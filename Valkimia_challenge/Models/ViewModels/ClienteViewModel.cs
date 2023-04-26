using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Valkimia_App.Models.ViewModels
{
    public class ClienteViewModel
    {
        [Key]
        [Display(Name = "Id del Usuario")]
        [JsonProperty("Value")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Debe ingresar su nombre.")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Solo se aceptan letras.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Debe ingresar su apellido.")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Solo se aceptan letras.")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "Debe ingresar su direccion.")]
        public string Domicilio { get; set; }
        [Required(ErrorMessage = "Debe ingresar su correo.")]
        [EmailAddress]
        [Display(Name = "Correo electronico")]
        [JsonProperty("Text")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Debe ingresar una contraseña.")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "La contraseña debe tener al menos 8 caracteres, incluyendo al menos una letra mayúscula, una letra minúscula, un número y un carácter especial.")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Debe repetir la contraseña.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no son iguales")]
        [Display(Name = "Confirma contraseña")]
        public string ConfirmaPassword { get; set; }
        [Required(ErrorMessage = "Debe ingresar su Ciudad.")]
        [Display(Name = "Ciudad")]
        //[RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Solo se aceptan letras.")]
        public Guid IdCiudad { get; set; }

        public ClienteViewModel()
        {
            // Constructor predeterminado sin parámetros
        }
    }
    
}