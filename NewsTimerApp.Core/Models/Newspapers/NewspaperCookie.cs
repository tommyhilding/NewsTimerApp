using System;
using System.Collections.Generic;
using System.Text;

namespace NewsTimerApp.Core.Models.Newspapers
{
    public class NewspaperCookie
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Value { get; set; }

        public string Domain { get; set; }
    }
}
