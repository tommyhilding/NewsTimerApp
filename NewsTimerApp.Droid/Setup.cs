using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.IoC;
using NewsTimerApp.Core;

namespace NewsTimerApp.Droid
{
    public class Setup : MvxAppCompatSetup<App>
    {
        protected override IMvxIoCProvider InitializeIoC()
        {
            var provider = base.InitializeIoC();

           //provider.RegisterSingleton<IDialogs>(() => new Dialogs());

            return provider;
        }
    }
}