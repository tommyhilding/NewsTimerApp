using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using NewsTimerApp.Core.Models.Newspapers;
using NewsTimerApp.Core.Services.Newspapers;

namespace NewsTimerApp.Core.ViewModels
{
    public class ReaderViewModel : MvxViewModel, IMvxViewModel<IdNavigationArg>
    {
        private readonly INewspaperApiService _newspaperApiService;
        private readonly ISessionApiService _sessionApiService;
        private readonly IMvxNavigationService _navigationService;
        private MvxObservableCollection<NewspaperCookie> _cookies;
        private Guid _newspaperId;

        public ReaderViewModel(
            INewspaperApiService newspaperApiService,
            ISessionApiService sessionApiService,
            IMvxNavigationService navigationService)
        {
            _newspaperApiService = newspaperApiService;
            _sessionApiService = sessionApiService;
            _navigationService = navigationService;
            _cookies = new MvxObservableCollection<NewspaperCookie>();
        }

        public MvxObservableCollection<NewspaperCookie> Cookies
        {
            get { return _cookies; }
            set
            {
                _cookies = value;
                RaisePropertyChanged(() => Cookies);
            }
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            //await LoadCookies();
        }

        public void Prepare(IdNavigationArg parameter)
        {
            _newspaperId = parameter.Id;

            var tcs = new TaskCompletionSource<NewspaperCookieResponse>();
            Task.Run(async () => tcs.SetResult(await _newspaperApiService.GetNewspaperCookies(_newspaperId)));
            var response = tcs.Task.Result;

            _sessionApiService.StartSessionTask(_newspaperId);

            Cookies.Clear();
            Cookies.AddRange(response.Cookies);
            RaisePropertyChanged(() => Cookies);
        }

        public void ViewSession()
        {
            _navigationService.Navigate<ReaderSessionViewModel>();
        }
    }
}
