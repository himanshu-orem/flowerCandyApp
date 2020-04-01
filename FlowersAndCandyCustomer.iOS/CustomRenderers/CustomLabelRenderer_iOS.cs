using System;
using System.ComponentModel;
using FlowersAndCandyCustomer.CustomControls;
using FlowersAndCandyCustomer.iOS.CustomRenderers;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LineLabel), typeof(CustomLabelRenderer_iOS))]
namespace FlowersAndCandyCustomer.iOS.CustomRenderers
{
    public class CustomLabelRenderer_iOS : LabelRenderer
    {
        public CustomLabelRenderer_iOS()
        {

        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var lineSpacingLabel = (LineLabel)this.Element;
            var paragraphStyle = new NSMutableParagraphStyle()
            {
                LineSpacing = (nfloat)lineSpacingLabel.LineSpacing
            };
            var _string = new NSMutableAttributedString(lineSpacingLabel.Text);
            var style = UIStringAttributeKey.ParagraphStyle;
            var range = new NSRange(0, _string.Length);

            _string.AddAttribute(style, paragraphStyle, range);

            this.Control.AttributedText = _string;
        }
    }
}
