using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlowersAndCandyCustomer.DependencyInterface;
using FlowersAndCandyCustomer.iOS.DependencyInterface;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: Dependency(typeof(DownloadService))]
namespace FlowersAndCandyCustomer.iOS.DependencyInterface
{
   
    public class DownloadService : IDownloadService
    {
        public async void Share(ImageSource imageSource)
        {
            var handler = new ImageLoaderSourceHandler();
            var uiImage = await handler.LoadImageAsync(imageSource);
            var img = NSObject.FromObject(uiImage);
            var mess = NSObject.FromObject("Share Image");
            var activityItems = new[] { mess, img };
            var activityController = new UIActivityViewController(activityItems, null);
            var topController = UIApplication.SharedApplication.KeyWindow.RootViewController;

            while (topController.PresentedViewController != null)
            {
                topController = topController.PresentedViewController;
            }

            topController.PresentViewController(activityController, true, () => { });
        }
    }
}