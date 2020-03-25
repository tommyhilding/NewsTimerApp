namespace NewsTimerApp.Core.Models.Security
{
    public class RefreshTokenModel
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
