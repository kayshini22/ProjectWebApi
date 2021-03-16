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
    public class RegisterController : ApiController
    {
        gladiatorEntities entities = new gladiatorEntities();
        public HttpResponseMessage Post(tbl_users user)
        {
            try
            {
                entities.tbl_users.Add(user);
                entities.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Accepted, user);
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Username already exists in database");
            }
        }
    }
}
