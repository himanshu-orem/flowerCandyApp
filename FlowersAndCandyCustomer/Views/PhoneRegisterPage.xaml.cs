using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.Resources;
using Rg.Plugins.Popup.Extensions;
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
    public partial class PhoneRegisterPage : ContentPage
    {
        public static string countryCode = "";
        public PhoneRegisterPage()
        {

            OtpPage.isLogin = false;
            OtpPage.phone = "";
            OtpPage.countryCode = "";
            OtpPage.id = "";
            SignupPage.id = "";

            InitializeComponent();
            if(Device.OS==TargetPlatform.iOS){
                sendBtn.BorderRadius = 20;
            }
            NavigationPage.SetHasNavigationBar(this, false);
            BackgroundImage = "login.png";
            getCountryCodes();
           
        }

        private void back_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new LoginPage());
        }
        public string CheckValidations()
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(phoneNumberTxt.Text))
            {
                msg += AppResources.enter_phone_number + Environment.NewLine;
            }
            if (phoneCodePicker.SelectedIndex == null)
            {
                msg += AppResources.selectCountry + Environment.NewLine;
            }

            return msg;
        }
        private async void SendBtn_Clicked(object sender, EventArgs e)
        {
            var returnMessage = CheckValidations();
            if (!string.IsNullOrEmpty(returnMessage))
            {
                await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(returnMessage));
                await Task.Delay(1000);
                ShowMessage.CloseAllPopup();
                return;
            }
            try
            {


                if (!CommonLib.checkconnection())
                {

                    await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(AppResources._connection));
                    await Task.Delay(1000);
                    ShowMessage.CloseAllPopup();
                    return;
                }


                await App.Current.MainPage.Navigation.PushPopupAsync(new Loader());



                string postData = "phone=" + phoneNumberTxt.Text + "&country_code=" + countryCode;
                var result = await CommonLib.RegisterPhone(CommonLib.ws_MainUrl + "registerPhone?" + postData);
                if (result.status == 1)
                {
                    SignupPage.id= result.data.User.id;
                    OtpPage.phone = result.data.User.phone;
                    OtpPage.countryCode = result.data.User.country_code;
                    OtpPage.id = result.data.User.id;
                    Loader.CloseAllPopup();

                    if (result.data.User.verify == "0")
                    {
                        OtpPage.isLogin = false;
                        App.Current.MainPage.Navigation.PushAsync(new OtpPage());

                       // await DisplayAlert("F & C", result.data.User.otp, "OK");
                    }
                    else
                    {
                        App.Current.MainPage = new NavigationPage(new SignupPage());
                    }
                }
                else
                {
                    Loader.CloseAllPopup();

                    if (App.Lng == "ar-AE")
                    {
                        await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(result.msg_ar));

                    }
                    else
                    {
                        await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(result.msg_en));

                    }
                    await Task.Delay(1000);
                    ShowMessage.CloseAllPopup();
                }

            }
            catch (Exception ex)
            {
                Loader.CloseAllPopup();
            }

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

        private void login_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new LoginPage());
        }

        private void forgotPasswordCommand(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new ForgotPasswordPage());
        }
    }
}