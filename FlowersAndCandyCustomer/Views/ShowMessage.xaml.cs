using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace FlowersAndCandyCustomer.Views
{
    public partial class ShowMessage : PopupPage
    {
        public ShowMessage(string message)
        {
            InitializeComponent();

            msgTxt.Text = message;

            CloseWhenBackgroundIsClicked = false;
        }


        public static async void CloseAllPopup()
        {
            await App.Current.MainPage.Navigation.PopPopupAsync(false);
        }
    }
}
