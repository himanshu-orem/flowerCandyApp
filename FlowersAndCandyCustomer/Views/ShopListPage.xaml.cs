using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FFImageLoading.Forms;
using FFImageLoading.Work;
using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.Resources;
using FlowersAndCandyCustomer.Services;
using FlowersAndCandyCustomer.ViewModels;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Position = Xamarin.Forms.Maps.Position;

namespace FlowersAndCandyCustomer.Views
{
	 
	public partial class ShopListPage : ContentView
	{
        public static string Address = "";
        public static string Lat = "";
        public static string Lng = "";


        public static string Lat1 = "";
        public static string Lng1 = "";

        public static string catId = "0";
        public static List<HomeCategory> homeCategories = new List<HomeCategory>();
        public ShopListPage ()
		{
            catId = "0";
            Category();

        }
        public async void loadLatLng()
        {

            try
            {
                cleanup:
                var timeout = TimeSpan.FromSeconds(1);
                var locator = CrossGeolocator.Current;
               // if (locator.IsGeolocationEnabled)
                {
                    locator.DesiredAccuracy = 50;
                    int tm = 1000;
                    if (Device.OS == TargetPlatform.Android) { tm = 100000; }
                    var position = await locator.GetPositionAsync(tm);
                    if (position != null)
                    {
                        Lat =  position.Latitude.ToString();
                        Lng =  position.Longitude.ToString();

                       
                        

                        if (!string.IsNullOrEmpty(Lat) && !string.IsNullOrEmpty(Lat))
                        {
                            ShopListService.loaderCheck = true;
                            ShopListViewModel model = new ShopListViewModel(SearchTxt.Text);
                            shopListView.BindingContext = model;
                        }

                       // HomePageProductViewModel modelOffer = new HomePageProductViewModel("");
                       // productListView.BindingContext = modelOffer;

                    }
                }
                if (string.IsNullOrEmpty(Lat) && string.IsNullOrEmpty(Lat))
                {
                    goto cleanup;
                }
            }
            catch (Exception ex)
            {
            }
        }
        public async void DeviceTokken()
        {
            try
            {
                string type = "android";
                if (Device.OS == TargetPlatform.iOS)
                {
                    type = "ios";
                }

                LoggedInUser objUser = App.Database.GetLoggedInUser();

                string postData = "id=" + objUser.userId + "&deviceType=" + type + "&deviceToken=" + App.DeviceToken;
                var result = await CommonLib.UpdateToken(CommonLib.ws_MainUrl + "updateToken?" + postData);
                if (result.status == 1)
                {

                }
            }
            catch (Exception ex)
            {
            }
        }
        private void LblClose_tgr_Tapped(object sender, EventArgs e)
        {
            string ID = "";
            var s = sender as StackLayout;
            var aa = s.Children;
            int count = 1;
            foreach (var item in aa)
            {
                if (count > 2)
                {
                    break;
                }
                else
                {
                    var cn = item as Label;
                    ID = cn.Text;
                    count++;
                }
            }


            string OFFER = "";
            var sOFFER = sender as StackLayout;
            var aaOFFER = sOFFER.Children;
            foreach (var item in aaOFFER)
            {
                var cn = item as Label;
                OFFER = cn.Text;
                break;
            }



            try
            {




                _categoryLyt.Children.Clear();

                foreach (var item in homeCategories)
                {
                    if (ID == item.id)
                    {
                        catId = item.id;
                        ShopProducts.shopOffer = OFFER;
                        //
                        Label idlbl = new Label()
                        {
                            Text = item.id,
                            IsVisible = false
                        };
                        Label offerlbl = new Label()
                        {
                            Text = string.IsNullOrEmpty(item.is_offer) ? "" : item.is_offer,
                            IsVisible = false
                        };


                        CachedImage img = new CachedImage()
                        {
                            Source = CommonLib.img_MainUrl + item.logo,
                            HorizontalOptions = LayoutOptions.Center,
                            HeightRequest = 80,
                            WidthRequest = 80,
                            Aspect = Aspect.AspectFit,
                            LoadingPlaceholder = "product_Placeholder.png",
                            Transformations = new List<ITransformation>()
                                {
                                   new FFImageLoading.Transformations.CircleTransformation()
                                   {
                                       BorderSize=20, BorderHexColor="#FE1F78"
                                   }
                                },
                        };
                        Label namelbl = new Label()
                        {
                            Text = item.name,
                            TextColor = Color.FromHex("#FE1F78"),
                            FontSize = 14,
                            FontAttributes = FontAttributes.Bold,
                            HorizontalTextAlignment = TextAlignment.Center,
                            FontFamily = "CALIBRI",
                            StyleId = "CALIBRI",
                        };
                        StackLayout _imglayout = new StackLayout()
                        {
                            Orientation = StackOrientation.Vertical, 
                           // HeightRequest = 150,
                            Spacing = 0,
                            BackgroundColor = Color.Transparent,
                            // Padding = new Thickness(5, 5, 5, 5),
                            Children = {
                                  offerlbl, idlbl, img,namelbl
                                           }
                        };
                        var lblClose_tgr = new TapGestureRecognizer();
                        lblClose_tgr.Tapped += LblClose_tgr_Tapped;
                        _imglayout.GestureRecognizers.Add(lblClose_tgr);
                        _categoryLyt.Children.Add(_imglayout);
                        //
                    }
                    else
                    {
                        //
                        Label idlbl = new Label()
                        {
                            Text = item.id,
                            IsVisible = false
                        };
                        Label offerlbl = new Label()
                        {
                            Text = string.IsNullOrEmpty(item.is_offer) ? "" : item.is_offer,
                            IsVisible = false
                        };
                        CachedImage img = new CachedImage()
                        {
                            Source = CommonLib.img_MainUrl + item.logo,
                            HorizontalOptions = LayoutOptions.Center,
                            HeightRequest = 80,
                            WidthRequest = 80,
                            Aspect = Aspect.AspectFit,
                            LoadingPlaceholder = "product_Placeholder.png",
                            Transformations = new List<ITransformation>()
                                {
                                   new FFImageLoading.Transformations.CircleTransformation()
                                   {
                                       BorderSize=20, BorderHexColor="#68696b"
                                   }
                                },
                        };
                        Label namelbl = new Label()
                        {
                            Text = item.name,
                            TextColor = Color.FromHex("#68696b"),
                            FontSize = 14,
                            FontAttributes = FontAttributes.Bold,
                            HorizontalTextAlignment = TextAlignment.Center,
                            FontFamily = "CALIBRI",
                            StyleId = "CALIBRI",
                        };
                        StackLayout _imglayout = new StackLayout()
                        {
                            Orientation = StackOrientation.Vertical,
                          //  HeightRequest = 150,
                            Spacing = 0,
                            BackgroundColor = Color.Transparent,
                            // Padding = new Thickness(5, 5, 5, 5),
                            Children = {
                                 offerlbl,  idlbl, img,namelbl
                                           }
                        };
                        var lblClose_tgr = new TapGestureRecognizer();
                        lblClose_tgr.Tapped += LblClose_tgr_Tapped;
                        _imglayout.GestureRecognizers.Add(lblClose_tgr);
                        _categoryLyt.Children.Add(_imglayout);
                        //
                    }
                }

                if (OFFER == "1")
                {
                    productListView.IsVisible = true;
                    shopListView.IsVisible = false;
                }
                else
                {
                    productListView.IsVisible = false;
                    shopListView.IsVisible = true;
                }

               // HomePageProductViewModel modelOffer = new HomePageProductViewModel("");
                //productListView.BindingContext = modelOffer;

                ShopListService.loaderCheck = true;
                ShopListViewModel model = new ShopListViewModel(SearchTxt.Text);
                shopListView.BindingContext = model;
            }
            catch (Exception)
            {
            }

        }
        private async void ShopListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (shopListView.SelectedItem != null)
            {
                var data = shopListView.SelectedItem as ShopList;

                if (data.is_close == "False")
                {
                    App.check = true;
                    ShopProducts.shopId = data.id;
                    ShopProducts.shopOffer = data.is_offer;
                    await App.Current.MainPage.Navigation.PushAsync(new ShopProducts());
                }
            }
            shopListView.SelectedItem = null;
        }

