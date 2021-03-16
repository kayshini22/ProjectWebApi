using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiProject.Models
{
    public class NewPassword
    {
        public string username { get; set; }
        public string newPassword { get; set; }

        public NewPassword()
        {
        }
        public NewPassword(string username, string newPassword)
        {
            this.username = username;
            this.newPassword = newPassword;
        }
    }
}