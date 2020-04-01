using System;
using Android.Content;
using Android.Graphics;
using FlowersAndCandyCustomer.CustomControls;
using FlowersAndCandyCustomer.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LineLabel), typeof(CustomLabelRenderer_Droid))]
namespace FlowersAndCandyCustomer.Droid.CustomRenderers
{
    public class CustomLabelRenderer_Droid : LabelRenderer
    {
        public CustomLabelRenderer_Droid(Context context) : base(context)
        {

        }

        protected LineLabel LineSpacingLabel { get; private set; }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                this.LineSpacingLabel = (LineLabel)this.Element;
            }

            if (!string.IsNullOrEmpty(e.NewElement?.StyleId))
            {
                var font = Typeface.CreateFromAsset(Android.App.Application.Context.ApplicationContext.Assets, e.NewElement.StyleId + ".ttf");

                Control.Typeface = font;

                var lineSpacing = this.LineSpacingLabel.LineSpacing;

                this.Control.SetLineSpacing(1f, (float)lineSpacing);

                this.UpdateLayout();
            }
        }
    }
}
