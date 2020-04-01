using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.Resources;
using FlowersAndCandyCustomer.ViewModels;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace FlowersAndCandyCustomer.Views
{
    public partial class SignupPage : ContentPage
    {
        public static string sellerType = "";
        public static string userType = "1";
        public static string countryCode = "";
        public static string id = "";
        public SignupPage()
        {
            InitializeComponent();

            sellerType = "";
            userType = "1";

           

            NavigationPage.SetHasNavigationBar(this, false);

            if (Device.RuntimePlatform == Device.iOS)
            {
                signUpBtn.CornerRadius = 20;
                emailTxt.Margin = new Thickness(0, 15, 0, 0);
                passwordTxt.Margin = new Thickness(0, 15, 0, 0);
                firstNameTxt.Margin = new Thickness(0, 15, 0, 0);
                lastNameTxt.Margin = new Thickness(0, 15, 0, 0);
                confirmPasswordTxt.Margin = new Thickness(0, 15, 0, 0);
               // phoneNumberTxt.Margin = new Thickness(0, 15, 0, 0);
              //  phoneCodePicker.Margin = new Thickness(0, 15, 0, 0);

                firstNameLbl.Margin = new Thickness(0, 30, 0, 0);
                lastNameLbl.Margin = new Thickness(0, 30, 0, 0);
                passwordLbl.Margin = new Thickness(0, 30, 0, 0);
                emailLbl.Margin = new Thickness(0, 30, 0, 0);
                confirmPasswordLbl.Margin = new Thickness(0, 30, 0, 0);
                //phoneNumberLbl.Margin = new Thickness(0, 30, 0, 0);
            }
            BindingContext = new SignupViewModel(Navigation);
            Category();
           // getCountryCodes();
           
        }

         

        private void customer_Tapped(object sender, EventArgs e)
        {
            userType = "1";
            customerImg.Source = "ico_round_checked.png";
            sellerImg.Source = "ico_round_check.png";
            sellerLyt.IsVisible = false;

            firstNameLbl.Text = AppResources.first_name;
                firstNameTxt.Placeholder = AppResources.enter_first_name;
            emailLbl.IsVisible = false;
            emailTxt.IsVisible = false;
            passwordLbl.IsVisible = true;
            passwordTxt.IsVisible = true;
            confirmPasswordLbl.IsVisible = true;
            confirmPasswordTxt.IsVisible = true;

            lastNameLbl.IsVisible = true;
            lastNameTxt.IsVisible = true;
        }
        private void seller_Tapped(object sender, EventArgs e)
        {
            userType = "2";
            customerImg.Source = "ico_round_check.png";
            sellerImg.Source = "ico_round_checked.png";
            sellerLyt.IsVisible = true;

            firstNameLbl.Text = AppResources.name;
                firstNameTxt.Placeholder = AppResources.enter_name;

            emailLbl.IsVisible = true;
            emailTxt.IsVisible = true;
            passwordLbl.IsVisible = true;
            passwordTxt.IsVisible = true;
            confirmPasswordLbl.IsVisible = true;
            confirmPasswordTxt.IsVisible = true;

            lastNameLbl.IsVisible = false;
            lastNameTxt.IsVisible = false;
        }

        

        private void SellerPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var select = sellerPicker.SelectedItem as HomeCategory;
                sellerType = select.id;
            }
            catch (Exception)
            {
            }
        }
        public async void Category()
        {
            try
            {


                if (!CommonLib.checkconnection())
                {
                    await Navigation.PushPopupAsync(new ShowMessage(AppResources._connection));
                    await Task.Delay(1000);
                    ShowMessage.CloseAllPopup();
                    return;
                }
               // await App.Current.MainPage.Navigation.PushPopupAsync(new Loader());
                var result = await CommonLib.HomePageCategory(CommonLib.ws_MainUrl + "getCategories?");
                if (result.status == 1)
                {
                    sellerPicker.ItemsSource = result.data.categories;
                }
               // Loader.CloseAllPopup();
            }
            catch (Exception ex)
            {
                Loader.CloseAllPopup();
            }

        }
    }
}
