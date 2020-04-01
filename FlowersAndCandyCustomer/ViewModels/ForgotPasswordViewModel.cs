using System;
using System.ComponentModel;
using System.Threading.Tasks;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.Resources;
using FlowersAndCandyCustomer.Views;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace FlowersAndCandyCustomer.ViewModels
{
    public class ForgotPasswordViewModel : INotifyPropertyChanged
    {
        public INavigation _navigation;
        /// <summary>
        /// property for the email field
        /// </summary>
        private string _email;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }

        public ForgotPasswordViewModel(INavigation navigation)
        {
            _navigation = navigation;
        }

        /// <summary>
        /// This event will be used for sign up functionality.
        /// </summary>
        public Command forgotPasswordCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    var returnMessage = CheckValidations();
                    if (!string.IsNullOrEmpty(returnMessage))
                    {
                       // await _navigation.PushPopupAsync(new ShowMessage(returnMessage));
                      //  await Task.Delay(1000);
                       //await _navigation.PopPopupAsync();
                        return;
                    }
                    else
                    {
                        try
                        {


                            if (!CommonLib.checkconnection())
                            {

                                await _navigation.PushPopupAsync(new ShowMessage(AppResources._connection));
                                await Task.Delay(1000);
                                await _navigation.PopPopupAsync();
                                return;
                            }


                            await App.Current.MainPage.Navigation.PushPopupAsync(new Loader());



                            string postData = "phone=" + Email+ "&country_code=" + ForgotPasswordPage.countryCode;
                            var result = await CommonLib.ForgotPassword(CommonLib.ws_MainUrl + "forgetPasswordByPhone?" + postData);
                            if (result.status == 1)
                            {
                                

                                Loader.CloseAllPopup();


                                ForgotOtpPage.phone = result.data.User.phone;
                                ForgotOtpPage.countryCode = result.data.User.country_code;
                                ForgotOtpPage.id = result.data.User.id;
                               await App.Current.MainPage.Navigation.PushAsync(new ForgotOtpPage());

                                //await App.Current.MainPage.DisplayAlert("", result.msg_ar, "OK");



                                // App.Current.MainPage = new NavigationPage(new LoginPage());

                            }
                            else
                            {
                                Loader.CloseAllPopup();

                                if (App.Lng == "ar-AE")
                                {
                                    await _navigation.PushPopupAsync(new ShowMessage(result.msg_ar));

                                }
                                else
                                {
                                    await _navigation.PushPopupAsync(new ShowMessage(result.msg_en));

                                }
                                await Task.Delay(1000);
                                await _navigation.PopPopupAsync();
                            }

                        }
                        catch (Exception ex)
                        {
                            Loader.CloseAllPopup();
                        }
                    }
                });
            }
        }

        public string CheckValidations()
        {
            string msg = string.Empty;

            if (string.IsNullOrEmpty(Email))
            {
                msg += AppResources.enter_email_validation + Environment.NewLine;
            }
            //if (!string.IsNullOrEmpty(Email))
            //{
            //    if (!CommonLib.CheckValidEmail(Email))
            //    {
            //        msg += AppResources.enter_valid_email_validation;
            //    }
            //}
            return msg;
        }

        /// <summary>
        /// This tapped event will be used for opening the login page.
        /// </summary>
        public Command loginCommand
        {
            get
            {
                return new Command((e) =>
                {
                    App.Current.MainPage = new NavigationPage(new LoginPage());

                });
            }
        }
    }
}
