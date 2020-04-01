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
	public partial class DeliveryAddressPopUp : PopupPage
    {
        public static int DeliveryAddresscheck = 0;

        public static string Lat = "";
        public static string Lng = "";

        public static string Lat1 = "";
        public static string Lng1 = "";

        public static string Txt = "";

        public DeliveryAddressPopUp ()
		{
			InitializeComponent ();
            if (ShopListPage.Lat != "")
            {
                customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Convert.ToDouble(ShopListPage.Lat), Convert.ToDouble(ShopListPage.Lng)),
                                                                Distance.FromMiles(1)));
            }

            DeliveryAddresscheck = 1;

            Lat = "";
            Lng = "";

            Lat1 = "";
            Lng1 = "";

            Txt = "";

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
            var positionAds = new Position(Convert.ToDouble(Lat), Convert.ToDouble(Lng));
            var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(positionAds);
            foreach (var address in possibleAddresses)
            {
                    addressLbl.Text = "";
                    addressLbl.Text = address;



                break;

            }
                Lat1 = Lat;
                Lng1 = Lng;
                Txt = addressLbl.Text;
                CustomerPaymentPage.customerAddress = addressLbl.Text;

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
                  

                    Lat = position.Latitude.ToString();
                    Lng = position.Longitude.ToString();


                   


                    Geocoder geoCoder = new Geocoder();
                    var positionAds = new Position(position.Latitude, position.Longitude);
                    var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(positionAds);
                    foreach (var address in possibleAddresses)
                    {
                        addressLbl.Text = address;

                        break;
                    }


                    Lat1 = Lat;
                    Lng1 = Lng;
                    Txt = addressLbl.Text;
                    CustomerPaymentPage.customerAddress = addressLbl.Text;

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
               
            Lat1 = "";
            Lng1 = "";
            Txt = "";
            CustomerPaymentPage.customerAddress = "";
           CloseAllPopup();
        }







        public static async void CloseAllPopup()
        {
          
            await App.Current.MainPage.Navigation.PopPopupAsync(false);
        }

        private async void done_Clicked(object sender, EventArgs e)
        {


            Lat1 = Lat;
            Lng1 = Lng;
            Txt = addressLbl.Text;
            CustomerPaymentPage.customerAddress = addressLbl.Text;

            CloseAllPopup();
            
        }
    }
}