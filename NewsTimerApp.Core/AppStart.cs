using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using NewsTimerApp.Core.Models.Security;
using NewsTimerApp.Core.Services.Security;
using NewsTimerApp.Core.ViewModels;

namespace NewsTimerApp.Core
{
    public class AppStart : MvxAppStart, IMvxAppStart
    {
        // private readonly IConnectionService _connectionService;
        public AppStart(
            IMvxApplication application,
            IMvxNavigationService navigationService
            ) : base(application, navigationService)
        {
    
        }

        protected override Task NavigateToFirstViewModel(object hint = null)
        {
            var accountService = Mvx.IoCProvider.Resolve<IAccountService>();
            var tcs = new TaskCompletionSource<Account>();
            Task.Run(async () => tcs.SetResult(await accountService.GetAccount()));
            var account = tcs.Task.Result;

            if (account.IsAuthenticated)
            {
                return NavigationService.Navigate<HomeViewModel>();
            }
            else
            {
                accountService.Logout();
                return NavigationService.Navigate<LoginViewModel>();
            }

            
        }
    }
}
