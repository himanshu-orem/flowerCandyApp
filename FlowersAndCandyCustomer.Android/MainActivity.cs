using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;
using Android.Support.V4.Content;
using Android;
using Android.Support.V4.App;
using Plugin.CurrentActivity;
using FFImageLoading.Forms.Droid;
using Plugin.FirebasePushNotification;
using Firebase;
using Android.Content;
using FlowersAndCandyCustomer.Views;

namespace FlowersAndCandyCustomer.Droid
{

    [Activity(Label = "Flower&Candy", Icon = "@drawable/splashic", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        const string permissionAF = Manifest.Permission.AccessFineLocation;
        const string permissionAC = Manifest.Permission.AccessCoarseLocation;

        const string permissionWS = Manifest.Permission.WriteExternalStorage;
        const string permissionRD = Manifest.Permission.ReadExternalStorage;
        const string permissionC = Manifest.Permission.Camera;
        const string permissionCon = Manifest.Permission.ReadContacts;

        const int RequestLocationId = 0;
        readonly string[] Permissions =
    {
       Manifest.Permission.AccessFineLocation,
         Manifest.Permission.AccessCoarseLocation,
          Manifest.Permission.WriteExternalStorage,
         Manifest.Permission.ReadExternalStorage,
          Manifest.Permission.Camera,
            Manifest.Permission.ReadContacts,
    };
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            FirebaseApp.InitializeApp(this);


            // fetch screen height and width
            var width = Resources.DisplayMetrics.WidthPixels;
            var height = Resources.DisplayMetrics.HeightPixels;
            var density = Resources.DisplayMetrics.Density;


            App.ScreenWidth = (width - 0.5f) / density;
            App.ScreenHeight = (height - 0.5f) / density;

            Rg.Plugins.Popup.Popup.Init(this, bundle);

            ImageCircleRenderer.Init();
            CachedImageRenderer.Init(true);
            // CrossCurrentActivity.Current.Init(this, bundle);

            Rg.Plugins.Popup.Popup.Init(this, bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            if (CheckSelfPermission(permissionAF) != (int)Permission.Granted)
            {
                RequestPermissions(Permissions, RequestLocationId);
            }
            if (CheckSelfPermission(permissionAC) != (int)Permission.Granted)
            {
                RequestPermissions(Permissions, RequestLocationId);
            }
            if (CheckSelfPermission(permissionWS) != (int)Permission.Granted)
            {
                RequestPermissions(Permissions, RequestLocationId);
            }
            if (CheckSelfPermission(permissionRD) != (int)Permission.Granted)
            {
                RequestPermissions(Permissions, RequestLocationId);
            }
            if (CheckSelfPermission(permissionC) != (int)Permission.Granted)
            {
                RequestPermissions(Permissions, RequestLocationId);
            }
            if (CheckSelfPermission(permissionCon) != (int)Permission.Granted)
            {
                RequestPermissions(Permissions, RequestLocationId);
            }
            global::Xamarin.FormsMaps.Init(this, bundle);

            

           
            LoadApplication(new App());
            FirebasePushNotificationManager.ProcessIntent(this, Intent);
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            FirebasePushNotificationManager.ProcessIntent(this, intent);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            if(Permissions.Length == permissions.Length)
            {
                IntializerPage.GetNotificationPermission();
            }
        }
        
    }
}

