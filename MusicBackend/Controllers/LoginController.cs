using MusicBackend.Models;
using MusicBackend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MusicBackend.Controllers
{
    public class LoginController : ApiController

    {
        #region Configuration

        private LoginServices _loginServices;

        public LoginController()
        {
            _loginServices = new LoginServices();
        }
        #endregion

        [Auth]
        [Route("api/Register")]
        [HttpPost]
        public String Register(RegisterModel user)
        {
            return _loginServices.Register(user);
        }
    }
}