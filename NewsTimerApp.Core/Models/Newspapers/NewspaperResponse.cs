using System;
using System.Collections.Generic;
using System.Text;

namespace NewsTimerApp.Core.Models.Newspapers
{
    public class NewspaperResponse
    {
        public NewspaperResponse()
        {
            Newspapers = new List<Newspaper>();
        }
        public bool Success { get; set; }

        public List<Newspaper> Newspapers { get; set; }
    }
}
