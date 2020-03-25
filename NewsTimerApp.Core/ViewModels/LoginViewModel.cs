using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using NewsTimerApp.Core.Models.Security;
using NewsTimerApp.Core.Services;
using NewsTimerApp.Core.Services.Security;

namespace NewsTimerApp.Core.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        private readonly IAccountService _accountService;
        private readonly IMvxNavigationService _navigationService;
        private readonly IUserDialogs _userDialogs;
        private string _userName;
        private string _password;

        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public LoginViewModel(
            IAccountService accountService,
            IMvxNavigationService navigationService,
            ISecureStorageService secureStorageService,
            IUserDialogs userDialogs)
        {
            _accountService = accountService;
            _navigationService = navigationService;
            _userDialogs = userDialogs;

            //TODO ta bort sen
            UserName = "tommy@netmine.se";
            Password = "abc.123";
        }

        public IMvxCommand LoginCommand => new MvxCommand(async () =>
        {
            await Login();
        });

        //public IMvxCommand ForgotPasswordCommand => new MvxCommand(async () =>
        //{
        //    await _navigationService.Navigate<ResetPasswordViewModel>();
        //});

        public IMvxCommand RegisterCommand => new MvxCommand(async () =>
        {
            await _navigationService.Navigate<RegisterViewModel>();
        });


        private async Task Login()
        {
            var credential = new Credential(UserName, Password);
            var account = await _accountService.Login(credential);

            if (account.IsAuthenticated)
            {
                await _navigationService.Navigate<HomeViewModel>();
            }
            else
            {
                _userDialogs.Alert("Inloggning misslyckades!");
            }
        }
    }
}
