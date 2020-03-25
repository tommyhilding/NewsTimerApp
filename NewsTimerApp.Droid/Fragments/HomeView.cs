using Android.OS;
using Android.Views;
using Android.Webkit;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using NewsTimerApp.Core.ViewModels;

namespace NewsTimerApp.Droid.Fragments
{
    [MvxFragmentPresentation(
        ActivityHostViewModelType = typeof(MainViewModel),
        FragmentContentId = Resource.Id.content_frame,
        AddToBackStack = true,
        EnterAnimation = Resource.Animation.slideInRight,
        ExitAnimation = Resource.Animation.slideOutLeft,
        PopEnterAnimation = Resource.Animation.slideInLeft,
        PopExitAnimation = Resource.Animation.slideOutRight)]
    public class HomeView : MvxFragment<HomeViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.HomeView, null);

            
            return view;
        }
    }
}