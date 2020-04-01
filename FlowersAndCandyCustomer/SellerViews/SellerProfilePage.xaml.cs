using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Resources;
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
	public partial class SellerProfilePage : ContentPage
	{
		public SellerProfilePage ()
		{
			InitializeComponent ();
            Title = AppResources.profile;
            BindingContext = new ProfileViewModel();
            LoggedInUser objUser = App.Database.GetLoggedInUser();
            if (objUser != null)
            {
                img.Source = string.IsNullOrEmpty(objUser.image) ? "user_placeholder.png" : objUser.image;
            }
            else
            {
                img.Source = "user_placeholder.png";
            }
        }
	}
}