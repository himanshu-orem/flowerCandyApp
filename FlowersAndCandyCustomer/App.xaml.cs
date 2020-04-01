using FlowersAndCandyCustomer.Data;
using FlowersAndCandyCustomer.DependencyInterface;
using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.Resources;
using FlowersAndCandyCustomer.SellerViews;
using FlowersAndCandyCustomer.Views;
using Plugin.FirebasePushNotification;
using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace FlowersAndCandyCustomer
{
    public partial class App : Application
    {
        public static string DeviceToken = "";
        public static bool check = true;
        public static double ScreenHeight;
        public static double ScreenWidth;
        public static string Lng = "ar-AE";

        public static string lang = "";
        public App()
        {
            
            InitializeComponent();



            check = true;


            L10n.SetLocale();
            var netLanguage = DependencyService.Get<ILocale>().GetCurrent();
            AppResources.Culture = new CultureInfo(netLanguage);
            lang = netLanguage;

            if (Device.RuntimePlatform==Device.Android)
            {
                if (Application.Current.Properties.ContainsKey("FirebaseToken") && !string.IsNullOrEmpty(Application.Current.Properties["FirebaseToken"].ToString()) && !string.IsNullOrWhiteSpace(Application.Current.Properties["FirebaseToken"].ToString()) && Application.Current.Properties.ContainsKey("NotificationPermission") && Convert.ToInt32(Application.Current.Properties["NotificationPermission"]) == 1)
                {
                    DeviceToken = Application.Current.Properties["FirebaseToken"].ToString();
                }
                else
                {
                    DeviceToken = string.Empty;
                } 
            }



            Current.Resources = new ResourceDictionary();
            var navigationStyle = new Style(typeof(NavigationPage));
            var barTextColorSetter = new Setter { Property = NavigationPage.BarTextColorProperty, Value = Color.White };
            var barBackgroundColorSetter = new Setter { Property = NavigationPage.BarBackgroundColorProperty, Value = Color.FromHex("#FE1F78") };

            navigationStyle.Setters.Add(barTextColorSetter);
            navigationStyle.Setters.Add(barBackgroundColorSetter);

            Current.Resources.Add(navigationStyle);



            if (Device.OS == TargetPlatform.iOS)
            {
                Language langObj = App.Database.GetLanguage();
                if (langObj != null)
                {
                    Lng = langObj.LangKey;
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
            else
            {
                MainPage = new NavigationPage(new Views.IntializerPage());
            }
           // MainPage = new NavigationPage(new AddShopPage());


            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
                if (Device.RuntimePlatform == "Android")
                {
                    Application.Current.Properties["FirebaseToken"] = p.Token;
                    Application.Current.SavePropertiesAsync();
                    DeviceToken = p.Token;
                }
            };

            var aa = CrossFirebasePushNotification.Current.Token;
            if (Device.RuntimePlatform == "iOS")
            {
                DeviceToken = aa;
            }

            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                foreach (var item in p.Data)
                {
                    DependencyService.Get<ISetSoundNotification>().SetNotificationSound();

                    //if (item.Key == "type")
                    //{
                    //    string key = item.Value.ToString();
                    //    if(key=="1")
                    //    DependencyService.Get<ISetSoundNotification>().SetNotificationSound();
                    //    return;
                    //}
                }

            };
            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Opened");
                foreach (var data in p.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                }


            };
            CrossFirebasePushNotification.Current.OnNotificationAction += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Action");

                if (!string.IsNullOrEmpty(p.Identifier))
                {
                    System.Diagnostics.Debug.WriteLine($"ActionId: {p.Identifier}");
                    foreach (var data in p.Data)
                    {
                        System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                    }

                }

            };
        }



        static DbCls database;
        public static DbCls Database
        {
            get
            {
                if (database == null)
                {
                    try
                    {
                        database = new DbCls(DependencyService.Get<IFileHelper>().GetLocalFilePath("DbCls.db3"));
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return database;
            }
        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        
    }
}
