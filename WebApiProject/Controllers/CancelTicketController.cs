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
    public class CancelTicketController : ApiController
    {
        gladiatorEntities entities = new gladiatorEntities();
        [HttpPut]
        public HttpResponseMessage cancelTicket(sp_getBookingHistory_Result tick)
        {
            DbContextTransaction transaction = entities.Database.BeginTransaction();
            try
            {
                tbl_tickets ticket = entities.tbl_tickets.Where(u => u.ticket_id == tick.ticket_id).FirstOrDefault();
                ticket.status = "cancelled";
                entities.SaveChanges();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not cancel the ticket");
            }
            return Request.CreateResponse(HttpStatusCode.Accepted, "Ticket Cancelled");
        }
    }
}