        private void ShopListView_Refreshing(object sender, EventArgs e)
        {
            ShopListViewModel model = new ShopListViewModel(SearchTxt.Text);
            shopListView.BindingContext = model;
            shopListView.EndRefresh();
        }


        public async void Category()
        {
            try
            {


                if (!CommonLib.checkconnection())
                {
                    await Navigation.PushPopupAsync(new ShowMessage(AppResources._connection));
                    await Task.Delay(1000);
                    ShowMessage.CloseAllPopup();
                    return;
                }
                try
                {
                    await App.Current.MainPage.Navigation.PushPopupAsync(new Loader());
                }
                catch (Exception ex)
                {
                }

                var result = await CommonLib.HomePageCategory(CommonLib.ws_MainUrl + "getCategories?");
                if (result.status == 1)
                {
                    homeCategories = result.data.categories;
                   
                    foreach (var cn in result.data.categories)
                    { catId = cn.id; ShopProducts.shopOffer=string.IsNullOrEmpty(cn.is_offer) ? "" : cn.is_offer;
                        
                        break;
                    }

                   
                    //
                    InitializeComponent();
                   

                    if (ShopProducts.shopOffer == "1")
                    {
                        productListView.IsVisible = true;
                        shopListView.IsVisible = false;
                    }
                    else
                    {
                        productListView.IsVisible = false;
                        shopListView.IsVisible = true;
                    }
                    loadLatLng();
                    DeviceTokken();
                    //


                    bool check = true;
                    foreach (var item in result.data.categories)
                    {

                        if (check == true)
                        {

                            //
                            Label idlbl = new Label()
                            {
                                Text = item.id,
                                IsVisible = false
                            };
                            Label offerlbl = new Label()
                            {
                                Text = string.IsNullOrEmpty(item.is_offer)?"": item.is_offer,
                                IsVisible = false
                            };
                            CachedImage img = new CachedImage()
                            {
                                Source = CommonLib.img_MainUrl + item.logo,
                                HorizontalOptions = LayoutOptions.Center,
                                HeightRequest = 80,
                                WidthRequest = 80,
                                Aspect = Aspect.AspectFit,
                                LoadingPlaceholder = "product_Placeholder.png",
                                Transformations = new List<ITransformation>()
                                {
                                   new FFImageLoading.Transformations.CircleTransformation()
                                   {
                                       BorderSize=20, BorderHexColor="#FE1F78"
                                   }
                                },
                            };
                            Label namelbl = new Label()
                            {
                                Text = item.name,
                                TextColor = Color.FromHex("#FE1F78"),
                                FontSize = 14,
                                FontAttributes = FontAttributes.Bold,
                                HorizontalTextAlignment = TextAlignment.Center,
                                FontFamily = "CALIBRI",
                                StyleId = "CALIBRI",
                            };
                            StackLayout _imglayout = new StackLayout()
                            {
                                Orientation = StackOrientation.Vertical,
                               // HeightRequest = 150,
                                Spacing =0,
                                BackgroundColor = Color.Transparent,
                                // Padding = new Thickness(5, 5, 5, 5),
                                Children = {
                                 offerlbl,  idlbl, img,namelbl
                                           }
                            };
                            var lblClose_tgr = new TapGestureRecognizer();
                            lblClose_tgr.Tapped += LblClose_tgr_Tapped;
                            _imglayout.GestureRecognizers.Add(lblClose_tgr);
                            _categoryLyt.Children.Add(_imglayout);
                            //



                         
                        }
                        else
                        {
                            //
                            Label idlbl = new Label()
                            {
                                Text = item.id,
                                IsVisible = false
                            };
                            Label offerlbl = new Label()
                            {
                                Text = string.IsNullOrEmpty(item.is_offer) ? "" : item.is_offer,
                                IsVisible = false
                            };
                            CachedImage img = new CachedImage()
                            {
                                Source = CommonLib.img_MainUrl + item.logo,
                                HorizontalOptions = LayoutOptions.Center,
                                HeightRequest = 80,
                                WidthRequest = 80,
                                Aspect = Aspect.AspectFit,
                                LoadingPlaceholder = "product_Placeholder.png",
                                Transformations = new List<ITransformation>()
                                {
                                   new FFImageLoading.Transformations.CircleTransformation()
                                   {
                                       BorderSize=20, BorderHexColor="#68696b"
                                   }
                                },
                            };
                            Label namelbl = new Label()
                            {
                                Text = item.name,
                                TextColor = Color.FromHex("#68696b"),
                                FontSize = 14,
                                FontAttributes = FontAttributes.Bold,
                                HorizontalTextAlignment = TextAlignment.Center,
                                FontFamily = "CALIBRI",
                                StyleId = "CALIBRI",
                            };
                            StackLayout _imglayout = new StackLayout()
                            {
                                Orientation = StackOrientation.Vertical,
                               // HeightRequest = 150,
                                Spacing = 0,
                                BackgroundColor = Color.Transparent,
                                // Padding = new Thickness(5, 5, 5, 5),
                                Children = {
                                  offerlbl, idlbl, img,namelbl
                                           }
                            };
                            var lblClose_tgr = new TapGestureRecognizer();
                            lblClose_tgr.Tapped += LblClose_tgr_Tapped;
                            _imglayout.GestureRecognizers.Add(lblClose_tgr);
                            _categoryLyt.Children.Add(_imglayout);
                            //


                            //Button button = new Button()
                            //{
                            //    Text = item.name,
                            //    FontFamily = "CALIBRI",
                            //    StyleId = "CALIBRI",
                            //    CornerRadius = 30,
                            //    TextColor = Color.FromHex("#A3989C"),
                            //    BackgroundColor = Color.FromHex("#FFEFF5"),
                            //    WidthRequest = 150
                            //};
                            //button.Clicked += Button_Clicked;
                            //_categoryLyt.Children.Add(button);
                        }
                        check = false;
                    }
                }
            }
            catch (Exception ex)
            {

            }
           // Loader.CloseAllPopup();
        }

