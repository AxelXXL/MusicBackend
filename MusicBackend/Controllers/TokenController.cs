﻿using MusicBackend.Models;
using MusicBackend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MusicBackend.Controllers
{
    public class TokenController : ApiController
    {

        [Route("api/GetToken")]
        [HttpGet]
        public TokenResponseModel GenerateNewToken(Guid ID_App)
        {
            return new TokenResponseModel() { Token = Security.GenerateNewToken(ID_App) };
        }
    }
}