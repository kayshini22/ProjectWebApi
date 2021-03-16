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
    public class TicketsController : ApiController
    {
        gladiatorEntities entities = new gladiatorEntities();
        [HttpPost]
        public HttpResponseMessage addticket(tbl_tickets ticket)
        {
            DbContextTransaction transaction = entities.Database.BeginTransaction();
            try
            {
                ticket.status = "upcoming";
                ticket.gate = 2;
                entities.tbl_tickets.Add(ticket);
                entities.SaveChanges();
                transaction.Commit();
                return Request.CreateResponse(HttpStatusCode.OK, ticket);
            }
            catch (Exception)
            {
                transaction.Rollback();
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data entered");
            }
        }
        [HttpGet]
        public List<sp_generateticketstep1_Result> tickets (int id)
        {
            List<sp_generateticketstep1_Result> result = new List<sp_generateticketstep1_Result>();
            result = entities.sp_generateticketstep1(id).ToList();
            return result;
        }
    }
}