        private async void Address_Tapped(object sender, EventArgs e)
        {
            //SearchShopPopup searchShopPopup = new SearchShopPopup();
            //searchShopPopup.Disappearing += SearchShopPopup_Disappearing;    
            //await App.Current.MainPage.Navigation.PushPopupAsync(searchShopPopup);

            ShopListService.loaderCheck = true;
            ShopListViewModel model = new ShopListViewModel(SearchTxt.Text);
            shopListView.BindingContext = model;


           




        }

        private void SearchShopPopup_Disappearing(object sender, EventArgs e)
        {
            if (SearchShopPopup.loading == false)
            {
                ShopListService.loaderCheck = true;
                ShopListViewModel model = new ShopListViewModel(SearchTxt.Text);
                shopListView.BindingContext = model;

              //  HomePageProductViewModel modelOffer = new HomePageProductViewModel("");
               // productListView.BindingContext = modelOffer;

               // adsLbl.Text = Address;
            }
            SearchShopPopup.loading = false;
        }

        private async void currentLocation_Tapped(object sender, EventArgs e)
        {
            //try
            //{
            //    await App.Current.MainPage.Navigation.PushPopupAsync(new Loader());
            //    var timeout = TimeSpan.FromSeconds(1);
            //    var locator = CrossGeolocator.Current;
            //    if (locator.IsGeolocationEnabled)
            //    {
            //        locator.DesiredAccuracy = 50;
            //        var position = await locator.GetPositionAsync(timeoutMilliseconds: 100000);
            //        if (position != null)
            //        {
            //            Lat = position.Latitude.ToString();
            //            Lng = position.Longitude.ToString();

            //            Geocoder geoCoder = new Geocoder();
            //            var positionAds = new Position(position.Latitude, position.Longitude);
            //            var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(positionAds);
            //            foreach (var address in possibleAddresses)
            //            {
            //                Address = address;
            //                adsLbl.Text = Address; break;
            //            }
            //            Loader.CloseAllPopup1();

            //            HomePageProductViewModel modelOffer = new HomePageProductViewModel("");
            //            productListView.BindingContext = modelOffer;

            //            ShopListService.loaderCheck = true;
            //            ShopListViewModel model = new ShopListViewModel();
            //            shopListView.BindingContext = model;

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Loader.CloseAllPopup1();
            //}
            SearchShopPopup searchShopPopup = new SearchShopPopup();
            searchShopPopup.Disappearing += SearchShopPopup_Disappearing;
            await App.Current.MainPage.Navigation.PushPopupAsync(searchShopPopup);
        }



        private void ProductListView_Refreshing(object sender, EventArgs e)
        {
            HomePageProductViewModel model = new HomePageProductViewModel("");
            productListView.BindingContext = model;
            productListView.EndRefresh();
        }

        private async void ProductListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (productListView.SelectedItem != null)
                {
                    var selectedData = productListView.SelectedItem as HomeProduct;
                    AddOrderViewModel.userId = selectedData.user_id;
                    AddOrderViewModel.catid = selectedData.category_id;
                    AddOrderViewModel.id = selectedData.id;

                    App.check = true;

                    await App.Current.MainPage.Navigation.PushAsync(new AddOrderPage());
                    productListView.SelectedItem = null;
                }
            }
            catch (Exception)
            {
            }

        }

        private void SearchTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShopListViewModel model = new ShopListViewModel(SearchTxt.Text);
            shopListView.BindingContext = model;
        }
    }
}