using MusicBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicBackend.Controllers
{
    public class BaseController : Controller
    {
        public BD_LOSS_SOUNDSEntities db = new BD_LOSS_SOUNDSEntities();
    }
}