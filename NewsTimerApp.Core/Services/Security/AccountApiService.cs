using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NewsTimerApp.Core.Models;
using NewsTimerApp.Core.Models.Security;

namespace NewsTimerApp.Core.Services.Security
{
    public class AccountApiService : BaseApiService, IAccountApiService
    {
        public AccountApiService(ISecureStorageService secureStorageService) : base(secureStorageService)
        {

        }

        public async Task<CreateAccountResponse> CreateAccount(CreateAccountRequest model)
        {
            return await PostAsync<CreateAccountResponse>("myHobby/createAccount", model);
        }
        public async Task<Account> Login(Credential credential)
        {
            return await PostAsync<Account>("account/login", credential);
        }

        public async Task<TokenResponse> GetToken(Credential credential)
        {
            var formData = new Dictionary<string, string>
            {
                {"grant_type", "password"},
                {"username", credential.UserName},
                {"password", credential.Password},
            };

            return await PostFormUrlEncodedContentAsync<TokenResponse>("token", formData);
        }

        public async Task<RefreshTokenModel> RefreshToken(RefreshTokenModel tokens)
        {
            var formData = new Dictionary<string, string>
            {
                {"accessToken", tokens.AccessToken},
                {"refreshToken", tokens.RefreshToken},
            };

            return await PostFormUrlEncodedContentAsync<RefreshTokenModel>("hobbyConnect/refreshToken", formData);
        }

        public async Task<Account> GetAccount()
        {
            var account = await GetAsync<Account>("account/userInfo");
            return account ?? new Account();
        }

        public async Task<ApiResponse> ResetPasswordRequest(ResetPasswordRequest request)
        {
            return await PostAsync<ApiResponse>("account/resetPasswordRequest", request);
        }

        public async Task<List<User>> GetConnectedUsers()
        {
            var users = await GetAsync<List<User>>("hobbyConnect/connectedUsers");
            return users ?? new List<User>();
        }
    }
}
