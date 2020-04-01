using System;
using System.Collections.Generic;
using FlowersAndCandyCustomer.Resources;
using FlowersAndCandyCustomer.ViewModels;
using Xamarin.Forms;

namespace FlowersAndCandyCustomer.Views
{
    public partial class ChangePasswordPage : ContentPage
    {
        public ChangePasswordPage()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);

            if (Device.RuntimePlatform == Device.iOS)
            {
                changePasswordBtn.CornerRadius = 20;
                currentPasswordTxt.Margin = new Thickness(0, 15, 0, 0);
                newPasswordTxt.Margin = new Thickness(0, 15, 0, 0);
                confirmNewPasswordTxt.Margin = new Thickness(0, 15, 0, 0);

                currentPasswordLbl.Margin = new Thickness(0, 30, 0, 0);
                newPasswordLbl.Margin = new Thickness(0, 30, 0, 0);
                confirmNewPasswordLbl.Margin = new Thickness(0, 30, 0, 0);
            }

            BindingContext = new ChangePasswordViewModel(Navigation);
             
        }
    }
}
