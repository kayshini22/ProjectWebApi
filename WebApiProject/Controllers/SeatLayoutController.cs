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
    public class SeatLayoutController : ApiController
    {
        gladiatorEntities entities = new gladiatorEntities();
        [HttpGet]
        public HttpResponseMessage SeatLayout(int id)
        {
            try
            {
                List<sp_getBookedSeats_Result> result = new List<sp_getBookedSeats_Result>(); ;
                result = entities.sp_getBookedSeats(id).ToList();
                if (result.Count == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No seats booked");

                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Exception occured");
            }
        }
    }
}
