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
    [EnableCors(origins:"http://localhost:4200",headers: "*",methods: "*")]
    public class LoginController : ApiController
    {
        gladiatorEntities entities = new gladiatorEntities();
        [HttpPost]
        public HttpResponseMessage logincheck(tbl_users user)
        { 
            //result = entities.sp_logincheck(user.username).FirstOrDefault();
            var result = entities.sp_login(user.username);
            if (result == null || result.ToString().Length==0)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Username or password");
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        [HttpGet]
        public HttpResponseMessage getuser(string username)
        {
            try
            {
                sp_getuser_Result user = new sp_getuser_Result();
                user = entities.sp_getuser(username).FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Data can't be fetched");
            }
           
        }
        [HttpPut]
        public HttpResponseMessage updateuser(string username,tbl_users user)
        {
            DbContextTransaction transaction = entities.Database.BeginTransaction();
            try
            {
                tbl_users target = entities.tbl_users.Where(u =>username == u.username).FirstOrDefault();
                target.lastname = user.lastname;
                target.phone = user.phone;
                target.dob = user.dob;
                entities.SaveChanges();
                transaction.Commit();
                return Request.CreateResponse(HttpStatusCode.OK, target);
            }
            catch (Exception)
            {
                transaction.Rollback();
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "updation can't be perfomed");
            }
        }
    }
}