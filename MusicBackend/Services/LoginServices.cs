using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MusicBackend.Models;
using MusicBackend.Services;
using System.Net.Http.Formatting;

namespace MusicBackend.Services
{
    public class LoginServices : ApiController
    {
        private readonly BD_LOSS_SOUNDSEntities db = new BD_LOSS_SOUNDSEntities();

        public HttpResponseMessage Register(RegisterModel user)
        {
            bool registrado;
            string message;

            if (user.Contrasena == user.ConfirmPassword)
            {
                user.Contrasena = Security.Encrypt(user.Contrasena);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.Conflict)
                {
                    Content = new StringContent("Las contraseñas no coinciden.")
                };
            }


            var rol = db.tb_RolesPrivacidad.Where(x => x.ID_ROL == user.ID_Rol).FirstOrDefault();
            if (rol == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("El permiso asignado no existe.")
                };
            }

            using (SqlConnection conn = new SqlConnection(db.Database.Connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_registerUser", conn);
                cmd.Parameters.AddWithValue("User", user.NombreUsuario);
                cmd.Parameters.AddWithValue("Password", user.Contrasena);
                cmd.Parameters.AddWithValue("ID_Rol", 3);
                cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                cmd.ExecuteNonQuery();

                registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                message = cmd.Parameters["Mensaje"].Value.ToString();
            }

            if (registrado)
            {
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(message)
                };
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.Conflict)
                {
                    Content = new StringContent(message)
                };
            }
        }


        [System.Web.Http.HttpPost]
        public HttpResponseMessage Login(LoginResponseModel user)
        {
            try
            {
                user.Contrasena = Security.Encrypt(user.Contrasena);

                using (SqlConnection cn = new SqlConnection(db.Database.Connection.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", cn);
                    cmd.Parameters.AddWithValue("User", user.Nombre_Usuario);
                    cmd.Parameters.AddWithValue("Password", user.Contrasena);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();

                   
                }

                if (user.Nombre_Usuario != null)
                {
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new ObjectContent<LoginResponseModel>(user, new JsonMediaTypeFormatter())
                    };
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Usuario no encontrado.")
                    };
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Ocurrió un error inesperado. Más información: " + ex.Message)
                };
            }
        }


    }
}