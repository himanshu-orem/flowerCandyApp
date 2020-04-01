using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.ViewModels;
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
	public partial class AddAddonsPage : ContentPage
	{
       
		public AddAddonsPage()
		{
			InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
          //  NavigationPage.SetHasBackButton(this, false);

           // BindingContext = new AddAddonsViewModel();

            AddAddonsViewModel model = new AddAddonsViewModel();
            addonListView.BindingContext = model;

        }
        private void back_Tapped(object sender, EventArgs e)
        {
           // Navigation.PopAsync();
            App.Current.MainPage = new NavigationPage(new HomeMasterPage());

        }

        private void AddonListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            addonListView.SelectedItem = null;
        }

         
    }
}