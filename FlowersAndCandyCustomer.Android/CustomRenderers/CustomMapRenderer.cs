using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FlowersAndCandyCustomer.CustomControls;
using FlowersAndCandyCustomer.Droid.CustomRenderers;
using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Views;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace FlowersAndCandyCustomer.Droid.CustomRenderers
{
    






    public class CustomMapRenderer : MapRenderer, IOnMapReadyCallback
    {
        public static Marker MoveAble_Marker;
        public static Android.Gms.Maps.Model.Polyline MoveAble_Polyline;
        //List<Position> routeCoordinates;
        // public List<CustomPin> customPins;
        bool isDrawn;

        public CustomMapRenderer(Context context) : base(context)
        {

        }
        public void update()
        {
            try
            {

                NativeMap.Clear();


                var polylineOptions = new PolylineOptions();
                polylineOptions.InvokeColor(0x66FF0000);
                foreach (var pin in ((CustomMap)Element).CustomPins)
                {
                    BitmapDescriptor icon = BitmapDescriptorFactory.FromResource(Resource.Drawable.driver);
                    if (pin.Id == "1")
                    {
                        icon = BitmapDescriptorFactory.FromResource(Resource.Drawable.customer);
                    }
                    else if (pin.Id == "Xamarin")
                    {
                        icon = BitmapDescriptorFactory.FromResource(Resource.Drawable.driver);
                    }
                    else
                    {
                        icon = BitmapDescriptorFactory.FromResource(Resource.Drawable.shopPin);
                    }


                    var marker = new MarkerOptions();
                    marker.SetPosition(new LatLng(pin.Pin.Position.Latitude, pin.Pin.Position.Longitude));
                    marker.SetTitle(pin.Pin.Label);
                    marker.SetSnippet(pin.Pin.Address);
                    marker.SetIcon(icon);
                    if (pin.Id != "1")
                    {

                        MoveAble_Marker = NativeMap.AddMarker(marker);
                    }
                    else
                    {
                        NativeMap.AddMarker(marker);
                    }

                }
                 
                //foreach (var position in ((CustomMap)Element).RouteCoordinates)
                //{
                //    polylineOptions.Add(new LatLng(position.Latitude, position.Longitude));
                //}
                //NativeMap.AddPolyline(polylineOptions);






            }
            catch { }
        }
        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            try
            {
                base.OnElementChanged(e);

                if (e.OldElement != null)
                {
                    NativeMap.InfoWindowClick -= OnInfoWindowClick;
                }


            }
            catch { }
        }
        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);
            map.MapClick += NativeMap_MapClick;
            
        }
        private void NativeMap_MapClick(object sender, GoogleMap.MapClickEventArgs e)
        {
            if (DeliveryAddressPopUp.DeliveryAddresscheck == 0)
            {
                ShopListPage.Lat1 = e.Point.Latitude.ToString();
                ShopListPage.Lng1 = e.Point.Longitude.ToString();

                MessagingCenter.Send(
                         new LoggedInUser { }, "test");
            }
            if (DeliveryAddressPopUp.DeliveryAddresscheck == 1)
            {
                DeliveryAddressPopUp.Lat = e.Point.Latitude.ToString();
                DeliveryAddressPopUp.Lng = e.Point.Longitude.ToString();

                MessagingCenter.Send(
                         new LoggedInUser { }, "test");
            }
            try
            {
                NativeMap.Clear();

            BitmapDescriptor icon = BitmapDescriptorFactory.FromResource(Resource.Drawable.customer);
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(e.Point.Latitude, e.Point.Longitude));
            marker.SetIcon(icon);
            
                MoveAble_Marker = NativeMap.AddMarker(marker);
                isDrawn = true;
            }
            catch (Exception)
            {
            }

        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                base.OnElementPropertyChanged(sender, e);

                if (e.PropertyName.Equals("VisibleRegion") && !isDrawn)
                {
                    NativeMap.Clear();
                    NativeMap.InfoWindowClick += OnInfoWindowClick;

                    var polylineOptions = new PolylineOptions();
                    polylineOptions.InvokeColor(0x66FF0000);
                    foreach (var pin in ((CustomMap)Element).CustomPins)
                    {
                        BitmapDescriptor icon = BitmapDescriptorFactory.FromResource(Resource.Drawable.driver);
                        if (pin.Id == "1")
                        {
                            icon = BitmapDescriptorFactory.FromResource(Resource.Drawable.customer);
                        }
                        else if(pin.Id== "Xamarin")
                        {
                            icon = BitmapDescriptorFactory.FromResource(Resource.Drawable.driver);
                        }
                        else 
                        {
                            icon = BitmapDescriptorFactory.FromResource(Resource.Drawable.shopPin);
                        }

                        var marker = new MarkerOptions();
                        marker.SetPosition(new LatLng(pin.Pin.Position.Latitude, pin.Pin.Position.Longitude));
                        marker.SetTitle(pin.Pin.Label);
                        marker.SetSnippet(pin.Pin.Address);
                        marker.SetIcon(icon);
                        if (pin.Id != "1")
                        {

                            MoveAble_Marker = NativeMap.AddMarker(marker);
                        }
                        else
                        {
                            NativeMap.AddMarker(marker);
                        }

                    }
                    //
                    //foreach (var position in ((CustomMap)Element).RouteCoordinates)
                    //{
                    //    polylineOptions.Add(new LatLng(position.Latitude, position.Longitude));
                    //}
                    //NativeMap.AddPolyline(polylineOptions);

                    isDrawn = true;

                    Device.StartTimer(TimeSpan.FromSeconds(10), () =>
                    {

                        update();

                        return true;
                    });

                }
            }
            catch { }

        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            try
            {
                base.OnLayout(changed, l, t, r, b);

                if (changed)
                {
                    isDrawn = false;
                }
            }
            catch { }
        }

        void OnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
            try
            {
                var customPin = GetCustomPin(e.Marker);
                if (customPin == null)
                {
                    throw new Exception("Custom pin not found");
                }

                if (!string.IsNullOrWhiteSpace(customPin.Url))
                {
                    var url = Android.Net.Uri.Parse(customPin.Url);
                    var intent = new Intent(Intent.ActionView, url);
                    intent.AddFlags(ActivityFlags.NewTask);
                    Android.App.Application.Context.StartActivity(intent);
                }
            }
            catch { }
        }



        public Android.Views.View GetInfoWindow(Marker marker)
        {
            return null;
        }

        CustomPin GetCustomPin(Marker annotation)
        {
            var position = new Position(annotation.Position.Latitude, annotation.Position.Longitude);
            foreach (var pin in ((CustomMap)Element).CustomPins)
            {
                if (pin.Pin.Position == position)
                {
                    return pin;
                }
            }
            return null;

        }


    }

}