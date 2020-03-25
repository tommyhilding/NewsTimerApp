using System;
using System.Collections.Generic;
using System.Text;

namespace NewsTimerApp.Core.Models.Security
{
    public class Credential
    {
        public Credential(string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
        }
        public string UserName { get; }
        public string Password { get; }
    }
}
