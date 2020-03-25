using System;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using NewsTimerApp.Core.ViewModels;

namespace NewsTimerApp.Core.Models.Newspapers
{
    public class Session : MvxNotifyPropertyChanged
    {
        private int _duration;
        public Guid NewspaperId { get; set; }
        
        public int Duaration
        {
            get { return _duration; }
            set 
            {
                _duration = value;
                RaisePropertyChanged(() => Duaration);
                RaisePropertyChanged(() => DuarationSec);
            }
        }
            
        public DateTime Start { get; set; }

        public string DuarationSec
        {
            get 
            { 
                return "Du har läst i " + _duration.ToString() + " sek."; 
            }
        }
    }
}
