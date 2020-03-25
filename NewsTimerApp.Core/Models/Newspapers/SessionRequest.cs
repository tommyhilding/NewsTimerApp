using System;
using System.Collections.Generic;
using System.Text;

namespace NewsTimerApp.Core.Models.Newspapers
{
    public class SessionRequest
    {
        public int DurationInSeconds { get; set; }

        public DateTime Start { get; set; }

        public Guid NewspaperId { get; set; }
    }
}
