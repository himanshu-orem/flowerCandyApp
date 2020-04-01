using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FlowersAndCandyCustomer.DependencyInterface;
using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Resources;
using FlowersAndCandyCustomer.Views;
using Xamarin.Forms;
using Xamarin.Forms.OpenWhatsApp;

namespace FlowersAndCandyCustomer.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        ObservableCollection<ProfileModel> _list;

        private ObservableCollection<ProfileModel> _profileList;
        public ObservableCollection<ProfileModel> ProfileList
        {
            get
            {
                return _profileList;

            }
            set
            {
                _profileList = value;
                OnPropertyChanged();
            }
        }

        private ProfileModel _profileItemSelected;

        public ProfileModel ProfileItemSelected
        {
            get { return _profileItemSelected; }
            set
            {
                _profileItemSelected = value;
                if (_profileItemSelected != null)
                {
                    OnPropertyChanged();
                    ShowDetailPage();
                }
            }
        }

        public ProfileViewModel()
        {
            LoggedInUser objUser = App.Database.GetLoggedInUser();

            _list = new ObservableCollection<ProfileModel>();
            if (objUser != null)
            {
                _list.Add(new ProfileModel
                {
                    Name = AppResources.profile_settings,
                    Image = "ico_menu_profile.png"
                });
                _list.Add(new ProfileModel
                {
                    Name = AppResources.change_password,
                    Image = "ico_password.png"
                }); 
                if (objUser.userType == "1")
                {
                    _list.Add(new ProfileModel
                    {
                        Name = AppResources.saved_Cards,
                        Image = "ico_saved_cards.png"
                    });
                }
            }

            _list.Add(new ProfileModel
            {
                Name = AppResources.about_us, Image= "ico_about.png"
            });

            _list.Add(new ProfileModel
            {
                Name = AppResources.contact_us,
                Image = "ic_contact_us.png"
            });

            _list.Add(new ProfileModel
            {
                Name = AppResources.terms_conditions,Image= "ico_terms.png"
            });
            _list.Add(new ProfileModel
            {
                Name = AppResources.setting,
                Image = "ic_settings.png"
            });


            if (objUser != null)
            {
                if (objUser.userType == "1")
                {
                    _list.Add(new ProfileModel
                    {
                        Name = AppResources.logout,
                        Image = "ico_logout"
                    });
                }
            }

            if (objUser == null)
            {
                    _list.Add(new ProfileModel
                    {
                        Name = AppResources.loginUser,
                        Image = "ico_logout"
                    });
            }

            ProfileList = _list;

            RaisePropertyChanged(nameof(ProfileList));
        }

        async void ShowDetailPage()
        {
            if (ProfileItemSelected.Name == AppResources.loginUser)
            {
               // App.Current.MainPage = new NavigationPage(new LoginPage());
                await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
            }
            if (ProfileItemSelected.Name == AppResources.terms_conditions)
            {
                await App.Current.MainPage.Navigation.PushAsync(new TermsConditionsPage());
            }
            if (ProfileItemSelected.Name == AppResources.setting)
            {
                await App.Current.MainPage.Navigation.PushAsync(new ChangeLanguagePage());
            }
            if (ProfileItemSelected.Name == AppResources.about_us)
            {
                await App.Current.MainPage.Navigation.PushAsync(new AboutUsPage());
            }
            if (ProfileItemSelected.Name == AppResources.saved_Cards)
            {
                await App.Current.MainPage.Navigation.PushAsync(new SavedCardsPage());
            }


            if (ProfileItemSelected.Name == AppResources.contact_us)
            {


                // Device.OpenUri(new Uri(String.Format("tel:{0}", ProfileContentView.phone)));
                var ans = await App.Current.MainPage.DisplayActionSheet("Flower&Candy", "Cancel", null, "Whatsapp", "Twitter");
                if (ans == "Whatsapp")
                {
                    try
                    {
                        Device.OpenUri(new Uri("whatsapp://send?phone=" + ProfileContentView.phone));

                    }
                    catch (Exception ex)
                    {
                        await App.Current.MainPage.DisplayAlert("Not Installed", "Whatsapp Not Installed", "OK");

                    }
                }
                if (ans == "Twitter")
                {
                    try
                    {
                        Device.OpenUri(new Uri("https://twitter.com/FlowerAndCandyS"));


                    }
                    catch (Exception ex)
                    {
                        await App.Current.MainPage.DisplayAlert("Not Installed", "Twitter Not Installed", "OK");

                    }
                }
            }


            if (ProfileItemSelected.Name == AppResources.change_password)
            {
                await App.Current.MainPage.Navigation.PushAsync(new ChangePasswordPage());
            }
            if (ProfileItemSelected.Name == AppResources.profile_settings)
            {
                await App.Current.MainPage.Navigation.PushAsync(new EditProfilePage());
            }
            if (ProfileItemSelected.Name == AppResources.logout)
            {
                var ans = await App.Current.MainPage.DisplayAlert("", AppResources.logout_text, AppResources.yes, AppResources.no);
                if (ans)
                {
                    App.Database.ClearLoginDetails();
                    AddNotePage.notes.Clear();
                    App.Database.ClearProduc();
                    App.Database.ClearAddon();
                    App.Current.MainPage = new LoginPage();
                }
            }



            LoggedInUser objUser = App.Database.GetLoggedInUser();

            _list = new ObservableCollection<ProfileModel>();
            if (objUser != null)
            {
                _list.Add(new ProfileModel
                {
                    Name = AppResources.profile_settings,
                    Image = "ico_menu_profile.png"
                });
                _list.Add(new ProfileModel
                {
                    Name = AppResources.change_password,
                    Image = "ico_password.png"
                });
                if (objUser.userType == "1")
                {
                    _list.Add(new ProfileModel
                    {
                        Name = AppResources.saved_Cards,
                        Image = "ico_saved_cards.png"
                    });
                }
            }

            _list.Add(new ProfileModel
            {
                Name = AppResources.about_us,
                Image = "ico_about.png"
            });
            _list.Add(new ProfileModel
            {
                Name = AppResources.contact_us,
                Image = "ic_contact_us.png"
            });
            _list.Add(new ProfileModel
            {
                Name = AppResources.terms_conditions,
                Image = "ico_terms.png"
            });
            _list.Add(new ProfileModel
            {
                Name = AppResources.setting,
                Image = "ic_settings.png"
            });
            if (objUser != null)
            {
                if (objUser.userType == "1")
                {
                    _list.Add(new ProfileModel
                    {
                        Name = AppResources.logout,
                        Image = "ico_logout"
                    });
                }
            }
            if (objUser == null)
            {
                _list.Add(new ProfileModel
                {
                    Name = AppResources.loginUser,
                    Image = "ico_logout"
                });
            }
            ProfileList = _list;

            RaisePropertyChanged(nameof(ProfileList));

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
