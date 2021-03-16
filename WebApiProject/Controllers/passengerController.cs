using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Web.Http.Cors;
using WebApiProject.Models;

namespace WebApiProject.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class passengerController : ApiController
    {
        gladiatorEntities entities = new gladiatorEntities();
        [HttpPost]
        public HttpResponseMessage Post([FromUri]string username,tbl_passengers passenger)
        {
            try
            {
                List<sp_getpassengers_Result> result = new List<sp_getpassengers_Result>();
                result = entities.sp_getpassengers(username).ToList<sp_getpassengers_Result>();
                foreach(var item in result)
                {
                    if (item.name.ToLower() == passenger.name.ToLower())
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"passenger exists already");
                    }
                }
                entities.tbl_passengers.Add(passenger);
                entities.SaveChanges();
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "passenger can not be added");
            }
            return Request.CreateResponse(HttpStatusCode.Accepted, passenger);
        }
        [HttpGet]
        public IEnumerable<sp_getpassengers_Result> getpassengers(string username)
        {
            List<sp_getpassengers_Result> sp_Getpassengers_Results = new List<sp_getpassengers_Result>();
            sp_Getpassengers_Results = entities.sp_getpassengers(username).ToList();
            return sp_Getpassengers_Results;
        }
        [HttpGet]
        public HttpResponseMessage getpassengerdetails(int id)
        {
            try
            {
                sp_get_passenger_details_Result passenger = new sp_get_passenger_details_Result();
                passenger = entities.sp_get_passenger_details(id).FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, passenger);
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "invalid passenger id");
            }

        }
        [HttpDelete]
        public HttpResponseMessage deletepassenger(int passenger_id)
        {
            DbContextTransaction transaction = entities.Database.BeginTransaction();
            try
            {
                entities.sp_deletepassenger(passenger_id);
                entities.SaveChanges();
                transaction.Commit();
                return Request.CreateResponse(HttpStatusCode.OK,"successfully deleted");
            }
            catch (Exception)
            {
                transaction.Rollback();
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "delete can't be perfomed");
            }
        }
    }
}
