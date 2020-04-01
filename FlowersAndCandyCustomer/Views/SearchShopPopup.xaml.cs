using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.Resources;
using Plugin.Geolocator;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace FlowersAndCandyCustomer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchShopPopup : PopupPage
    {
        public static int check = 0;
        public static bool loading = false;
        ListView lstViewPlaces;
        Entry sbSearch;
        Button btn;
        public SearchShopPopup()
        {
            InitializeComponent();
            if (ShopListPage.Lat != "")
            {
                customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Convert.ToDouble(ShopListPage.Lat), Convert.ToDouble(ShopListPage.Lng)),
                                                                Distance.FromMiles(1)));
            }
            DeliveryAddressPopUp.DeliveryAddresscheck = 0;

            loading = false;

            customMap.HeightRequest = App.ScreenHeight - 150;



            loadMap();

            MessagingCenter.Subscribe<LoggedInUser>(this, "test", (sender) =>
            {
                textChange();
            });
        }
        public async void textChange()
        {
            try
            {

                await Navigation.PushPopupAsync(new Loader());

                Geocoder geoCoder = new Geocoder();
                var positionAds = new Position(Convert.ToDouble(ShopListPage.Lat1), Convert.ToDouble(ShopListPage.Lng1));
                var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(positionAds);
                foreach (var address in possibleAddresses)
                {
                    addressLbl.Text = "";
                    addressLbl.Text = address;

                    break;
                }
                Loader.CloseAllPopup();
            }
            catch (Exception)
            {
                Loader.CloseAllPopup();
            }
        }
        public async void loadMap()
        {

            try
            {

                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync(10000);

                outer:
                if (position != null)
                {
                    customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude),
                                                             Distance.FromMiles(1)));
                    ShopListPage.Lat1 = position.Latitude.ToString();
                    ShopListPage.Lng1 = position.Longitude.ToString();


                    Geocoder geoCoder = new Geocoder();
                    var positionAds = new Position(position.Latitude, position.Longitude);
                    var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(positionAds);
                    foreach (var address in possibleAddresses)
                    {
                        addressLbl.Text = address;

                        break;
                    }
                }
                else
                {
                    goto outer;
                }
            }
            catch (Exception)
            {

            }
        }


        private void Btn_Clicked(object sender, EventArgs e)
        {
            loading = true;
            CloseAllPopup();
        }







        public static async void CloseAllPopup()
        {
            await App.Current.MainPage.Navigation.PopPopupAsync(false);
        }

        private async void done_Clicked(object sender, EventArgs e)
        {
            try
            {



                ShopListPage.Lat = ShopListPage.Lat1;
                ShopListPage.Lng = ShopListPage.Lng1;

                Geocoder geoCoder = new Geocoder();
                var positionAds = new Position(Convert.ToDouble(ShopListPage.Lat1), Convert.ToDouble(ShopListPage.Lng1));
                var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(positionAds);
                foreach (var address in possibleAddresses)
                {
                    ShopListPage.Address = address;
                    CloseAllPopup();
                    break;
                }
            }
            catch (Exception ex)
            {
            }
        }
    }

}