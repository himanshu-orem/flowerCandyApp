using System;
using System.ComponentModel;
using System.Threading.Tasks;
using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.Resources;
using FlowersAndCandyCustomer.Views;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace FlowersAndCandyCustomer.ViewModels
{
    public class ChangePasswordViewModel : INotifyPropertyChanged
    {
        public INavigation _navigation;
        /// <summary>
        /// property for the current password field
        /// </summary>
        private string _currentPassword;
        public string CurrentPassword
        {
            get
            {
                return _currentPassword;
            }
            set
            {
                _currentPassword = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CurrentPassword"));
            }
        }

        /// <summary>
        /// property for the new password field
        /// </summary>
        private string _newPassword;
        public string NewPassword
        {
            get
            {
                return _newPassword;
            }
            set
            {
                _newPassword = value;
                PropertyChanged(this, new PropertyChangedEventArgs("NewPassword"));
            }
        }

        /// <summary>
        /// property for the confirm new password field
        /// </summary>
        private string _confirmNewPassword;
        public string ConfirmNewPassword
        {
            get
            {
                return _confirmNewPassword;
            }
            set
            {
                _confirmNewPassword = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ConfirmNewPassword"));
            }
        }
        public ChangePasswordViewModel(INavigation navigation)
        {
            _navigation = navigation;
        }

        /// <summary>
        /// This event will be used for sign up functionality.
        /// </summary>
        public Command changePasswordCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    var returnMessage = CheckValidations();
                    if (!string.IsNullOrEmpty(returnMessage))
                    {
                        await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(returnMessage));
                        await Task.Delay(1000);
                        await App.Current.MainPage.Navigation.PopPopupAsync();
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

                            LoggedInUser objUser = App.Database.GetLoggedInUser();

                            string postData = "user_id=" + objUser.userId + "&oldPass=" + CurrentPassword + "&newPass=" + NewPassword;
                            var result = await CommonLib.ChangePassword(CommonLib.ws_MainUrl + "changePassword?" + postData);
                            if (result.status == 1)
                            {


                                Loader.CloseAllPopup();
                                App.Database.ClearLoginDetails();
                                if (App.Lng == "ar-AE")
                                {
                                    await App.Current.MainPage.DisplayAlert("", result.msg_ar, "OK");

                                }
                                else
                                {
                                    await App.Current.MainPage.DisplayAlert("", result.msg_en, "OK");

                                }


                                App.Current.MainPage = new NavigationPage(new LoginPage());

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

        public Command BackImgTapped
        {
            get
            {
                return new Command((e) =>
                {
                    App.Current.MainPage.Navigation.PopAsync();
                });
            }
        }


        public string CheckValidations()
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(CurrentPassword))
            {
                msg += AppResources.enter_current_password_validation + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(NewPassword))
            {
                msg += AppResources.enter_new_password_validation + Environment.NewLine;
            }
            if (!string.IsNullOrEmpty(CurrentPassword))
            {
                if (NewPassword != ConfirmNewPassword)
                {
                    msg += AppResources.password_confirm_password_mismatch_validation + Environment.NewLine;
                }
            }
            if (!string.IsNullOrEmpty(CurrentPassword) && !string.IsNullOrEmpty(NewPassword))
            {
                if (CurrentPassword == NewPassword)
                {
                    msg += AppResources.current_old_password_matched_validation + Environment.NewLine;
                }
            }
            return msg;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
