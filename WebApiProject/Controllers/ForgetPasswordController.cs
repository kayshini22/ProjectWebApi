using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiProject.Models;
using System.Net.Mail;

namespace WebApiProject.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class ForgetPasswordController : ApiController
    {
        gladiatorEntities entities = new gladiatorEntities();
        [HttpGet]
        public HttpResponseMessage forgetPassword_Send_Mail(string username)
        {
            tbl_users user = entities.tbl_users.Where(t => t.username == username).FirstOrDefault();
            if (user == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid ID");
            }
            else
            {
                var token = Guid.NewGuid().ToString();
                MailAddress to = new MailAddress(user.username);
                MailAddress from = new MailAddress("infotechairlines@gmail.com");

                MailMessage message = new MailMessage(from, to);
                message.Subject = "Testing Mail";
                message.IsBodyHtml = true;
                //string url = "http:/localhost:4200/ResetPassword?id=" + student.STUID + "&token=" + token;
                message.Body = "Reseting <a href='http:/localhost:4200/ResetPassword?id=" + user.username + "&token=" + token + "' >Click!</a>";

                SmtpClient client = new SmtpClient("smtp.elasticemail.com", 2525)
                {
                    Credentials = new NetworkCredential("infotechairlines@gmail.com", "37222EFEE91DD0025E3E4E61F9D73DC039D8"),
                    EnableSsl = true
                };

                try
                {
                    client.Send(message);

                }
                catch (SmtpException ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
                }
                List<string> list = new List<string>();
                list.Add(Convert.ToString(user.username));
                list.Add(token);

                return Request.CreateResponse<List<string>>(HttpStatusCode.OK, list);
            }
        }
        [HttpPut]
        public HttpResponseMessage savePassword(tbl_users n)
        {
            tbl_users s = entities.tbl_users.Where(t => t.username == n.username).FirstOrDefault();
            if (s == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Data cannot be found");
            }
            else
            {
                s.password = n.password;
                entities.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "1");
            }
        }
    }
}
