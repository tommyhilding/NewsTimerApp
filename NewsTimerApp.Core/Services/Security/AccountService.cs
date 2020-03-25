using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NewsTimerApp.Core.Models;
using NewsTimerApp.Core.Models.Security;

namespace NewsTimerApp.Core.Services.Security
{
    public class AccountService : IAccountService
    {
        private readonly IAccountApiService _accountApiService;
        private readonly ISecureStorageService _secureStorageService;

        public bool IsConnected { get; set; }
        public Account Account { get; private set; }

        public AccountService(
            IAccountApiService accountApiService,
            ISecureStorageService secureStorageService)
        {
            _accountApiService =  accountApiService;
            _secureStorageService = secureStorageService;
            Account = new Account();
           
        }
        public async Task<CreateAccountResponse> CreateAccount(CreateAccountRequest model)
        {
            model.Culture = System.Globalization.CultureInfo.CurrentUICulture.Name;
            return await _accountApiService.CreateAccount(model);
        }

        public async Task<Account> Login(Credential credential)
        {
            var tokens = await _accountApiService.GetToken(credential);
            if (tokens.UserName != null)
            {
                await _secureStorageService.SetAsync(Constants.AccessToken, tokens.AccessToken);
                await _secureStorageService.SetAsync(Constants.RefreshToken, tokens.RefreshToken);
                await _secureStorageService.SetAsync(Constants.IsRegistered, "true");
                await _secureStorageService.SetAsync(Constants.Email, credential.UserName);
            }
            else
            {
                ClearSavedTokens();
            }
            await GetAccount();
            return Account;
        }

        public async Task<Account> GetAccount()
        {
            Account = await _accountApiService.GetAccount();
            return Account;
        }

        public void Logout()
        {
            ClearSavedTokens();
            _secureStorageService.Remove(Constants.Email);
        }

        private void ClearSavedTokens()
        {
            _secureStorageService.Remove(Constants.AccessToken);
            _secureStorageService.Remove(Constants.RefreshToken);
        }

        public async Task<ApiResponse> ResetPasswordRequest(ResetPasswordRequest request)
        {
            return await _accountApiService.ResetPasswordRequest(request);
        }

        public async Task<List<User>> GetConnectedUsers()
        {
            return await _accountApiService.GetConnectedUsers();
        }
    }
}
