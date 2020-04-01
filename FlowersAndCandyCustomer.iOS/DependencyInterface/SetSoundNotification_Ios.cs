using System;
using System.Threading;
using AudioToolbox;
using FlowersAndCandyCustomer.DependencyInterface;
using FlowersAndCandyCustomer.iOS.DependencyInterface;
using FlowersAndCandyCustomer.Models;
using Foundation;
using Xamarin.Forms;
[assembly: Dependency(typeof(SetSoundNotification_Ios))]
namespace FlowersAndCandyCustomer.iOS.DependencyInterface
{
    public class SetSoundNotification_Ios : ISetSoundNotification
    {
        NSUrl url;
        SystemSound ss;
        public void SetNotificationSound()
        {
            try
            {
                url = NSUrl.FromFilename("notifysound.mp3");
                ss = new SystemSound(url);
                ss.PlayAlertSound();
            }
            catch (System.Exception e)
            {
            }
        }
    }
}