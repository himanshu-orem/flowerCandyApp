using System;
using System.Collections.Generic;
using FlowersAndCandyCustomer.Resources;
using FlowersAndCandyCustomer.ViewModels;
using Xamarin.Forms;

namespace FlowersAndCandyCustomer.Views
{
    public partial class PrivacyPolicyPage : ContentPage
    {
        public PrivacyPolicyPage()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);

            BindingContext = new PrivacyPolicyViewModel(Navigation);
            
        }
    }
}
