using System;
using System.ComponentModel;
using Android.Content;
using FlowersAndCandyCustomer.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ScrollView), typeof(ScrollViewExRenderer))]
namespace FlowersAndCandyCustomer.Droid.CustomRenderers
{
    public class ScrollViewExRenderer : ScrollViewRenderer
    {
        public ScrollViewExRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            try
            {
                base.OnElementChanged(e);

                if (e.OldElement != null || this.Element == null)
                {
                    return;
                }

                if (e.OldElement != null)
                {
                    e.OldElement.PropertyChanged -= OnElementPropetyChanged;
                }

                e.NewElement.PropertyChanged += OnElementPropetyChanged;
            }
            catch { }

        }

        private void OnElementPropetyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                if (ChildCount > 0)
                {
                    GetChildAt(0).HorizontalScrollBarEnabled = false;
                }
            }
            catch { }
        }
    }
}
