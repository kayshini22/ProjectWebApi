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
    public class ReturnTicketsController : ApiController
    {
        gladiatorEntities entities = new gladiatorEntities();
        [HttpGet]
        public List<sp_generateticketstep1_Result> returntickets(int id)
        {
            List<sp_generateticketstep1_Result> result = new List<sp_generateticketstep1_Result>();
            result = entities.sp_generateticketstep1(id).ToList();
            return result;
        }

    }
}
