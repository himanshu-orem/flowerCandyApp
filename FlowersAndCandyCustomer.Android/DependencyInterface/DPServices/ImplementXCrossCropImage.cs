using System;
using System.Threading;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using FlowersAndCandyCustomer.DependencyInterface;
using FlowersAndCandyCustomer.Droid.SourceCode.DPServices;
using FlowersAndCandyCustomer.Droid.SourceCode.XCropImage;
using FlowersAndCandyCustomer.SourceCode.Utilites.Events;
using Plugin.CurrentActivity;
using Xamarin.Forms;
 
 
 
 

[assembly: Dependency(typeof(ImplementXCrossCropImage))]

namespace FlowersAndCandyCustomer.Droid.SourceCode.DPServices
{
    public class ImplementXCrossCropImage : IXCrossCropImage
    {
        #region cropimage

        private int GetRequestId()
        {
            var id = _requestId;
            if (_requestId == int.MaxValue)
                _requestId = 0;
            else
                _requestId++;

            return id;
        }

        private int _requestId;
        private TaskCompletionSource<byte[]> _completionSource;

        public Task<byte[]> CropImageFromOriginalToBytes(string filePath)
        {
            var id = GetRequestId();

            var ntcs = new TaskCompletionSource<byte[]>(id);
            if (Interlocked.CompareExchange(ref _completionSource, ntcs, null) != null)
            {
#if DEBUG
                throw new InvalidOperationException("Only one operation can be active at a time");
#else
                return null;
#endif
            }

            var _context = Android.App.Application.Context;

            var intent = new Intent(_context, typeof(CropImage));
            intent.PutExtra("image-path", filePath);
            intent.PutExtra("scale", true);

            //event
            EventHandler<XViewEventArgs> handler = null;
            handler = (s, e) =>
            {
                var tcs = Interlocked.Exchange(ref _completionSource, null);

                CropImage.MediaCroped -= handler;
                tcs.SetResult((e.CastObject as Bitmap)?.BitmapToBytes());
            };

            CropImage.MediaCroped += handler;
            _context.StartActivity(intent);

            return _completionSource.Task;
        }
        #endregion
    }
}