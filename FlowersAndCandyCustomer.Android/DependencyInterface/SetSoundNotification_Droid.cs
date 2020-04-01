using FlowersAndCandyCustomer.DependencyInterface;
using Android.App;
using Android.Content;
using Android.OS;
using FlowersAndCandyCustomer.Droid.DependencyInterface;
using Xamarin.Forms;
using Android.Media;
using System.IO;
using Android.Net;

[assembly: Dependency(typeof(SetSoundNotification_Droid))]
namespace FlowersAndCandyCustomer.Droid.DependencyInterface
{
    public class SetSoundNotification_Droid: ISetSoundNotification
    {
        public void SetNotificationSound()
        {
            

            try
            {
                Uri defaultSoundUri = Uri.Parse("android.resource://" + Android.App.Application.Context.PackageName + "/" + Resource.Raw.notifysound);
                Ringtone r = RingtoneManager.GetRingtone(Android.App.Application.Context, defaultSoundUri);
                r.Play();
            }
            catch (System.Exception e)
            {
            }

           

        }
    }
}