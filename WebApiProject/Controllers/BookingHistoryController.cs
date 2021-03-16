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
    public class BookingHistoryController : ApiController
    {
        gladiatorEntities entities = new gladiatorEntities();
        [HttpGet]
        public List<sp_getBookingHistory_Result> getbookinghistory(string username, string status)
        {
            List<sp_getBookingHistory_Result> result = new List<sp_getBookingHistory_Result>();
            result = entities.sp_getBookingHistory(username, status).ToList();
            return result;
        }
    }
}
