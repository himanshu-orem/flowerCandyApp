using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Repository;
using Xamarin.Forms.Maps;

namespace FlowersAndCandyCustomer.SellerViews
{
	 


    public class ShopAddressSearch : ContentPage
    {
        public static int check = 0;
        ListView lstViewPlaces;
        Entry sbSearch;
        public ShopAddressSearch()
        {
            Title = "Search Address";




            sbSearch = new Entry() { Placeholder = "Search", TextColor = Color.Black };
            lstViewPlaces = new ListView()
            {
                IsPullToRefreshEnabled = true,
            };
            lstViewPlaces.SeparatorColor = Color.FromHex("#8A9C94");
            lstViewPlaces.SeparatorVisibility = SeparatorVisibility.Default;
            lstViewPlaces.ItemTemplate = new DataTemplate(typeof(CustomPlaceCell));
            lstViewPlaces.ItemSelected += lstViewPlaces_ItemSelected;
            sbSearch.TextChanged += SbSearch_TextChanged;
            var _layoutMain = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children =
                {
                sbSearch,
                lstViewPlaces
                }
            };
            Content = _layoutMain;
        }

        private void lstViewPlaces_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (lstViewPlaces.SelectedItem != null)
            {
                var place = (PredictionL)lstViewPlaces.SelectedItem;


                AddShopPage.Address = place.description;
                EditShopPage.Address = place.description;


                getPosition(place.description);
                lstViewPlaces.SelectedItem = null;
                this.Navigation.PopAsync();
            }
        }
        async void getPosition(string address)
        {

            try
            {
                Geocoder gc = new Geocoder();
                IEnumerable<Position> possibleAddresses =
                    await gc.GetPositionsForAddressAsync(address);

                foreach (var result in possibleAddresses)
                {

                    AddShopPage.Latitude = result.Latitude.ToString();
                    AddShopPage.Longitude = result.Longitude.ToString();

                    EditShopPage.Latitude = result.Latitude.ToString();
                    EditShopPage.Longitude = result.Longitude.ToString();


                    break;
                }
            }
            catch
            {
            }
        }
        private void SbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(sbSearch.Text))
            {
                GetPlaces(sbSearch.Text);
            }
            else
            {
                lstViewPlaces.ItemsSource = null;
            }
        }


        public async void GetPlaces(string searchText)
        {
            try
            {
                List<string> dictCities = new List<string>();

                string url = "https://maps.googleapis.com/maps/api/place/autocomplete/json?input=" + searchText +
                    "&types=geocode&key=AIzaSyCAW0qqikPYbOKLu_aobSw04z1Dnfhgpv4";
                wsGooglePlaces _objUserData = await CommonLib.getGooglePlaces(url);
                if (_objUserData != null && _objUserData.predictions.Count > 0)
                {
                    lstViewPlaces.ItemsSource = null;
                    lstViewPlaces.ItemsSource = _objUserData.predictions;
                }
                else if (_objUserData == null)
                {
                    await DisplayAlert("", "Internet seems to be down.", "OK");
                }
            }
            catch
            {
            }
            finally
            {
            }
        }
        public class CustomPlaceCell : ViewCell
        {
            int fontSize = 14;
            public CustomPlaceCell()
            {
                Label lblName = new Label
                {
                    FontSize = fontSize,
                    HorizontalTextAlignment = TextAlignment.Start,
                    BackgroundColor = Color.Transparent,
                    HorizontalOptions = LayoutOptions.Start,
                };
                lblName.SetBinding(Label.TextProperty, new Binding("description"));
                StackLayout _layoutRow11 = new StackLayout()
                {
                    HorizontalOptions = LayoutOptions.Start,
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 0,
                    BackgroundColor = Color.Transparent,
                    Children =
                {
                    lblName ,
                }
                };
                var sl_projectinfo = new StackLayout
                {
                    Padding = new Thickness(20),
                    Orientation = StackOrientation.Vertical,
                    BackgroundColor = Color.Transparent,
                    Children =
                    {
                       _layoutRow11,
                    }
                };
                View = sl_projectinfo;
            }
        }
    }
}