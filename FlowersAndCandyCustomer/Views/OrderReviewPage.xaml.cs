using System;
using System.Collections.Generic;
using FlowersAndCandyCustomer.ViewModels;
using Xamarin.Forms;

namespace FlowersAndCandyCustomer.Views
{
    public partial class OrderReviewPage : ContentPage
    {
        public OrderReviewPage(string address)
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);

            firstLbl.WidthRequest = (App.ScreenWidth / 2) + 50;
            secondLbl.WidthRequest = (App.ScreenWidth / 2) - 50;

            if(!string.IsNullOrEmpty(address))
            {
                //addressFrame.IsVisible = true;
               // addressLbl.Text = address;
                secondLbl.BackgroundColor = Color.FromHex("#B8074E");
                secondLbl.TextColor = Color.White;
                secondLbl.IsEnabled = true;
            }

            BindingContext = new OrderReviewViewModel();
            //language
            if (App.lang == "ar-AE")
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
    }
}
