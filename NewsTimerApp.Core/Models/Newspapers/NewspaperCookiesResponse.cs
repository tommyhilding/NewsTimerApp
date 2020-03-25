using System;
using System.Collections.Generic;
using System.Text;

namespace NewsTimerApp.Core.Models.Newspapers
{
    public class NewspaperCookieResponse
    {
        public NewspaperCookieResponse()
        {
            Cookies = new List<NewspaperCookie>();
        }
        public bool Success { get; set; }

        public List<NewspaperCookie> Cookies { get; set; }

        public string Message { get; set; }
    }
}
