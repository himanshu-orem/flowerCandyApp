using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;

namespace FlowersAndCandyCustomer.Droid
{
    [Activity(Label = "Flower&Candy", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        private static int SPLASH_TIME = 1 * 1000;// 1 seconds
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RequestWindowFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.Splash);

            try
            {
                new Handler().PostDelayed(() =>
                {
                    var intent = new Intent(this, typeof(MainActivity));
                    StartActivity(intent);
                    Finish();

                }, SPLASH_TIME);

            }

            catch (Exception)
            {


            }

        }
    }
}
