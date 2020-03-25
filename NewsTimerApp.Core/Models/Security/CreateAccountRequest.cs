namespace NewsTimerApp.Core.Models.Security
{
    public class CreateAccountRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Culture { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
