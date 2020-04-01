using System;
using System.Collections.Generic;
using FlowersAndCandyCustomer.Resources;
using FlowersAndCandyCustomer.ViewModels;
using Xamarin.Forms;

namespace FlowersAndCandyCustomer.Views
{
    public partial class TermsConditionsPage : ContentPage
    {
        public TermsConditionsPage()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);

            BindingContext = new TermsConditionsViewModel(Navigation);
        }
    }
}
