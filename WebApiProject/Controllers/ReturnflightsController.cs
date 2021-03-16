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
    public class ReturnflightsController : ApiController
    {
        gladiatorEntities entities = new gladiatorEntities();
        [HttpGet]
        public HttpResponseMessage SearchreturnFlights(DateTime travel_date, string source_destination, string target_destination, int no_traveller)
        {
            try
            {
                List<sp_flight_search_Result> result = null;
                result = entities.sp_flight_search(travel_date, source_destination, target_destination, no_traveller).ToList();
                if (result == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No flights are scheduled");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No flights are scheduled");
            }
        }
    }
}
