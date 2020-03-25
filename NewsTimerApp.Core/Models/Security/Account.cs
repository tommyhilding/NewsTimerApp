using System;
using NewsTimerApp.Core.Models.Enum;

namespace NewsTimerApp.Core.Models.Security
{
    public class Account
    {
        public Account()
        {

        }

        public Guid UserId { get; set; }

        public Guid ApiKey { get; set; }

        public string UserName { get; set; }

        public bool IsAuthenticated { get; set; }

        public bool IsActive { get; set; }

        public UserRole Role { get; set; }

        public bool MarketingActive { get; set; }

        public string Culture { get; set; }
    }
}
