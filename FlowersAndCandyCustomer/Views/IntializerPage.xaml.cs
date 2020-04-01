using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.Resources;
using FlowersAndCandyCustomer.SellerViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlowersAndCandyCustomer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IntializerPage : ContentPage
    {
        public IntializerPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Device.BeginInvokeOnMainThread(() =>
            {
                onload();
            });

        }

        public static async void GetNotificationPermission()
        {
            var notificationPermission = await App.Current.MainPage.DisplayAlert(AppResources.notificationpopup_headertitle, AppResources.notificationpopup_title, AppResources.notificationpopup_Allow, AppResources.notificationpopup_DontAllow);
            if (notificationPermission)
            {
                Application.Current.Properties["NotificationPermission"] = 1;
                if (Application.Current.Properties.ContainsKey("FirebaseToken") && !string.IsNullOrEmpty(Application.Current.Properties["FirebaseToken"].ToString()) && !string.IsNullOrWhiteSpace(Application.Current.Properties["FirebaseToken"].ToString()))
                {
                    App.DeviceToken = Application.Current.Properties["FirebaseToken"].ToString();
                    DeviceTokken();
                }
            }
            else
            {
                App.DeviceToken = string.Empty;
                DeviceTokken();
            }
        }
        public static async void DeviceTokken()
        {
            try
            {
                string type = "android";

                LoggedInUser objUser = App.Database.GetLoggedInUser();

                string postData = "id=" + objUser.userId + "&deviceType=" + type + "&deviceToken=" + App.DeviceToken;
                var result = await CommonLib.UpdateToken(CommonLib.ws_MainUrl + "updateToken?" + postData);
                if (result.status == 1)
                {

                }
            }
            catch (Exception ex)
            {
            }
        }
        void onload()
        {
            Language langObj = App.Database.GetLanguage();
            if (langObj != null)
            {
                App.Lng = langObj.LangKey;
                LoggedInUser objUser = App.Database.GetLoggedInUser();
                if (objUser == null)
                {
                    App.Current.MainPage = new NavigationPage(new MainPage());
                }
                if (objUser != null)
                {
                    if (objUser.userType == "2")
                    {
                        if (objUser.is_shop == "0")
                        {
                            App.Current.MainPage = new NavigationPage(new AddShopPage());
                        }
                        else
                        {
                            App.Current.MainPage = new NavigationPage(new HomeMasterPage());
                        }
                    }
                    if (objUser.userType == "1")
                    {
                        App.Current.MainPage = new NavigationPage(new MainPage());
                    }
                }
            }
            else
            {
                App.Current.MainPage = new NavigationPage(new LanguagePage());
            }
            

        }
    }
}