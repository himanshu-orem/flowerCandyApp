using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net;
using System.IO;
using System.Text;
using Environment = System.Environment;
using FlowersAndCandyCustomer.DependencyInterface;
using FlowersAndCandyCustomer.Droid.DependencyInterface;
using Xamarin.Forms;
using Plugin.CurrentActivity;
using Android.Graphics;
using Xamarin.Forms.Platform.Android;
using Android.Support.V4.Content;

[assembly: Dependency(typeof(DownloadService))]
namespace FlowersAndCandyCustomer.Droid.DependencyInterface
{
    public class DownloadService : IDownloadService
    {
        public async void Share(ImageSource imageSource)
        {
            try
            {
                var intent = new Intent(Intent.ActionSend);

                intent.SetType("image/jpeg");

                IImageSourceHandler handler;

                if (imageSource is FileImageSource)
                {
                    handler = new FileImageSourceHandler();
                }
                else if (imageSource is StreamImageSource)
                {
                    handler = new StreamImagesourceHandler(); // sic
                }
                else if (imageSource is UriImageSource)
                {
                    handler = new ImageLoaderSourceHandler(); // sic
                }
                else
                {
                    throw new NotImplementedException();
                }

                var bitmap = await handler.LoadImageAsync(imageSource, Android.App.Application.Context);

                Java.IO.File path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures
                    + Java.IO.File.Separator + "MyImage.jpg");

                using (System.IO.FileStream os = new System.IO.FileStream(path.AbsolutePath, System.IO.FileMode.Create))
                {
                    bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, os);
                }

                intent.AddFlags(ActivityFlags.GrantReadUriPermission);
                intent.AddFlags(ActivityFlags.GrantWriteUriPermission);
                intent.PutExtra(Intent.ExtraStream, FileProvider.GetUriForFile(Android.App.Application.Context, "com.orem.fcCustomer.fileprovider", path));

                Android.App.Application.Context.StartActivity(Intent.CreateChooser(intent, "Share Image"));

            }
            catch (Exception ex)
            {

            }
        }
    }
}