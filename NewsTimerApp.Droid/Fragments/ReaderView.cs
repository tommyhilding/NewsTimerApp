using System;
using Android.OS;
using Android.Support.Design.Widget;
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
    public class ReaderView : MvxFragment<ReaderViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.ReaderView, null);

            FloatingActionButton fab = view.FindViewById<FloatingActionButton>(Droid.Resource.Id.fab);
            fab.Click += FabOnClick;

            var cookieManager = CookieManager.Instance;

            foreach(var cookie in ViewModel.Cookies)
            {
                string cookieStr = string.Empty;
                cookieStr += cookie.Name;
                cookieStr += "=";
                cookieStr += cookie.Value;
                cookieStr += "; domain=";
                cookieStr += cookie.Domain;

                cookieManager.SetCookie("www.vn.se", cookieStr);
            }

            WebView webView = view.FindViewById<WebView>(Droid.Resource.Id.webView);
            webView.Settings.DomStorageEnabled = true;
            webView.SetWebViewClient(new WebViewClient());
            webView.Settings.JavaScriptEnabled = true;
            webView.LoadUrl("https://www.vn.se");


            return view;
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            ViewModel.ViewSession();
        }
    }
}