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
    public class SignupViewModel : INotifyPropertyChanged
    {
        public INavigation _navigation;

        /// <summary>
        /// property for the first name field
        /// </summary>
        private string _firstName;
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("FirstName"));
            }
        }

        /// <summary>
        /// property for the last name field
        /// </summary>
        private string _lastName;
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("LastName"));
            }
        }

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

        /// <summary>
        /// property for the confirm password field
        /// </summary>
        private string _confirmPassword;
        public string ConfirmPassword
        {
            get
            {
                return _confirmPassword;
            }
            set
            {
                _confirmPassword = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ConfirmPassword"));
            }
        }

        /// <summary>
        /// property for the confirm password field
        /// </summary>
        private string _phoneNumber;
        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                _phoneNumber = value;
                PropertyChanged(this, new PropertyChangedEventArgs("PhoneNumber"));
            }
        }


        /// <summary>
        /// property for the terms and conditions check box
        /// </summary>
        private string _termsCheckBox;
        public string TermsCheckBox
        {
            get
            {
                return _termsCheckBox;
            }
            set
            {
                _termsCheckBox = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TermsCheckBox"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public SignupViewModel(INavigation navigation)
        {
            _navigation = navigation;
            _termsCheckBox = "checkbox_2.png";
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
                    // _navigation.PopAsync();
                    App.Current.MainPage = new NavigationPage(new PhoneRegisterPage());
                });
            }
        }

        /// <summary>
        /// This event will be used for sign up functionality.
        /// </summary>
        public Command signUpCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    var returnMessage = CheckValidations();
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



                            string postData = "first_name=" + FirstName + "&last_name=" + LastName + "&password=" + Password + "&email=" + Email + "&id=" + SignupPage.id + "&deviceType=1" + "&deviceToken=1" + "&userType=" + SignupPage.userType + "&category=" + SignupPage.sellerType;
                            var result = await CommonLib.RegisterUser(CommonLib.ws_MainUrl + "registerUser?" + postData);
                            if (result.status == 1)
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

                                Loader.CloseAllPopup();

                                if (result.data.User.userType == "1")
                                {
                                    App.Current.MainPage = new NavigationPage(new MainPage());
                                }
                                if (result.data.User.userType == "2")
                                {
                                    App.Current.MainPage = new NavigationPage(new AddShopPage());
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

                });
            }
        }

        /// <summary>
        /// This tapped event will be used for checking the terms and conditions.
        /// </summary>
        public Command termsConditionsCommand
        {
            get
            {
                return new Command((e) =>
                {
                    if (TermsCheckBox == "checkbox_2.png")
                    {
                        TermsCheckBox = "checkbox.png";
                    }
                    else
                    {
                        TermsCheckBox = "checkbox_2.png";
                    }
                });
            }
        }

        public Command openPrivacyPolicyCommand
        {
            get
            {
                return new Command((e) =>
                {
                    _navigation.PushAsync(new PrivacyPolicyPage());
                });
            }
        }

        public Command openTermsConditionsCommand
        {
            get
            {
                return new Command((e) =>
                {
                    _navigation.PushAsync(new TermsConditionsPage());
                });
            }
        }

        public string CheckValidations()
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(FirstName))
            {
                msg += AppResources.enter_first_name_validation + Environment.NewLine;
            }
            if (SignupPage.userType == "2")
            {
                if (string.IsNullOrEmpty(Email))
                {
                    msg += AppResources.enter_email_validation + Environment.NewLine;
                }
                if (!string.IsNullOrEmpty(Email))
                {
                    if (!CommonLib.CheckValidEmail(Email.Trim()))
                    {
                        msg += AppResources.enter_valid_email_validation + Environment.NewLine;
                    }
                }


            }
            if (string.IsNullOrEmpty(Password))
            {
                msg += AppResources.enter_password_validation + Environment.NewLine;
            }
            if (!string.IsNullOrEmpty(Password))
            {
                if (Password != ConfirmPassword)
                {
                    msg += AppResources.password_confirm_password_mismatch_validation + Environment.NewLine;
                }
            }
            //if (string.IsNullOrEmpty(PhoneNumber))
            //{
            //    msg += AppResources.enter_phone_number_validation + Environment.NewLine;
            //}
            if (SignupPage.userType == "2")
            {
                if (string.IsNullOrEmpty(SignupPage.sellerType))
                {
                    msg += AppResources.sellerType + Environment.NewLine;
                }
            }

            //if (!string.IsNullOrEmpty(PhoneNumber) && PhoneNumber.Length < 10)
            //{
            //    msg += AppResources._phonevalidation + Environment.NewLine;
            //}

            //if (!string.IsNullOrEmpty(PhoneNumber) && PhoneNumber.Length > 10)
            //{
            //    msg += AppResources._phonevalidation + Environment.NewLine;
            //}
            if (TermsCheckBox == "checkbox_2.png")
            {
                msg += AppResources.terms_conditions_validation;
            }
            return msg;
        }
    }
}
