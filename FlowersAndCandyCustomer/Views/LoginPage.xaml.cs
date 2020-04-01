using System;
using System.Collections.Generic;
using System.Linq;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.ViewModels;
using Xamarin.Forms;

namespace FlowersAndCandyCustomer.Views
{
    public partial class LoginPage : ContentPage
    {
        public static string userType = "1";
        public static string countryCode = "";
        public LoginPage()
        {
            InitializeComponent();
            userType = "2";
            BackgroundImage = "login.png";
            NavigationPage.SetHasNavigationBar(this, false);

            if (Device.RuntimePlatform == Device.iOS)
            {
                this.Padding = new Thickness(0, 40, 0, 0);
                loginBtn.CornerRadius = 20;
                emailTxt.Margin = new Thickness(0, 15, 0, 0);
                passwordTxt.Margin = new Thickness(0, 15, 0, 0);
                emailLbl.Margin = new Thickness(0, 60, 0, 0);
                passwordLbl.Margin = new Thickness(0, 50, 0, 0);
            }

            BindingContext = new LoginViewModel(Navigation);

            getCountryCodes();

        }
        private void customer_Tapped(object sender, EventArgs e)
        {
            userType = "2";
            customerImg.Source = "ico_round_checked.png";
            sellerImg.Source = "ico_round_check.png";
            phnLyt.IsVisible = false;
            emailLyt.IsVisible = true;
            forgotLbl.IsVisible = true;

        }
        private void seller_Tapped(object sender, EventArgs e)
        {
            userType = "1";
            customerImg.Source = "ico_round_check.png";
            sellerImg.Source = "ico_round_checked.png";
            phnLyt.IsVisible = true;
            emailLyt.IsVisible = false;
            forgotLbl.IsVisible = false;

        }
        private void PhoneCodePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string Code = phoneCodePicker.SelectedItem.ToString();
                countryCode = Code.Replace("+", "");
                phoneCodePicker.Title = phoneCodePicker.SelectedItem.ToString();
            }
            catch (Exception)
            {
            }
        }
        public void getCountryCodes()
        {

            try
            {
                var getList = CommonLib.LoadJson();
                if (getList != null)
                {

                    var getList1 = getList.OrderBy(x => x.dial_code);
                    phoneCodePicker.SelectedIndex = 0;
                    phoneCodePicker.Title = "+966";
                    foreach (var item in getList1)
                    {

                        if (item.dial_code != 0)
                        {
                            phoneCodePicker.Items.Add("+" + item.dial_code);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            countryCode = phoneCodePicker.Title.Replace("+", "");
        }
    }
}
