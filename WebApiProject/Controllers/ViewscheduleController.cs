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
    public class ViewscheduleController : ApiController
    {
        gladiatorEntities entities = new gladiatorEntities();
        [HttpGet]
        public HttpResponseMessage Get_Schedule()
        {
            List<sp_getschedules_Result> result = null;
            result = entities.sp_getschedules().ToList();
            if (result == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something went wrong");
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpPut]
        public HttpResponseMessage Put(int id,[FromBody] tbl_schedules schedules)
        {
            DbContextTransaction transaction = entities.Database.BeginTransaction();
            try
            {
                tbl_schedules updateschedule = entities.tbl_schedules.Find(id);
                updateschedule.arrival_time = schedules.arrival_time;
                updateschedule.travel_date = schedules.travel_date;
                updateschedule.departure_time = schedules.departure_time;
                updateschedule.duration = schedules.duration;
                updateschedule.avail_economy_class = schedules.avail_economy_class;
                updateschedule.avail_business_class = schedules.avail_business_class;
                updateschedule.avail_first_class = schedules.avail_first_class;
                updateschedule.avail_premium_class = schedules.avail_premium_class;
                updateschedule.schedule_status = schedules.schedule_status;
                entities.SaveChanges();
                transaction.Commit();
                return Request.CreateResponse(HttpStatusCode.OK, "Successfully updated");
            }
            catch (Exception)
            {
                transaction.Rollback();
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Updation can't be performed");
            }
        }
    }
}
