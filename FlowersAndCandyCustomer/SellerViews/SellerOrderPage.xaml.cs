using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Resources;
using FlowersAndCandyCustomer.ViewModels;
using FlowersAndCandyCustomer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlowersAndCandyCustomer.SellerViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SellerOrderPage : ContentPage
	{
		public SellerOrderPage ()
		{
			InitializeComponent ();
            Title = AppResources.orders ;
            if(Device.OS==TargetPlatform.iOS){
                currentOrdersBtn.BorderRadius = 20;
                previousOrdersBtn.BorderRadius = 20;

            }
           
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            SellerOrdersViewModel modelC = new SellerOrdersViewModel("1");
            SellerOrders.BindingContext = modelC;
            SellerOrdersViewModel modelP = new SellerOrdersViewModel("2");
            PSellerOrders.BindingContext = modelP;
        }
        private void CurrentOrdersBtn_Clicked(object sender, EventArgs e)
        {
            currentOrdersBtn.TextColor = Color.White;
            currentOrdersBtn.BackgroundColor = Color.FromHex("#FE1F78");
            previousOrdersBtn.TextColor = Color.FromHex("#A3989C");
            previousOrdersBtn.BackgroundColor = Color.FromHex("#FFEFF5");
            PSellerOrders.IsVisible = false;
            SellerOrders.IsVisible = true;
        }

        private void PreviousOrdersBtn_Clicked(object sender, EventArgs e)
        {
            previousOrdersBtn.TextColor = Color.White;
            previousOrdersBtn.BackgroundColor = Color.FromHex("#FE1F78");
            currentOrdersBtn.TextColor = Color.FromHex("#A3989C");
            currentOrdersBtn.BackgroundColor = Color.FromHex("#FFEFF5");
            PSellerOrders.IsVisible = true;
            SellerOrders.IsVisible = false;
        }

        private async void SellerOrders_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (SellerOrders.SelectedItem != null)
            {
                try
                {
                    var selected = SellerOrders.SelectedItem as OrderSeller;
                    SellerOrderInfo.id = selected.id;
                    await App.Current.MainPage.Navigation.PushAsync(new SellerOrderInfo());
                    SellerOrders.SelectedItem = null;
                }
                catch (Exception)
                {
                }
            }
        }

        private void SellerOrders_Refreshing(object sender, EventArgs e)
        {
            SellerOrdersViewModel modelC = new SellerOrdersViewModel("1");
            SellerOrders.BindingContext = modelC;
            SellerOrders.EndRefresh();

        }

        private void PSellerOrders_Refreshing(object sender, EventArgs e)
        {
            SellerOrdersViewModel modelP = new SellerOrdersViewModel("2");
            PSellerOrders.BindingContext = modelP;
            PSellerOrders.EndRefresh();
        }

        private async void PSellerOrders_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (PSellerOrders.SelectedItem != null)
            {
                try
                {
                    var selected = PSellerOrders.SelectedItem as OrderSeller;
                    SellerOrderInfo.id = selected.id;
                    await App.Current.MainPage.Navigation.PushAsync(new SellerOrderInfo());
                    PSellerOrders.SelectedItem = null;
                }
                catch (Exception)
                {
                }
            }
        }
    }
}