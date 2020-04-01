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
	public partial class UpdatePasswordPage : ContentPage
	{
		public UpdatePasswordPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasBackButton(this, false);
            if (Device.RuntimePlatform == Device.iOS)
            {
                changePasswordBtn.CornerRadius = 20;
                
                newPasswordTxt.Margin = new Thickness(0, 15, 0, 0);
                confirmNewPasswordTxt.Margin = new Thickness(0, 15, 0, 0);

               
                newPasswordLbl.Margin = new Thickness(0, 30, 0, 0);
                confirmNewPasswordLbl.Margin = new Thickness(0, 30, 0, 0);
            }
        }

        private void back_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        public string CheckValidations()
        {
            string msg = string.Empty;
          
            if (string.IsNullOrEmpty(newPasswordTxt.Text))
            {
                msg += AppResources.enter_new_password_validation + Environment.NewLine;
            }
            if (!string.IsNullOrEmpty(newPasswordTxt.Text))
            {
                if (newPasswordTxt.Text != confirmNewPasswordTxt.Text)
                {
                    msg += AppResources.password_confirm_password_mismatch_validation + Environment.NewLine;
                }
            }
           
            return msg;
        }
        private async void ChangePasswordBtn_Clicked(object sender, EventArgs e)
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


                string postData = "user_id=" + ForgotOtpPage.id + "&newPass=" + confirmNewPasswordTxt.Text;
                var result = await CommonLib.UpdateToken(CommonLib.ws_MainUrl + "resetPassword?" + postData);
                if (result.status == 1)
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
                    App.Current.MainPage = new NavigationPage(new LoginPage());

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
    }
}