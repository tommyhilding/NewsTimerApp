using Android.OS;
using Android.Views;
using Android.Webkit;
using MvvmCross.Droid.Support.Design;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using NewsTimerApp.Core.ViewModels;

namespace NewsTimerApp.Droid.Fragments
{
    [MvxDialogFragmentPresentation]
    public class ReaderSessionView  : MvxBottomSheetDialogFragment<ReaderSessionViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.ReaderSessionView, null);

            return view;
        }
    }
}