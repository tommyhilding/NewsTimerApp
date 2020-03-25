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
    public class RegisterViewModel : MvxViewModel
    {
        private readonly IAccountService _accountService;
        private readonly IUserDialogs _userDialogs;
        private readonly IMvxNavigationService _navigationService;
        private readonly ISecureStorageService _secureStorageService;

        #region Inputs
        //inputs
        private string _firstName;
        private string _lastName;
        private string _userName;
        private string _password;
        private string _confirmPassword;
        private bool _acceptTerms = false;

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

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { SetProperty(ref _confirmPassword, value); }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        public bool AcceptTerms
        {
            get { return _acceptTerms; }
            set { SetProperty(ref _acceptTerms, value); }
        }
        #endregion

        public RegisterViewModel(
            IAccountService accountService,
            IUserDialogs userDialogs,
            IMvxNavigationService navigationService,
            ISecureStorageService secureStorageService)
        {
            _accountService = accountService;
            _userDialogs = userDialogs;
            _navigationService = navigationService;
            _secureStorageService = secureStorageService;
        }

        public IMvxCommand GoToLoginCommand => new MvxCommand(async () =>
        {
            await _navigationService.Navigate<LoginViewModel>();
        });

        public IMvxCommand RegisterCommand => new MvxCommand(async () =>
        {
            await Register();
        });

        private async Task Register()
        {

            if (!IsValidEmail(UserName))
            {
                _userDialogs.Alert(
                  "Ett fel har uppstått",
                  "Ogilltig e-post",
                  "OK");

                return;
            }

            if (!PasswordConfirmMatch())
            {
                _userDialogs.Alert(
                  "Ett fel har uppstått",
                  "Lösenorden stämmer ej överens!",
                  "OK");

                return;
            }

            if (!AcceptTerms)
            {
                _userDialogs.Alert(
                   "Ett fel har uppstått",
                  "Du måste godkänna villkoren!",
                  "OK");

                return;
            }

            var credential = new Credential(UserName, Password);
            var createAccountRequest = new CreateAccountRequest
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = UserName,
                Password = Password,
                Culture = System.Globalization.CultureInfo.CurrentUICulture.Name
            };

            CreateAccountResponse createAccountResponse = await _accountService.CreateAccount(createAccountRequest);
            if (createAccountResponse.Success)
            {
                _userDialogs.Alert(
                   "Grattis",
                   "Ditt konto har blivit skapat",
                   "OK");

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
            else
            {
                if (createAccountResponse.Message != null)
                {
                    _userDialogs.Alert(string.Join(",", createAccountResponse.Message), "Något gick fel!", "OK");
                }
                else
                {
                    _userDialogs.Alert("Något gick fel!", "Något gick fel!", "OK");
                }
            }
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        bool PasswordConfirmMatch()
        {
            if (!string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ConfirmPassword) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                if (Password == ConfirmPassword)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
