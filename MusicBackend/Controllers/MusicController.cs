using MusicBackend.Models;
using MusicBackend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MusicBackend.Controllers
{
    public class MusicController : ApiController
    {
        #region Configurations
        private MusicServices _musicServices;

        public MusicController()
        {
            _musicServices = new MusicServices();
        }
        #endregion

        [Auth]
        [System.Web.Http.Route("api/Canciones")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetCanciones(int? ID_Cancion)
        {
            return _musicServices.GetCanciones(ID_Cancion);
        }
    }
}