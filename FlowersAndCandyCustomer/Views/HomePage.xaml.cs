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
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace FlowersAndCandyCustomer.Views
{
    public partial class HomePage : ContentView
    {
        public static string Lat = "";
        public static string Lng = "";

        public static string catId = "0";
        public static List<HomeCategory> homeCategories = new List<HomeCategory>();
        public HomePage()
        {
            catId = "0";
            Category();


            //language
            //if (App.lang == "ar-AE")
            //{
            //    this.FlowDirection = FlowDirection.RightToLeft;
            //}
            //else
            //{
            //    this.FlowDirection = FlowDirection.LeftToRight;
            //}

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
                var result = await CommonLib.HomePageCategory(CommonLib.ws_MainUrl + "getCategories?");
                if (result.status == 1)
                {
                    homeCategories = result.data.categories;

                    foreach (var cn in result.data.categories)
                    { catId = cn.id;break; }
                   

                    //
                    InitializeComponent();
                    loadLatLng();
                    //


                    bool check = true;
                    foreach (var item in result.data.categories)
                    {
                        
                        if (check == true)
                        {

                            //
                            Label idlbl = new Label()
                            {
                                Text = item.id,IsVisible=false
                            };
                            CachedImage img = new CachedImage()
                            {
                                Source = CommonLib.img_MainUrl + item.logo,
                                HorizontalOptions = LayoutOptions.Center,
                                HeightRequest = 60,
                                WidthRequest = 60,
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
                                FontAttributes = FontAttributes.Bold,HorizontalTextAlignment=TextAlignment.Center,
                                FontFamily = "CALIBRI",
                                StyleId = "CALIBRI",
                            };
                            StackLayout _imglayout = new StackLayout()
                            {
                                Orientation = StackOrientation.Vertical,
                                HeightRequest=120,
                                BackgroundColor = Color.Transparent,
                               // Padding = new Thickness(5, 5, 5, 5),
                                Children = {
                                   idlbl, img,namelbl
                                           }
                            };
                            var lblClose_tgr = new TapGestureRecognizer();
                            lblClose_tgr.Tapped += LblClose_tgr_Tapped;
                            _imglayout.GestureRecognizers.Add(lblClose_tgr);
                            _categoryLyt.Children.Add(_imglayout);
                            //




                            //Button button = new Button()
                            //{
                            //    Text = item.name,FontAttributes=FontAttributes.Bold,
                            //    FontFamily = "CALIBRI",FontSize=16,
                            //    StyleId = "CALIBRI",
                            //    CornerRadius = 30,
                            //    TextColor = Color.White,
                            //    BackgroundColor = Color.FromHex("#FE1F78"),
                            //    WidthRequest = 150
                            //};
                            //button.Clicked += Button_Clicked;
                            //_categoryLyt.Children.Add(button);
                        }
                        else
                        {
                            //
                            Label idlbl = new Label()
                            {
                                Text = item.id,
                                IsVisible = false
                            };
                            CachedImage img = new CachedImage()
                            {
                                Source = CommonLib.img_MainUrl + item.logo,
                                HorizontalOptions = LayoutOptions.Center,
                                HeightRequest = 60,
                                WidthRequest = 60,
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
                                HeightRequest = 120,
                                BackgroundColor = Color.Transparent,
                               // Padding = new Thickness(5, 5, 5, 5),
                                Children = {
                                   idlbl, img,namelbl
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

        }

        private void LblClose_tgr_Tapped(object sender, EventArgs e)
        {
            string ID = "";
            var s = sender as StackLayout;
            var aa= s.Children;
            foreach (var item in aa)
            {
                var cn = item as Label;
                ID = cn.Text;
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
                        //
                        Label idlbl = new Label()
                        {
                            Text = item.id,
                            IsVisible = false
                        };
                        CachedImage img = new CachedImage()
                        {
                            Source = CommonLib.img_MainUrl + item.logo,
                            HorizontalOptions = LayoutOptions.Center,
                            HeightRequest = 60,
                            WidthRequest = 60,
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
                            HeightRequest = 120,
                            BackgroundColor = Color.Transparent,
                            // Padding = new Thickness(5, 5, 5, 5),
                            Children = {
                                   idlbl, img,namelbl
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
                        CachedImage img = new CachedImage()
                        {
                            Source = CommonLib.img_MainUrl + item.logo,
                            HorizontalOptions = LayoutOptions.Center,
                            HeightRequest = 60,
                            WidthRequest = 60,
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
                            HeightRequest = 120,
                            BackgroundColor = Color.Transparent,
                            // Padding = new Thickness(5, 5, 5, 5),
                            Children = {
                                   idlbl, img,namelbl
                                           }
                        };
                        var lblClose_tgr = new TapGestureRecognizer();
                        lblClose_tgr.Tapped += LblClose_tgr_Tapped;
                        _imglayout.GestureRecognizers.Add(lblClose_tgr);
                        _categoryLyt.Children.Add(_imglayout);
                        //
                    }
                }

                HomePageProductService.loaderCheck = true;
                HomePageProductViewModel model = new HomePageProductViewModel(catId);
                productListView.BindingContext = model;
            }
            catch (Exception)
            {
            }

        }

        //private void Button_Clicked(object sender, EventArgs e)
        //{

        //    try
        //    {

           
        //    var btn = (Button)sender;
        //    string btnTxt = btn.Text;

        //    _categoryLyt.Children.Clear();

        //    foreach (var item in homeCategories)
        //    {
        //        if (btnTxt == item.name)
        //        {
        //            catId = item.id;
        //            Button button = new Button()
        //            {
        //                Text = item.name,
        //                FontAttributes = FontAttributes.Bold,
        //                FontSize = 16,
        //                FontFamily = "CALIBRI",
        //                StyleId = "CALIBRI",
        //                CornerRadius = 30,
        //                TextColor = Color.White,
        //                BackgroundColor = Color.FromHex("#FE1F78"),
        //                WidthRequest = 150
        //            };
        //            button.Clicked += Button_Clicked;
        //            _categoryLyt.Children.Add(button);
        //        }
        //        else
        //        {
        //            Button button = new Button()
        //            {
        //                Text = item.name,
        //                FontFamily = "CALIBRI",
        //                StyleId = "CALIBRI",
        //                CornerRadius = 30,
        //                TextColor = Color.FromHex("#A3989C"),
        //                BackgroundColor = Color.FromHex("#FFEFF5"),
        //                WidthRequest = 150
        //            };
        //            button.Clicked += Button_Clicked;
        //            _categoryLyt.Children.Add(button);
        //        }
        //    }

        //    HomePageProductService.loaderCheck = true;
        //    HomePageProductViewModel model = new HomePageProductViewModel(catId);
        //    productListView.BindingContext = model;
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        private void ProductListView_Refreshing(object sender, EventArgs e)
        {
            HomePageProductViewModel model = new HomePageProductViewModel(catId);
            productListView.BindingContext = model;
            productListView.EndRefresh();
        }

        private async void ProductListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if(productListView.SelectedItem!= null){
                    var selectedData = productListView.SelectedItem as HomeProduct;
                    AddOrderViewModel.userId = selectedData.user_id;
                    AddOrderViewModel.catid = selectedData.category_id;

                    AddOrderViewModel.id = selectedData.id;

                    App.check = true;

                    await App.Current.MainPage.Navigation.PushAsync(new AddOrderPage());
                    productListView.SelectedItem = null; }
            }
            catch (Exception)
            {
            }
          
        }

        public async void loadLatLng()
        {

            try
            {
                var timeout = TimeSpan.FromSeconds(1);
                var locator = CrossGeolocator.Current;
                if (locator.IsGeolocationEnabled)
                {
                    locator.DesiredAccuracy = 50;
                    int tm = 1000;
                    if (Device.OS == TargetPlatform.Android) { tm = 100000; }
                    var position = await locator.GetPositionAsync(tm);
                    if (position != null)
                    {
                        Lat = position.Latitude.ToString();
                        Lng = position.Longitude.ToString();
                       

                        if (Device.RuntimePlatform == Device.iOS)
                        {
                            //firstBtn.CornerRadius = 20;
                            //secondBtn.CornerRadius = 20;
                            //thirdBtn.CornerRadius = 20;
                            //fourthBtn.CornerRadius = 20;
                        }

                        
                        //HomePageProductService.loaderCheck = true;
                        HomePageProductViewModel model = new HomePageProductViewModel(catId);
                        productListView.BindingContext = model;

                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
