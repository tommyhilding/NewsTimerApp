using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.ViewModels;
using NewsTimerApp.Core.Models.Newspapers;
using NewsTimerApp.Core.Services.Newspapers;

namespace NewsTimerApp.Core.ViewModels
{
    public class HomeViewModel : MvxViewModel
    {
        private readonly INewspaperApiService _newspaperApiService;
        private MvxObservableCollection<Newspaper> _newspapers;

        public HomeViewModel(INewspaperApiService newspaperApiService)
        {
            _newspaperApiService = newspaperApiService;
            _newspapers = new MvxObservableCollection<Newspaper>();
        }

        public MvxObservableCollection<Newspaper> Newspapers
        {
            get { return _newspapers; }
            set
            {
                _newspapers = value;
                RaisePropertyChanged(() => Newspapers);
            }
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            await LoadNewspapers();
        }

        private async Task LoadNewspapers()
        {
            var response = await _newspaperApiService.GetNewspapers();

            Newspapers.Clear();
            Newspapers.AddRange(response.Newspapers);
            await RaisePropertyChanged(() => Newspapers);
        }
    }
}
