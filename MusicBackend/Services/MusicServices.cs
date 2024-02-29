using MusicBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Newtonsoft.Json;


namespace MusicBackend.Services
{
    public class MusicServices : BaseServices
    {

        public List<tb_Cancion> GetCanciones()
        {
            return db.tb_Cancion.ToList();
        }


    }
}