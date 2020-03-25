using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NewsTimerApp.Core.Models;
using NewsTimerApp.Core.Models.Security;

namespace NewsTimerApp.Core.Services.Security
{
    public interface IAccountService
    {
        bool IsConnected { get; set; }
        Account Account { get; }
        Task<CreateAccountResponse> CreateAccount(CreateAccountRequest model);
        Task<Account> Login(Credential credential);
        Task<Account> GetAccount();
        void Logout();
        Task<ApiResponse> ResetPasswordRequest(ResetPasswordRequest request);
        Task<List<User>> GetConnectedUsers();
    }
}
