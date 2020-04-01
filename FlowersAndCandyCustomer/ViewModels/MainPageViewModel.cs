using System;
using System.ComponentModel;
using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Resources;
using FlowersAndCandyCustomer.Views;
using Xamarin.Forms;

namespace FlowersAndCandyCustomer.ViewModels
{
    public class MainPageViewModel: INotifyPropertyChanged
    {
        private string _titleText;
        public string TitleText
        {
            get
            {
                return _titleText;
            }
            set
            {
                _titleText = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TitleText"));
            }
        }

        private bool _homeVisible;
        public bool HomeVisible
        {
            get
            {
                return _homeVisible;
            }
            set
            {
                _homeVisible = value;
                PropertyChanged(this, new PropertyChangedEventArgs("HomeVisible"));
            }
        }

        private bool _orderVisible;
        public bool OrderVisible
        {
            get
            {
                return _orderVisible;
            }
            set
            {
                _orderVisible = value;
                PropertyChanged(this, new PropertyChangedEventArgs("OrderVisible"));
            }
        }


       

        private bool _profileVisible;
        public bool ProfileVisible
        {
            get
            {
                return _profileVisible;
            }
            set
            {
                _profileVisible = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ProfileVisible"));
            }
        }

        private bool _moreVisible;
        public bool MoreVisible
        {
            get
            {
                return _moreVisible;
            }
            set
            {
                _moreVisible = value;
                PropertyChanged(this, new PropertyChangedEventArgs("MoreVisible"));
            }
        }

        private Color _profileTextColor;
        public Color ProfileTextColor
        {
            get
            {
                return _profileTextColor;
            }
            set
            {
                _profileTextColor = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ProfileTextColor"));
            }
        }

        private Color _homeTextColor;
        public Color HomeTextColor
        {
            get
            {
                return _homeTextColor;
            }
            set
            {
                _homeTextColor = value;
                PropertyChanged(this, new PropertyChangedEventArgs("HomeTextColor"));
            }
        }

        private Color _orderTextColor;
        public Color OrderTextColor
        {
            get
            {
                return _orderTextColor;
            }
            set
            {
                _orderTextColor = value;
                PropertyChanged(this, new PropertyChangedEventArgs("OrderTextColor"));
            }
        }

        private Color _moreTextColor;
        public Color MoreTextColor
        {
            get
            {
                return _moreTextColor;
            }
            set
            {
                _moreTextColor = value;
                PropertyChanged(this, new PropertyChangedEventArgs("MoreTextColor"));
            }
        }

        private string _orderImage;
        public string OrderImage
        {
            get
            {
                return _orderImage;
            }
            set
            {
                _orderImage = value;
                PropertyChanged(this, new PropertyChangedEventArgs("OrderImage"));
            }
        }

        private string _homeImage;
        public string HomeImage
        {
            get
            {
                return _homeImage;
            }
            set
            {
                _homeImage = value;
                PropertyChanged(this, new PropertyChangedEventArgs("HomeImage"));
            }
        }

        private string _moreImage;
        public string MoreImage
        {
            get
            {
                return _moreImage;
            }
            set
            {
                _moreImage = value;
                PropertyChanged(this, new PropertyChangedEventArgs("MoreImage"));
            }
        }

        private string _profileImage;
        public string ProfileImage
        {
            get
            {
                return _profileImage;
            }
            set
            {
                _profileImage = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ProfileImage"));
            }
        }



        public MainPageViewModel()
        {
            _homeVisible = true;
            _orderVisible = false;
            _profileVisible = false;
            _moreVisible = false;
            _homeTextColor = Color.FromHex("#FE1F78");
            _orderTextColor = Color.Gray;
            _profileTextColor = Color.Gray;
            _moreTextColor = Color.Gray;
            _profileImage = "ico_profile.png";
            _orderImage = "ico_orders.png";
            _homeImage = "ico_home_selected.png";
            _moreImage = "ico_home.png";
            _titleText = AppResources.flowers_candy;
        }

        public Command tappedHomeViewCommand
        {
            get
            {
                return new Command(async(e) =>
                {

                    LoggedInUser objUser = App.Database.GetLoggedInUser();
                    if (objUser == null)
                    {
                        //var ans = await App.Current.MainPage.DisplayAlert("Flower&Candy", AppResources.youwantlogin, AppResources.yes, AppResources.no);
                       // if (ans)
                        {
                           // App.Current.MainPage = new NavigationPage(new LoginPage());
                        }
                    }


                    HomeVisible = true;
                    OrderVisible = false;
                    ProfileVisible = false;
                    MoreVisible = false;
                    HomeTextColor = Color.FromHex("#FE1F78");
                    OrderTextColor = Color.Gray;
                    ProfileTextColor = Color.Gray;
                    MoreTextColor = Color.Gray;
                    ProfileImage = "ico_profile.png";
                    OrderImage = "ico_orders.png";
                    MoreImage = "ico_home.png";
                    HomeImage = "ico_home_selected.png";
                    TitleText = AppResources.flowers_candy;


                });
            }
        }

        public Command tappedOrderViewCommand
        {
            get
            {
                return new Command((e) =>
                {
                    HomeVisible = false;
                    OrderVisible = true;
                    ProfileVisible = false;
                    MoreVisible = false;
                    OrderTextColor = Color.FromHex("#FE1F78");
                    HomeTextColor = Color.Gray;
                    ProfileTextColor = Color.Gray;
                    MoreTextColor = Color.Gray;
                    ProfileImage = "ico_profile.png";
                    HomeImage = "ico_home.png";
                    MoreImage = "ico_home.png";
                    OrderImage = "ico_orders_selected.png";
                    TitleText = AppResources.orders;
                });
            }
        }

        public Command tappedProfileViewCommand
        {
            get
            {
                return new Command(async(e) =>
                {

                    LoggedInUser objUser = App.Database.GetLoggedInUser();
                    if (objUser == null)
                    {
                       // var ans = await App.Current.MainPage.DisplayAlert("Flower&Candy", AppResources.youwantlogin, AppResources.yes, AppResources.no);
                       // if (ans)
                        {
                           // App.Current.MainPage = new NavigationPage(new LoginPage());
                           // await App.Current.MainPage.Navigation.PushAsync(new LoginPage());

                        }
                    }



                    HomeVisible = false;
                    OrderVisible = false;
                    ProfileVisible = true;
                    MoreVisible = false;
                    ProfileTextColor = Color.FromHex("#FE1F78");
                    HomeTextColor = Color.Gray;
                    OrderTextColor = Color.Gray;
                    MoreTextColor = Color.Gray;
                    OrderImage = "ico_orders.png";
                    HomeImage = "ico_home.png";
                    MoreImage = "ico_home.png";
                    ProfileImage = "ico_profile_selected.png";
                    TitleText = AppResources.profile;
                });
            }
        }

        public Command tappedMoreViewCommand
        {
            get
            {
                return new Command((e) =>
                {
                    HomeVisible = false;
                    OrderVisible = false;
                    ProfileVisible = false;
                    MoreVisible = true;
                    MoreTextColor = Color.FromHex("#FE1F78");
                    HomeTextColor = Color.Gray;
                    OrderTextColor = Color.Gray;
                    ProfileTextColor = Color.Gray;
                    OrderImage = "home.png";
                    HomeImage = "home.png";
                    ProfileImage = "home.png";
                    MoreImage = "home_selected.png";
                    TitleText = AppResources.profile;
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
