using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlowersAndCandyCustomer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerOrders : ContentView
    {
        public CustomerOrders()
        {
            InitializeComponent();

            CustomerOrdersViewModel modelC = new CustomerOrdersViewModel("1");
            customerOrders.BindingContext = modelC;
            CustomerOrdersViewModel modelP = new CustomerOrdersViewModel("2");
            PcustomerOrders.BindingContext = modelP;
            if(Device.OS==TargetPlatform.iOS){
                currentOrdersBtn.BorderRadius = 20;
                previousOrdersBtn.BorderRadius = 20;
            }
        }

        private void CurrentOrdersBtn_Clicked(object sender, EventArgs e)
        {
            currentOrdersBtn.TextColor = Color.White;
            currentOrdersBtn.BackgroundColor = Color.FromHex("#FE1F78");
            previousOrdersBtn.TextColor = Color.FromHex("#A3989C");
            previousOrdersBtn.BackgroundColor = Color.FromHex("#FFEFF5");
            PcustomerOrders.IsVisible = false;
            customerOrders.IsVisible = true;
        }

        private void PreviousOrdersBtn_Clicked(object sender, EventArgs e)
        {
            previousOrdersBtn.TextColor = Color.White;
            previousOrdersBtn.BackgroundColor = Color.FromHex("#FE1F78");
            currentOrdersBtn.TextColor = Color.FromHex("#A3989C");
            currentOrdersBtn.BackgroundColor = Color.FromHex("#FFEFF5");
            PcustomerOrders.IsVisible = true;
            customerOrders.IsVisible = false;
        }

        private async void CustomerOrders_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (customerOrders.SelectedItem != null)
            {
                try
                {
                    var selected = customerOrders.SelectedItem as CustomerOrdersCls;
                    CustomerOrderInfo.id = selected.id;
                    await App.Current.MainPage.Navigation.PushAsync(new CustomerOrderInfo());
                    customerOrders.SelectedItem = null;
                }
                catch (Exception)
                {
                }
            }
        }

        private void CustomerOrders_Refreshing(object sender, EventArgs e)
        {
            CustomerOrdersViewModel modelC = new CustomerOrdersViewModel("1");
            customerOrders.BindingContext = modelC;
            customerOrders.EndRefresh();

        }

        private void PcustomerOrders_Refreshing(object sender, EventArgs e)
        {
            CustomerOrdersViewModel modelP = new CustomerOrdersViewModel("2");
            PcustomerOrders.BindingContext = modelP;
            PcustomerOrders.EndRefresh();
        }

        private async void PcustomerOrders_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (PcustomerOrders.SelectedItem != null)
            {
                try
                {
                    var selected = PcustomerOrders.SelectedItem as CustomerOrdersCls;
                    CustomerOrderInfo.id = selected.id;
                    await App.Current.MainPage.Navigation.PushAsync(new CustomerOrderInfo());
                    PcustomerOrders.SelectedItem = null;
                }
                catch (Exception)
                {
                }
            }
        }
    }
}