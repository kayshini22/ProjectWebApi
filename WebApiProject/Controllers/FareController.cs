using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiProject.Models;

namespace WebApiProject.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class FareController : ApiController
    {
        gladiatorEntities entities = new gladiatorEntities();
        [HttpGet]
        public sp_getfare_Result getfare(int id)
        {
            sp_getfare_Result fare = new sp_getfare_Result();
            fare = entities.sp_getfare(id).FirstOrDefault();
            return fare;
        }

    }
}
