using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using NewsTimerApp.Core.Models.Newspapers;
using NewsTimerApp.Core.Services.Newspapers;

namespace NewsTimerApp.Core.ViewModels
{
    public class ReaderSessionViewModel : MvxViewModel
    {
        private readonly ISessionApiService _sessionApiService;
        private readonly IMvxNavigationService _navigationService;

        public ReaderSessionViewModel(
            IMvxNavigationService navigationService,
            ISessionApiService sessionApiService)
        {
            _navigationService = navigationService;
            _sessionApiService = sessionApiService;
        }

        public Session Session
        {
            get
            {
                return _sessionApiService.Session;
            }
        }

        public IMvxAsyncCommand CloseCommand => new MvxAsyncCommand(async () => await _navigationService.Close(this));

        public IMvxAsyncCommand EndCommand => new MvxAsyncCommand(EndAsync);

        private async Task EndAsync()
        {
            this._sessionApiService.StopSessionTask();

            await _sessionApiService.SaveSession();

            await _navigationService.Close(this);
            await _navigationService.Navigate<HomeViewModel>();
        }
    }
}
