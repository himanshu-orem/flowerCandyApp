using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Resources;
using FlowersAndCandyCustomer.ViewModels;
using Xamarin.Forms;

namespace FlowersAndCandyCustomer.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            firstLayout.WidthRequest = App.ScreenWidth / 3;
            secondLayout.WidthRequest = App.ScreenWidth / 3;
            thirdLayout.WidthRequest = App.ScreenWidth / 3;
            //fourthLayout.WidthRequest = App.ScreenWidth / 4;

            BindingContext = new MainPageViewModel();
            LoggedInUser objUser = App.Database.GetLoggedInUser();
            if (objUser == null)
            {
                secondLayout.IsVisible = false;
                crtImg.Source = "";
            }
            if(Device.OS==TargetPlatform.iOS){
                crtImg.Margin = new Thickness(0, 6, 0, 0);
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var _products = App.Database.GetAllCount();
                cartCountLbl.Text = _products.ToString();
            }
            catch (Exception)
            {
            }
        }

        private async void Cart_Tapped(object sender, EventArgs e)
        {
            LoggedInUser objUser = App.Database.GetLoggedInUser();
            if (objUser != null)
            {
                await App.Current.MainPage.Navigation.PushAsync(new CartPage());
            }
        }
        protected   override bool OnBackButtonPressed()
        {

                    back();
                    return true;
                


        }
       public async void back()
        {
            var ans =await App.Current.MainPage.DisplayAlert("", AppResources.closeApp, AppResources.yes, AppResources.no);
            if (ans)
            {
                System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
            }
           
        }

    }
}
