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
	public partial class ForgotOtpPage : ContentPage
	{
        public static string phone = "";
        public static string countryCode = "";
        public static string id = "";
        public ForgotOtpPage ()
		{
            InitializeComponent();
            Title = AppResources._otpVerification;
            BackgroundImage = "login.png";
            NavigationPage.SetHasNavigationBar(this, false);

            if (Device.OS == TargetPlatform.iOS)
            {
                doneBtn.BorderRadius = 20;
                frm1.Padding = new Thickness(14, 9, 14, 9);
                frm2.Padding = new Thickness(14, 9, 14, 9);
                frm3.Padding = new Thickness(14, 9, 14, 9);
                frm4.Padding = new Thickness(14, 9, 14, 9);
                frm5.Padding = new Thickness(14, 9, 14, 9);
            }
            
            txtFirstNumber.Focus();
        }
        private void ResendOtp_Tapped(object sender, EventArgs e)
        {
            setOtp();
        }
        public string CheckValidations()
        {
            string msg = string.Empty;

            if (string.IsNullOrEmpty(txtFirstNumber.Text))
            {
                msg = AppResources._yourFirstDigit + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtSecondNumber.Text))
            {
                msg += AppResources._yourSecondDigit + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtThirdNumber.Text))
            {
                msg += AppResources._yourThirdDigit + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtFourthNumber.Text))
            {
                msg += AppResources._youFrourthDigit + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtFifthNumber.Text))
            {
                msg += AppResources._yourFifthDigit + Environment.NewLine;
            }

            return msg;
        }
        private async void done_Clicked(object sender, EventArgs e)
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

                string otp = txtFirstNumber.Text.Trim() + txtSecondNumber.Text.Trim() + txtThirdNumber.Text.Trim() + txtFourthNumber.Text.Trim() + txtFifthNumber.Text.Trim();

                string postData = "id=" + id + "&otp=" + otp;
                var result = await CommonLib.RegisterPhone(CommonLib.ws_MainUrl + "otpVerify?" + postData);
                if (result.status == 1)
                {
                    OtpPage.phone = result.data.User.phone;
                    OtpPage.countryCode = result.data.User.country_code;
                    Loader.CloseAllPopup();
                    await Navigation.PushAsync(new UpdatePasswordPage());
                   // App.Current.MainPage = new NavigationPage(new SignupPage());


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
        public async void setOtp()
        {
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



                string postData = "phone=" + phone + "&country_code=" + countryCode;
                var result = await CommonLib.RegisterPhone(CommonLib.ws_MainUrl + "registerPhone?" + postData);
                if (result.status == 1)
                {


                    Loader.CloseAllPopup();

                    //  await DisplayAlert("F & C", result.data.User.otp, "OK");

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
        private void back_Tapped(object sender, EventArgs e)
        {
            //  App.Current.MainPage = new NavigationPage(new PhoneRegisterPage());
            Navigation.PopAsync();
        }
        //Auto cursor implementation

        private void click_otp1(object sender, EventArgs e)
        {
            string _text = txtFirstNumber.Text;
            if (txtFirstNumber.Text.Length >= 1)
            {
                if (txtFirstNumber.Text.Length > 1)
                {
                    _text = _text.Remove(_text.Length - 1);  // Remove Last character
                    txtFirstNumber.Text = _text;
                }
                txtSecondNumber.Focus();

            }
            else
            {
                txtFirstNumber.Focus();
            }
        }
        private void click_otp2(object sender, EventArgs e)
        {
            string _text = txtSecondNumber.Text;
            if (txtSecondNumber.Text.Length >= 1)
            {
                if (txtSecondNumber.Text.Length > 1)
                {
                    _text = _text.Remove(_text.Length - 1);  // Remove Last character
                    txtSecondNumber.Text = _text;
                }
                txtThirdNumber.Focus();
            }
            else
            {
                txtSecondNumber.Focus();
            }
        }
        private void click_otp3(object sender, EventArgs e)
        {
            string _text = txtThirdNumber.Text;
            if (txtThirdNumber.Text.Length >= 1)
            {
                if (txtThirdNumber.Text.Length > 1)
                {
                    _text = _text.Remove(_text.Length - 1);  // Remove Last character
                    txtThirdNumber.Text = _text;
                }
                txtFourthNumber.Focus();

            }
            else
            {
                if (!string.IsNullOrEmpty(_text))
                    txtFourthNumber.Focus();
            }
        }
        private void click_otp4(object sender, EventArgs e)
        {
            string _text = txtFourthNumber.Text;
            if (txtFourthNumber.Text.Length >= 1)
            {
                if (txtFourthNumber.Text.Length > 1)
                {
                    _text = _text.Remove(_text.Length - 1);  // Remove Last character
                    txtFourthNumber.Text = _text;

                }

                txtFifthNumber.Focus();
            }
            else
            {
                txtFourthNumber.Focus();
            }
        }
        private void click_otp5(object sender, EventArgs e)
        {
            string _text = txtFifthNumber.Text;
            if (txtFifthNumber.Text.Length >= 1)
            {
                if (txtFifthNumber.Text.Length > 1)
                {
                    _text = _text.Remove(_text.Length - 1);  // Remove Last character
                    txtFifthNumber.Text = _text;

                }
            }
            else
            {
                txtFifthNumber.Focus();
            }
        }
    }
}