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
    public class FlightController : ApiController
    {
        gladiatorEntities entities = new gladiatorEntities();
        [HttpGet]
        public List<sp_getflights_Result> getflights()
        {
            List<sp_getflights_Result> flights = new List<sp_getflights_Result>();
            flights = entities.sp_getflights().ToList<sp_getflights_Result>();
            return flights;
        }
        [HttpPost]
        public HttpResponseMessage Post(tbl_flights flight)
        {
            try
            {
                entities.tbl_flights.Add(flight);
                entities.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, flight);
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid details entered");

            }
        }
        [HttpPut]
        public HttpResponseMessage Put(int id, tbl_flights flight)
        {
            DbContextTransaction transaction = entities.Database.BeginTransaction();
            try
            {
                tbl_flights updateflight = entities.tbl_flights.Find(id);
                updateflight.flight_name = flight.flight_name;
                updateflight.source_destination = flight.source_destination;
                updateflight.target_destination = flight.target_destination;
                updateflight.economy_class_fare = flight.economy_class_fare;
                updateflight.business_class_fare = flight.business_class_fare;
                updateflight.first_class_fare = flight.first_class_fare;
                updateflight.premium_class_fare = flight.premium_class_fare;
                updateflight.capacity_economy_class = flight.capacity_economy_class;
                updateflight.capacity_business_class = flight.capacity_business_class;
                updateflight.capacity_first_class = flight.capacity_first_class;
                updateflight.capacity_premium_class = flight.capacity_premium_class;
                updateflight.status = flight.status;
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
