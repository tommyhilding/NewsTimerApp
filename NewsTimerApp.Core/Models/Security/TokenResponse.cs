using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NewsTimerApp.Core.Models.Security
{
    [JsonObject]
    public class TokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresInSeconds { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }

        [JsonProperty(".issued")]
        public DateTime? IssuedAt { get; set; }

        [JsonProperty(".expires")]
        public DateTime? ExpiresAt { get; set; }
    }
}
