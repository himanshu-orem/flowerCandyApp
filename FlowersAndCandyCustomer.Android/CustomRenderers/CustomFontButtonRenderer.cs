using System;
using Android.Content;
using Android.Graphics;
using FlowersAndCandyCustomer.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Button), typeof(CustomFontButtonRenderer))]
namespace FlowersAndCandyCustomer.Droid.CustomRenderers
{
    public class CustomFontButtonRenderer : ButtonRenderer
    {
        public CustomFontButtonRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (!string.IsNullOrEmpty(e.NewElement?.FontFamily))
            {
                try
                {
                    var font = Typeface.CreateFromAsset(Forms.Context.ApplicationContext.Assets, e.NewElement.FontFamily + ".ttf");
                    Control.Typeface = font;
                }
                catch (Exception)
                {
                    // An exception means that the custom font wasn't found.
                    // Typeface.CreateFromAsset throws an exception when it didn't find a matching font.
                    // When it isn't found we simply do nothing, meaning it reverts back to default.
                }
                if (Control != null)
                {
                    Control.SetAllCaps(false);
                }
            }
        }
    }
}
