using System;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using NewsTimerApp.Core.ViewModels;

namespace NewsTimerApp.Core.Models.Newspapers
{
    public class Newspaper
    {
        protected readonly IMvxNavigationService _navigationService;

        public Newspaper()
        {
            _navigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Url { get; set; }

        public string Thumbnail 
        { 
            get
            {
                return "@drawable/vn";
            }
        }

        

        public IMvxAsyncCommand ReadCommand => new MvxAsyncCommand(ReadAsync);

        private async Task ReadAsync()
        {
            await _navigationService.Navigate<ReaderViewModel, IdNavigationArg>(new IdNavigationArg { Id = this.Id });
        }
    }
}
