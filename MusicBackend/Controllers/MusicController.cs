using MusicBackend.Models;
using MusicBackend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

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
        [Route("api/Canciones")]
        [HttpGet]
        public List<tb_Cancion> GetCanciones()
        {
            return _musicServices.GetCanciones();
        }
    }
}