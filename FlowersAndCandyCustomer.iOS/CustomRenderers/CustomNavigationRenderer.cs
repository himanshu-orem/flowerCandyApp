using System;
using FlowersAndCandyCustomer.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Page),
                          typeof(CustomNavigationRenderer))]
namespace FlowersAndCandyCustomer.iOS.CustomRenderers
{
    public class CustomNavigationRenderer : PageRenderer
    {
        public CustomNavigationRenderer()
        {
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            try
            {
                UIBarButtonItem backButton = new UIBarButtonItem();
                backButton.Title = "";
                NavigationController.NavigationBar.TopItem.BackBarButtonItem = backButton;
            }
            catch (Exception)
            {

            }
        }
    }
}
