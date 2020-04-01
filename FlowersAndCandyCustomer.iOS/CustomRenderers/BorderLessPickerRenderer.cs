using System;
using System.ComponentModel;
using CoreAnimation;
using CoreGraphics;
using FlowersAndCandyCustomer.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Picker), typeof(BorderLessPickerRenderer))]
namespace FlowersAndCandyCustomer.iOS.CustomRenderers
{
    public class BorderLessPickerRenderer : PickerRenderer
    {
        private CALayer _borderLayer;

        public BorderLessPickerRenderer()
        {
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            Control.Layer.BorderWidth = 0;
            Control.BorderStyle = UITextBorderStyle.None;

            var element = Element as Picker;
            if (element == null)
                return;

            DrawBorder(Color.FromRgb(169, 169, 169).ToCGColor());
        }

        public override CGRect Frame
        {
            get { return base.Frame; }
            set
            {
                base.Frame = value;

                var element = Element as Picker;
                if (element == null)
                    return;

                DrawBorder(Color.FromRgb(169, 169, 169).ToCGColor());
            }
        }

        private void DrawBorder(CGColor borderColor)
        {
            if (Frame.Height <= 0 || Frame.Width <= 0)
                return;

            if (_borderLayer == null)
            {
                _borderLayer = new CALayer
                {
                    MasksToBounds = false,
                    Frame = new CGRect(0f, Frame.Height + 3, UIScreen.MainScreen.Bounds.Size.Width - 64, 1f),
                    BorderColor = borderColor,
                    BorderWidth = 1.0f
                };

                Control.Layer.AddSublayer(_borderLayer);
                Control.BorderStyle = UITextBorderStyle.None;
            }
            else
            {
                _borderLayer.BorderColor = borderColor;
                _borderLayer.Frame = new CGRect(0f, Frame.Height + 3, UIScreen.MainScreen.Bounds.Size.Width - 50, 1f);
            }
        }
    }
}
