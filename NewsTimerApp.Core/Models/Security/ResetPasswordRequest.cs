namespace NewsTimerApp.Core.Models.Security
{
    public class ResetPasswordRequest
    {
        public ResetPasswordRequest(string email)
        {
            Email = email;
        }
        public string Email { get; }
    }
}
