using FlowersAndCandyCustomer.DependencyInterface;
using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlowersAndCandyCustomer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LanguagePage : ContentPage
    {
        public LanguagePage()
        {
            InitializeComponent();
            BackgroundImage = "login.png";
            loadLatLng();
        }



        public async void loadLatLng()
        {
            try
            {
         
                var timeout = TimeSpan.FromSeconds(1);
                var locator = CrossGeolocator.Current;
                {
                    locator.DesiredAccuracy = 50;
                    int tm = 1000;
                    if (Device.OS == TargetPlatform.Android) { tm = 100000; }
                    var position = await locator.GetPositionAsync(tm);
                    if (position != null)
                    {
                    }
                }
               
            }
            catch (Exception ex)
            {
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        void clickEng(object sender, System.EventArgs e)
        {
            string lang = "en-US";
            Language objUser = new Language();
            objUser.LangKey = lang;
            App.lang = lang;
            App.Database.SaveLanguage(objUser);


            L10n.SetLocale();
            var netLanguage = DependencyService.Get<ILocale>().GetCurrent();
            AppResources.Culture = new CultureInfo(lang);

            App.Current.MainPage = new NavigationPage(new MainPage());
        }
        void clickArb(object sender, System.EventArgs e)
        {
            string lang = "ar-AE";
            Language objUser = new Language();
            objUser.LangKey = lang;
            App.lang = lang;
            App.Database.SaveLanguage(objUser);

            L10n.SetLocale();
            var netLanguage = DependencyService.Get<ILocale>().GetCurrent();
            AppResources.Culture = new CultureInfo(lang);

            App.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
}