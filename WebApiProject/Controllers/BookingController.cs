using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiProject.Models;

namespace WebApiProject.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class BookingController : ApiController
    {
        gladiatorEntities entities = new gladiatorEntities();
        [HttpPost]
        public HttpResponseMessage addbooking(tbl_booking booking)
        {
            DbContextTransaction transaction = entities.Database.BeginTransaction();
            try
            {
                booking.status = "confirmed";
                entities.tbl_booking.Add(booking);
                entities.SaveChanges();
                transaction.Commit();
                return Request.CreateResponse(HttpStatusCode.OK, booking);
            }
            catch (Exception)
            {
                transaction.Rollback();
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data entered");
            }
        }
    }
}
