using System;
using System.ComponentModel;
using System.Threading.Tasks;
using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.Resources;
using FlowersAndCandyCustomer.SellerViews;
using FlowersAndCandyCustomer.Views;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace FlowersAndCandyCustomer.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public INavigation _navigation;
        /// <summary>
        /// property for the email field
        /// </summary>
        private string _email;
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
        private string _phn;
        public string Phn
        {
            get
            {
                return _phn;
            }
            set
            {
                _phn = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Phn"));
            }
        }

        /// <summary>
        /// property for the password field
        /// </summary>
        private string _password;

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public LoginViewModel(INavigation navigation)
        {
            _navigation = navigation;
        }

        /// <summary>
        /// This tapped event will be used for opening the signup page.
        /// </summary>
        public Command signUpCommand
        {
            get
            {
                return new Command((e) =>
                {
                    // _navigation.PushAsync(new SignupPage());
                    App.Current.MainPage = new NavigationPage(new PhoneRegisterPage());
                });
            }
        }

        /// <summary>
        /// This tapped event will be used for opening the forgot password page.
        /// </summary>
        public Command forgotPasswordCommand
        {
            get
            {
                return new Command((e) =>
                {
                    

                     App.Current.MainPage = new NavigationPage(new ForgotPasswordPage());
                });
            }
        }

        /// <summary>
        /// This event will be used for login button functionality.
        /// </summary>
        public Command loginBtnCommand
        {
            get
            {
                return new Command(async (e) =>
                {

                    if (LoginPage.userType == "2")
                    {

                        var returnMessage = CheckLoginValidations();
                        if (!string.IsNullOrEmpty(returnMessage))
                        {
                            await _navigation.PushPopupAsync(new ShowMessage(returnMessage));
                            await Task.Delay(1000);
                            await _navigation.PopPopupAsync();
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



                                string postData = "email=" + Email.Trim() + "&password=" + Password.Trim();
                                var result = await CommonLib.Login(CommonLib.ws_MainUrl + "login?" + postData);
                                if (result.status == 1)
                                {

                                    Loader.CloseAllPopup();

                                    if (result.data.User.userType == "2")
                                    {
                                        // Save data in sqlite for login check
                                        LoggedInUser objUser = new LoggedInUser();
                                        objUser.userId = Convert.ToInt32(result.data.User.id);
                                        objUser.fname = result.data.User.first_name;
                                        objUser.lname = result.data.User.last_name;
                                        objUser.email = result.data.User.email;
                                        objUser.image = CommonLib.img_MainUrl + result.data.User.image;
                                        objUser.phone = result.data.User.phone;
                                        objUser.userType = result.data.User.userType;
                                        objUser.country_code = result.data.User.country_code;
                                        objUser.is_shop = result.data.User.is_shop;

                                       


                                        App.Database.SaveLoggedInUser(objUser);

                                       // App.Current.MainPage = new NavigationPage(new MainPage());
                                    }
                                    if (result.data.User.userType == "2")
                                    {
                                        if (result.data.User.is_shop == "0")
                                        {
                                            App.Current.MainPage = new NavigationPage(new AddShopPage());
                                        }
                                        else
                                        {
                                            App.Current.MainPage = new NavigationPage(new HomeMasterPage());
                                        }
                                    }



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
                    }
                    else
                    {
                        var returnMessage = CheckLoginValidationsPhn();
                        if (!string.IsNullOrEmpty(returnMessage))
                        {
                            await _navigation.PushPopupAsync(new ShowMessage(returnMessage));
                            await Task.Delay(1000);
                            await _navigation.PopPopupAsync();
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



                                string postData = "phone=" + Phn.Trim() + "&country_code=" + LoginPage.countryCode + "&password=" + Password.Trim();
                                var result = await CommonLib.LoginPhn(CommonLib.ws_MainUrl + "loginByphone?" + postData);
                                if (result.status == 1)
                                {

                                    Loader.CloseAllPopup();
                                    //OtpPage.isLogin = true;
                                    //OtpPage.id = result.data.User.id;
                                    //OtpPage.phone= result.data.User.phone;
                                    //OtpPage.countryCode = result.data.User.country_code;



                                    // Save data in sqlite for login check
                                    LoggedInUser objUser = new LoggedInUser();
                                    objUser.userId = Convert.ToInt32(result.data.User.id);
                                    objUser.fname = result.data.User.first_name;
                                    objUser.lname = result.data.User.last_name;
                                    objUser.email = result.data.User.email;
                                    objUser.image = CommonLib.img_MainUrl + result.data.User.image;
                                    objUser.phone = result.data.User.phone;
                                    objUser.userType = result.data.User.userType;
                                    objUser.country_code = result.data.User.country_code;
                                    objUser.is_shop = result.data.User.is_shop;

                                    objUser.surName = result.data.User.surname;
                                    objUser.streetAddress = result.data.User.streetaddress;
                                    objUser.city = result.data.User.city;
                                    objUser.state = result.data.User.state;
                                    objUser.country = result.data.User.country;
                                    objUser.postCode = result.data.User.postcode;
                                    App.Database.SaveLoggedInUser(objUser);

                                  
                                    App.Current.MainPage = new NavigationPage(new MainPage());



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
                    }


                });
            }
        }

        public string CheckLoginValidations()
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(Email))
            {
                msg += AppResources.enter_email_validation + Environment.NewLine;
            }
            if (!string.IsNullOrEmpty(Email))
            {
                if (!CommonLib.CheckValidEmail(Email))
                {
                    msg += AppResources.enter_valid_email_validation + Environment.NewLine;
                }
            }
            if (string.IsNullOrEmpty(Password))
            {
                msg += AppResources.enter_password_validation;
            }
            return msg;
        }

        public string CheckLoginValidationsPhn()
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(Phn))
            {
                msg += AppResources.enterPhone + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(Password))
            {
                msg += AppResources.enter_password_validation;
            }
            return msg;
        }
    }
}
