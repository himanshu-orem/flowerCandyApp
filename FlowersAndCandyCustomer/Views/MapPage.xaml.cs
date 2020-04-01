using FlowersAndCandyCustomer.CustomControls;
using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.Resources;
using Plugin.Geolocator;
using Rg.Plugins.Popup.Extensions;
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
    public partial class MapPage : ContentPage
    {
        public static List<PloyLineRoutes> poliList = new List<PloyLineRoutes>();
        public static string PickUpLatitude = "";
        public static string PickUpLongitude = "";

        public static string DriverName = "";
        public static string DriverPhone = "";
        public static string DriverImage = "";


        public static string DestinationLatitude = "";
        public static string DestinationLongitude = "";

        public static string ShopLatitude = "";
        public static string ShopLongitude = "";
        public static string ShopName = "";
        public static string Time = "";

        public MapPage()
        {
            NavigationPage.SetHasBackButton(this, false);
            getPolyLine();
        }

        //public void loadLatLng()
        //{


        //    try
        //    {
        //        if (poliList != null)
        //        {
        //            double destination_Latitude = 0;
        //            double destination_Longitude = 0;
        //            double dic = Calc(Convert.ToDouble(PickUpLatitude), Convert.ToDouble(PickUpLongitude), Convert.ToDouble(DestinationLatitude), Convert.ToDouble(DestinationLongitude));


        //            var start_pin = new CustomPin
        //            {
        //                Pin = new Pin
        //                {
        //                    Type = PinType.Place,
        //                    Position = new Position(Convert.ToDouble(PickUpLatitude), Convert.ToDouble(PickUpLongitude)),
        //                    Label = "Driver",
        //                },
        //                Id = "Xamarin",
        //                startPin = true

        //            };
        //            var destination_pin = new CustomPin
        //            {
        //                Pin = new Pin
        //                {
        //                    Type = PinType.Place,
        //                    Position = new Position(Convert.ToDouble(DestinationLatitude), Convert.ToDouble(DestinationLongitude)),
        //                    Label = "Customer",
        //                },
        //                Id = "1",
        //            };

        //            var shop_pin = new CustomPin
        //            {
        //                Pin = new Pin
        //                {
        //                    Type = PinType.Place,
        //                    Position = new Position(Convert.ToDouble(ShopLatitude), Convert.ToDouble(ShopLongitude)),
        //                    Label = ShopName,
        //                },
        //                Id = "Shop",
        //            };



        //            customMap.CustomPins = new List<CustomPin> { start_pin, destination_pin, shop_pin };
        //            if (Device.OS == TargetPlatform.iOS)
        //            {
        //                customMap.Pins.Add(start_pin.Pin);
        //                customMap.Pins.Add(destination_pin.Pin);
        //                customMap.Pins.Add(shop_pin.Pin);

        //            }

        //            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Convert.ToDouble(PickUpLatitude), Convert.ToDouble(PickUpLongitude)), Xamarin.Forms.Maps.Distance.FromKilometers(dic)));



        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}
        public async void getPolyLine()
        {
            if (!string.IsNullOrEmpty(PickUpLatitude) && !string.IsNullOrEmpty(PickUpLongitude))
            {

                try
                {




                    await Navigation.PushPopupAsync(new Loader());

                    try
                    {
                        InitializeComponent();
                        DriverDetails();
                        try
                        {
                            if (!string.IsNullOrEmpty(PickUpLatitude) && !string.IsNullOrEmpty(PickUpLongitude))
                            {
                                customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Convert.ToDouble(PickUpLatitude), Convert.ToDouble(PickUpLongitude)),
                                                                    Distance.FromMiles(1)));
                            }
                            else
                            {
                                var locator = CrossGeolocator.Current;
                                var position = await locator.GetPositionAsync(10000);
                                customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude),
                                                                             Distance.FromMiles(1)));
                            }

                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            RootObjectPolyLine obj = new RootObjectPolyLine();
                            obj = await CommonLib.GetPolyLine(Convert.ToDouble(PickUpLatitude), Convert.ToDouble(PickUpLongitude), Convert.ToDouble(DestinationLatitude), Convert.ToDouble(DestinationLongitude));
                            string points = string.Empty;
                            foreach (var data in obj.routes)
                            {
                                points = data.overview_polyline.points;
                                foreach (var time in data.legs)
                                {
                                    Time = time.duration.text;
                                    timeTxt.Text = Time;
                                }
                            }


                         

                            poliList = CommonLib.DecodePoly(points);
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {


                            double dic = Calc(Convert.ToDouble(PickUpLatitude), Convert.ToDouble(PickUpLongitude), Convert.ToDouble(DestinationLatitude), Convert.ToDouble(DestinationLongitude));
                            if (poliList != null)
                            {
                                double destination_Latitude = 0;
                                double destination_Longitude = 0;
                                foreach (var data in poliList)
                                {
                                    customMap.RouteCoordinates.Add(new Position(data.Latitude, data.Longitude));
                                    destination_Latitude = data.Latitude;
                                    destination_Longitude = data.Longitude;
                                }


                                var start_pin = new CustomPin
                                {
                                    Pin = new Pin
                                    {
                                        Type = PinType.Place,
                                        Position = new Position(Convert.ToDouble(PickUpLatitude), Convert.ToDouble(PickUpLongitude)),
                                        Label = "Driver",
                                    },
                                    Id = "Xamarin",
                                    startPin = true

                                };
                                var destination_pin = new CustomPin
                                {
                                    Pin = new Pin
                                    {
                                        Type = PinType.Place,
                                        Position = new Position(Convert.ToDouble(DestinationLatitude), Convert.ToDouble(DestinationLongitude)),
                                        Label = "Customer",
                                    },
                                    Id = "1",
                                };

                                //var shop_pin = new CustomPin
                                //{
                                //    Pin = new Pin
                                //    {
                                //        Type = PinType.Place,
                                //        Position = new Position(Convert.ToDouble(ShopLatitude), Convert.ToDouble(ShopLongitude)),
                                //        Label = ShopName,
                                //    },
                                //    Id = "Shop",
                                //};
                                customMap.CustomPins = new List<CustomPin> { start_pin, destination_pin };
                                if (Device.OS == TargetPlatform.iOS)
                                {
                                    customMap.Pins.Add(start_pin.Pin);
                                    customMap.Pins.Add(destination_pin.Pin);
                                }

                                customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Convert.ToDouble(PickUpLatitude), Convert.ToDouble(PickUpLongitude)), Xamarin.Forms.Maps.Distance.FromKilometers(dic)));

                            }
                        }
                        catch (Exception ex)
                        {

                        }


                    }
                    catch (Exception ex)
                    {
                    }

                    //




                    Loader.CloseAllPopup();

                }
                catch (Exception)
                {
                    Loader.CloseAllPopup();
                }
                //timer
                Device.StartTimer(TimeSpan.FromSeconds(10), () =>
                {

                    getPolyLine1();
                    return true;
                });
            }
            else
            {
                //
                try
                {




                    await Navigation.PushPopupAsync(new Loader());

                    try
                    {
                        InitializeComponent();
                        DriverDetails();
                        try
                        {
                            if (!string.IsNullOrEmpty(DestinationLatitude) && !string.IsNullOrEmpty(DestinationLongitude))
                            {
                                customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Convert.ToDouble(DestinationLatitude), Convert.ToDouble(DestinationLongitude)),
                                                                    Distance.FromMiles(1)));
                            }
                            else
                            {
                                var locator = CrossGeolocator.Current;
                                var position = await locator.GetPositionAsync(10000);
                                customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude),
                                                                             Distance.FromMiles(1)));
                            }

                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            RootObjectPolyLine obj = new RootObjectPolyLine();
                            obj = await CommonLib.GetPolyLine(Convert.ToDouble(DestinationLatitude), Convert.ToDouble(DestinationLongitude), Convert.ToDouble(ShopLatitude), Convert.ToDouble(ShopLongitude));
                            string points = string.Empty;
                            foreach (var data in obj.routes)
                            {
                                points = data.overview_polyline.points;
                                foreach (var time in data.legs)
                                {
                                    Time = time.duration.text;
                                    timeTxt.Text = Time;
                                }
                            }

                            poliList = CommonLib.DecodePoly(points);
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {


                            double dic = Calc(Convert.ToDouble(DestinationLatitude), Convert.ToDouble(DestinationLongitude), Convert.ToDouble(ShopLatitude), Convert.ToDouble(ShopLongitude));
                            if (poliList != null)
                            {
                                double destination_Latitude = 0;
                                double destination_Longitude = 0;
                                customMap.RouteCoordinates.Clear();
                                foreach (var data in poliList)
                                {
                                    customMap.RouteCoordinates.Add(new Position(data.Latitude, data.Longitude));
                                    destination_Latitude = data.Latitude;
                                    destination_Longitude = data.Longitude;
                                }


                               
                                var destination_pin = new CustomPin
                                {
                                    Pin = new Pin
                                    {
                                        Type = PinType.Place,
                                        Position = new Position(Convert.ToDouble(DestinationLatitude), Convert.ToDouble(DestinationLongitude)),
                                        Label = "Customer",
                                    },
                                    Id = "1",
                                };

                                var shop_pin = new CustomPin
                                {
                                    Pin = new Pin
                                    {
                                        Type = PinType.Place,
                                        Position = new Position(Convert.ToDouble(ShopLatitude), Convert.ToDouble(ShopLongitude)),
                                        Label = ShopName,
                                    },
                                    Id = "Shop",
                                };
                                customMap.CustomPins = new List<CustomPin> {destination_pin, shop_pin };
                                if (Device.OS == TargetPlatform.iOS)
                                {
                                 
                                    customMap.Pins.Add(destination_pin.Pin);
                                    customMap.Pins.Add(shop_pin.Pin);
                                }

                                customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Convert.ToDouble(DestinationLatitude), Convert.ToDouble(DestinationLongitude)), Xamarin.Forms.Maps.Distance.FromKilometers(dic)));

                            }
                        }
                        catch (Exception ex)
                        {

                        }


                    }
                    catch (Exception ex)
                    {
                    }

                    //




                    Loader.CloseAllPopup();

                }
                catch (Exception)
                {
                    Loader.CloseAllPopup();
                }
                //timer
                Device.StartTimer(TimeSpan.FromSeconds(10), () =>
                {

                    getPolyLine1();
                    return true;
                });
            }


        }
        void back_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }
        public static double Calc(double Lat1, double Long1, double Lat2, double Long2)
        {
            try
            {

                double dDistance = Double.MinValue;
                double dLat1InRad = Lat1 * (Math.PI / 180.0);
                double dLong1InRad = Long1 * (Math.PI / 180.0);
                double dLat2InRad = Lat2 * (Math.PI / 180.0);
                double dLong2InRad = Long2 * (Math.PI / 180.0);

                double dLongitude = dLong2InRad - dLong1InRad;
                double dLatitude = dLat2InRad - dLat1InRad;


                double a = Math.Pow(Math.Sin(dLatitude / 2.0), 2.0) +
                           Math.Cos(dLat1InRad) * Math.Cos(dLat2InRad) *
                           Math.Pow(Math.Sin(dLongitude / 2.0), 2.0);


                double c = 2.0 * Math.Asin(Math.Sqrt(a));


                const Double kEarthRadiusKms = 6376.5;
                dDistance = kEarthRadiusKms * c;

                return dDistance;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async void getPolyLine1()
        {

            if (!string.IsNullOrEmpty(PickUpLatitude) && !string.IsNullOrEmpty(PickUpLongitude))
            {
                try
                {


                    try
                    {


                        try
                        {
                            RootObjectPolyLine obj = new RootObjectPolyLine();
                            obj = await CommonLib.GetPolyLine(Convert.ToDouble(PickUpLatitude), Convert.ToDouble(PickUpLongitude), Convert.ToDouble(DestinationLatitude), Convert.ToDouble(DestinationLongitude));
                            string points = string.Empty;
                            foreach (var data in obj.routes)
                            {
                                points = data.overview_polyline.points;
                                foreach (var time in data.legs)
                                {
                                    Time = time.duration.text;
                                    timeTxt.Text = Time;
                                }
                            }

                            poliList = CommonLib.DecodePoly(points);
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {


                            double dic = Calc(Convert.ToDouble(PickUpLatitude), Convert.ToDouble(PickUpLongitude), Convert.ToDouble(DestinationLatitude), Convert.ToDouble(DestinationLongitude));
                            if (poliList != null)
                            {
                                double destination_Latitude = 0;
                                double destination_Longitude = 0;
                                foreach (var data in poliList)
                                {
                                    customMap.RouteCoordinates.Add(new Position(data.Latitude, data.Longitude));
                                    destination_Latitude = data.Latitude;
                                    destination_Longitude = data.Longitude;
                                }


                                var start_pin = new CustomPin
                                {
                                    Pin = new Pin
                                    {
                                        Type = PinType.Place,
                                        Position = new Position(Convert.ToDouble(PickUpLatitude), Convert.ToDouble(PickUpLongitude)),
                                        //  Position = new Position(30.706459, 76.708265),
                                        Label = "Driver",
                                        // Address = storeAddress
                                    },
                                    Id = "Xamarin",
                                    startPin = true

                                };
                                var destination_pin = new CustomPin
                                {
                                    Pin = new Pin
                                    {
                                        Type = PinType.Place,
                                        Position = new Position(Convert.ToDouble(DestinationLatitude), Convert.ToDouble(DestinationLongitude)),
                                        // Position = new Position(30.7024, 76.8215),
                                        Label = "Customer",
                                        // Address = customerAddress
                                    },
                                    Id = "1",
                                };

                                //var shop_pin = new CustomPin
                                //{
                                //    Pin = new Pin
                                //    {
                                //        Type = PinType.Place,
                                //        Position = new Position(Convert.ToDouble(ShopLatitude), Convert.ToDouble(ShopLongitude)),
                                //        Label = ShopName,
                                //    },
                                //    Id = "Shop",
                                //};



                                customMap.CustomPins = new List<CustomPin> { start_pin, destination_pin };
                                if (Device.OS == TargetPlatform.iOS)
                                {
                                    customMap.Pins.Add(start_pin.Pin);
                                    customMap.Pins.Add(destination_pin.Pin);
                                }

                                customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Convert.ToDouble(PickUpLatitude), Convert.ToDouble(PickUpLongitude)), Xamarin.Forms.Maps.Distance.FromKilometers(dic)));

                            }
                        }
                        catch (Exception ex)
                        {

                        }


                    }
                    catch (Exception ex)
                    {
                    }

                    //






                }
                catch (Exception)
                {

                }
            }
            else
            {

                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync(10000);
              

                try
                {

                    try
                    {


                        try
                        {
                            RootObjectPolyLine obj = new RootObjectPolyLine();
                            obj = await CommonLib.GetPolyLine(Convert.ToDouble(position.Latitude), Convert.ToDouble(position.Longitude), Convert.ToDouble(ShopLatitude), Convert.ToDouble(ShopLongitude));
                            string points = string.Empty;
                            foreach (var data in obj.routes)
                            {
                                points = data.overview_polyline.points;
                                foreach (var time in data.legs)
                                {
                                    Time = time.duration.text;
                                    timeTxt.Text = Time;
                                }
                            }

                            poliList = CommonLib.DecodePoly(points);
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {


                            double dic = Calc(Convert.ToDouble(position.Latitude), Convert.ToDouble(position.Longitude), Convert.ToDouble(ShopLatitude), Convert.ToDouble(ShopLongitude));
                            if (poliList != null)
                            {
                                double destination_Latitude = 0;
                                double destination_Longitude = 0;
                                foreach (var data in poliList)
                                {
                                    customMap.RouteCoordinates.Add(new Position(data.Latitude, data.Longitude));
                                    destination_Latitude = data.Latitude;
                                    destination_Longitude = data.Longitude;
                                }


                               
                                var destination_pin = new CustomPin
                                {
                                    Pin = new Pin
                                    {
                                        Type = PinType.Place,
                                        Position = new Position(Convert.ToDouble(position.Latitude), Convert.ToDouble(position.Longitude)),
                                        Label = "Customer",
                                    },
                                    Id = "1",
                                };

                                var shop_pin = new CustomPin
                                {
                                    Pin = new Pin
                                    {
                                        Type = PinType.Place,
                                        Position = new Position(Convert.ToDouble(ShopLatitude), Convert.ToDouble(ShopLongitude)),
                                        Label = ShopName,
                                    },
                                    Id = "Shop",
                                };



                                customMap.CustomPins = new List<CustomPin> {destination_pin, shop_pin };
                                if (Device.OS == TargetPlatform.iOS)
                                {
                                    customMap.Pins.Add(destination_pin.Pin);
                                    customMap.Pins.Add(shop_pin.Pin);
                                }

                                customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Convert.ToDouble(position.Latitude), Convert.ToDouble(position.Longitude)), Xamarin.Forms.Maps.Distance.FromKilometers(dic)));

                            }
                        }
                        catch (Exception ex)
                        {

                        }


                    }
                    catch (Exception ex)
                    {
                    }

                    //






                }
                catch (Exception)
                {

                }
            }
        }

        public void DriverDetails()
        {
            if (!string.IsNullOrEmpty(DriverName)&& DriverName!=" ")
            {
                driverLyt.IsVisible = true;
                userImage.Source = CommonLib.img_MainUrl+DriverImage;
                userName.Text = DriverName;
            }
            else
            {
                driverLyt.IsVisible = false;
            }
        }

        private void Call_Tapped(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(DriverPhone))
            Device.OpenUri(new Uri(String.Format("tel:{0}", DriverPhone)));
        }

        private async void DeliverLbl_Clicked(object sender, EventArgs e)
        {
            var ans = await App.Current.MainPage.DisplayAlert("", AppResources.haveyourecivedorder, AppResources.yes, AppResources.no);
            if (ans)
            {
                try
                {


                    if (!CommonLib.checkconnection())
                    {

                        await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(AppResources._connection));
                        await Task.Delay(1000);
                        ShowMessage.CloseAllPopup();
                        return;
                    }


                    await App.Current.MainPage.Navigation.PushPopupAsync(new Loader());

                    LoggedInUser objUser = App.Database.GetLoggedInUser();

                    string postData = "order_id=" + CustomerOrderInfo.id + "&user_id=" + objUser.userId;
                    var result = await CommonLib.SellerOrderAcceptReject(CommonLib.ws_MainUrl + "customerOrderComplete?" + postData);
                    if (result.status == 1)
                    {
                        Loader.CloseAllPopup();

                        if (App.Lng == "ar-AE")
                        {
                            await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(result.msg_ar));

                        }
                        else
                        {
                            await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(result.msg_en));

                        }
                        await Task.Delay(1000);
                        ShowMessage.CloseAllPopup();
                        App.Current.MainPage = new NavigationPage(new MainPage());
                    }
                    else
                    {
                        Loader.CloseAllPopup();

                        if (App.Lng == "ar-AE")
                        {
                            await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(result.msg_ar));

                        }
                        else
                        {
                            await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(result.msg_en));

                        }
                        await Task.Delay(1000);
                        ShowMessage.CloseAllPopup();
                    }

                }
                catch (Exception ex)
                {
                    Loader.CloseAllPopup();
                }
            }
        }
    }
}