using System;
using Android.Content;
using Android.Graphics;
using Android.Widget;
using FlowersAndCandyCustomer.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(CustomNavigationBarRenderer))]
namespace FlowersAndCandyCustomer.Droid.CustomRenderers
{
    public class CustomNavigationBarRenderer : NavigationPageRenderer
    {
        private Android.Support.V7.Widget.Toolbar toolbar;
        public CustomNavigationBarRenderer(Context context) : base(context)
        {

        }

        public override void OnViewAdded(Android.Views.View child)
        {
            base.OnViewAdded(child);
            if (child.GetType() == typeof(Android.Support.V7.Widget.Toolbar))
            {
                toolbar = (Android.Support.V7.Widget.Toolbar)child;
                toolbar.ChildViewAdded += Toolbar_ChildViewAdded;
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            try
            {
                var toolbar1 = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
                try
                {
                    base.OnLayout(changed, l, t, r, b);
                }
                catch (Exception)
                {
                }

                var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

                for (int index = 0; index < toolbar.ChildCount; index++)
                {
                    if (toolbar.GetChildAt(index) is TextView)
                    {
                        var title = toolbar.GetChildAt(index) as TextView;
                        float toolbarCenter = toolbar.MeasuredWidth / 2;
                        float titleCenter = title.MeasuredWidth / 2;
                        title.SetX(toolbarCenter - titleCenter);
                        var font = Typeface.CreateFromAsset(Forms.Context.ApplicationContext.Assets, "CALIBRI.ttf");
                        title.Typeface = font;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void Toolbar_ChildViewAdded(object sender, ChildViewAddedEventArgs e)
        {
            var view = e.Child.GetType();
            if (e.Child.GetType() == typeof(Android.Widget.TextView))
            {
                var textView = (Android.Widget.TextView)e.Child;
                var spaceFont = Typeface.CreateFromAsset(Android.App.Application.Context.ApplicationContext.Assets, "CALIBRI.ttf");
                textView.Typeface = spaceFont;
                toolbar.ChildViewAdded -= Toolbar_ChildViewAdded;
            }
        }




    }
}
