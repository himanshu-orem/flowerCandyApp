using System;
using System.Collections.Generic;
using System.Linq;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.ViewModels;
using Xamarin.Forms;

namespace FlowersAndCandyCustomer.Views
{
    public partial class ForgotPasswordPage : ContentPage
    {
        public static string countryCode = "";

        public ForgotPasswordPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
            BackgroundImage = "login.png";

            if (Device.RuntimePlatform == Device.iOS)
            {
                this.Padding = new Thickness(0, 30, 0, 0);
                sendBtn.CornerRadius = 20;
                emailTxt.Margin = new Thickness(0, 15, 0, 0);
                emailLbl.Margin = new Thickness(0, 50, 0, 0);
            }


            BindingContext = new ForgotPasswordViewModel(Navigation);
            getCountryCodes();
            phoneCodePicker.SelectedIndex = 0;
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
    }
}
