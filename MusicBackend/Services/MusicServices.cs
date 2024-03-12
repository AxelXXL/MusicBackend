using MusicBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using Antlr.Runtime.Tree;


namespace MusicBackend.Services
{
    public class MusicServices : ApiController
    {
        private readonly BD_LOSS_SOUNDSEntities db = new BD_LOSS_SOUNDSEntities();

        public HttpResponseMessage GetCanciones(int? ID_Cancion)
        {
            try
            {

                IEnumerable<object> canciones;
                if (ID_Cancion != null)
                {
                    canciones = db.tb_Cancion.Where(x => x.ID_CANCION == ID_Cancion).Select(x => new
                    {
                        x.ID_CANCION,
                        x.Nombre_Cancion,
                        x.Numero_Cancion,
                        x.Duracion_Cancion,
                        x.ID_ARTISTA,
                        x.ID_ALBUM,
                        x.Ruta_Audio,
                        x.Caratula_Cancion
                    }).ToList();
                }
                else
                {
                    canciones = db.tb_Cancion.Select(x => new
                    {
                        x.ID_CANCION,
                        x.Nombre_Cancion,
                        x.Numero_Cancion,
                        x.Duracion_Cancion,
                        x.ID_ARTISTA,
                        x.ID_ALBUM,
                        x.Ruta_Audio,
                        x.Caratula_Cancion
                    }).ToList();
                }

                if (canciones.Count() == 0)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent)
                    {
                        Content = new StringContent("No se encontro la canción solicitada.")
                    };
                }

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<List<object>>(canciones.Cast<object>().ToList(), new JsonMediaTypeFormatter())
                };
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