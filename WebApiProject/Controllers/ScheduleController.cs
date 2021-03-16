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
    public class ScheduleController : ApiController
    {
        gladiatorEntities entities = new gladiatorEntities();
        [HttpGet]
        public HttpResponseMessage Get_Flight()
        {
            List<sp_getflightid_Result1> result = null;
            result = entities.sp_getflightid().ToList<sp_getflightid_Result1>();
            if (result == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something went wrong");
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPost]
        public HttpResponseMessage Post(tbl_schedules schedules)
        {
            DbContextTransaction transaction = entities.Database.BeginTransaction();
            try
            {
                entities.tbl_schedules.Add(schedules);
                entities.SaveChanges();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Details not entered properly");
            }
            return Request.CreateResponse(HttpStatusCode.OK, schedules);
        }
    }
}
