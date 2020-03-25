using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NewsTimerApp.Core.Models;
using NewsTimerApp.Core.Models.Security;

namespace NewsTimerApp.Core.Services.Security
{
    public interface IAccountApiService
    {
        Task<CreateAccountResponse> CreateAccount(CreateAccountRequest model);
        Task<Account> Login(Credential credential);
        Task<TokenResponse> GetToken(Credential credential);
        Task<RefreshTokenModel> RefreshToken(RefreshTokenModel tokens);
        Task<Account> GetAccount();
        Task<ApiResponse> ResetPasswordRequest(ResetPasswordRequest request);
        Task<List<User>> GetConnectedUsers();
    }
}
