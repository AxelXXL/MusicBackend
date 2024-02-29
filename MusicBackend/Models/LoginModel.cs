using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicBackend.Models
{
    public class LoginModel
    {
    }

    public class RegisterModel
    {
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public string ConfirmPassword { get; set; }
        public int ID_Rol { get; set; }
    }
}