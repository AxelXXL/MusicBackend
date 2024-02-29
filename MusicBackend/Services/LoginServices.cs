using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicBackend.Models;
using MusicBackend.Services;

namespace MusicBackend.Services
{
    public class LoginServices : BaseServices
    {

        public String Register(RegisterModel user)
        {
            bool registrado;
            string message;

            if (user.Contrasena == user.ConfirmPassword)
                user.Contrasena = Security.Encrypt(user.Contrasena);
            else
                return "Las contraseñas no coinciden";

            var rol = db.tb_RolesPrivacidad.Where(x => x.ID_ROL == user.ID_Rol).FirstOrDefault();
            if (rol == null)
                return "El permiso asignado no existe";

            using (SqlConnection conn = new SqlConnection(db.Database.Connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_registerUser", conn);
                cmd.Parameters.AddWithValue("User", user.NombreUsuario);
                cmd.Parameters.AddWithValue("Password", user.Contrasena);
                cmd.Parameters.AddWithValue("ID_Rol", user.ID_Rol);
                cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                cmd.ExecuteNonQuery();

                registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                message = cmd.Parameters["Mensaje"].Value.ToString();
            }
            return message;
        }

    }
}