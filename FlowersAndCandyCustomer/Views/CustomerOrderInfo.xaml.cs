using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.Resources;
using FlowersAndCandyCustomer.ViewModels;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlowersAndCandyCustomer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerOrderInfo : ContentPage
    {
        public static string id = "";
        public static double _ptotalPrice = 0;
        private ObservableCollection<CartViewModel> _expandedGroups;
        public CustomerOrderInfo()
        {

            

           InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);


        }
        protected override void OnAppearing()
        {
            MapPage.PickUpLatitude = "";
            MapPage.PickUpLongitude = "";

            MapPage.DestinationLatitude = "";
            MapPage.DestinationLongitude = "";

            MapPage.ShopLatitude = "";
            MapPage.ShopLongitude = "";
            MapPage.ShopName = "";
            MapPage.Time = "";
            MapPage.DriverName = "";

            load();
        }
        void back_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }
        public async void load()
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

                string postData = "order_id=" + id;
                var result = await CommonLib.GetCustomerOrderInfo(CommonLib.ws_MainUrl + "getUserOrderInfo?" + postData);
                if (result.status == 1)
                {
                    //
                    DateTime order = Convert.ToDateTime(result.data.order.Order.created).AddMinutes(5);
                    DateTime server = Convert.ToDateTime(result.data.server_time);

                    string sts = "";
                    if (result.data.order.Order.status == "0")
                    {
                        sts = AppResources.Ordercreated;
                        img1.Source = "checked.png";
                        img2.Source = "twoImg.png";
                        img3.Source = "threeImg.png";
                        img4.Source = "fourImg.png";
                        if (order > server)
                        {
                            if (result.data.order.Order.payment_method == "1")
                                cancelLbl.IsVisible = true;
                        }
                        else
                        {
                            cancelLbl.IsVisible = false;
                        }

                    }
                    if (result.data.order.Order.status == "1")
                    {
                        sts = AppResources.Orderacceptedbyseller;
                        img1.Source = "checked.png";
                        img2.Source = "checked.png";
                        img3.Source = "threeImg.png";
                        img4.Source = "fourImg.png";
                        if (order > server)
                        {
                            if (result.data.order.Order.payment_method == "1")
                                cancelLbl.IsVisible = true;
                        }
                        else
                        {
                            cancelLbl.IsVisible = false;
                        }

                    }
                    if (result.data.order.Order.status == "2")
                    {
                        sts = AppResources.Orderrejectedby;
                        img1.Source = "oneImg.png";
                        img2.Source = "twoImg.png";
                        img3.Source = "threeImg.png";
                        img4.Source = "fourImg.png";
                        cancelLbl.IsVisible = false;

                    }
                    if (result.data.order.Order.status == "3")
                    {
                        sts = AppResources.Orderassignedtodriver;
                        img1.Source = "checked.png";
                        img2.Source = "checked.png";
                        img3.Source = "threeImg.png";
                        img4.Source = "fourImg.png";
                        if (order > server)
                        {
                            if (result.data.order.Order.payment_method == "1")
                                cancelLbl.IsVisible = true;
                        }
                        else
                        {
                            cancelLbl.IsVisible = false;
                        }

                    }
                    if (result.data.order.Order.status == "-1" || result.data.order.Order.status == "-2" || result.data.order.Order.status == "-3" || result.data.order.Order.status == "-4")
                    {
                        sts = AppResources.Ordercancel;
                        img1.Source = "oneImg.png";
                        img2.Source = "twoImg.png";
                        img3.Source = "threeImg.png";
                        img4.Source = "fourImg.png";
                        cancelLbl.IsVisible = false;


                    }
                    if (result.data.order.Order.status == "4")
                    {
                        sts = AppResources.Orderacceptedbydriver;
                        img1.Source = "checked.png";
                        img2.Source = "checked.png";
                        img3.Source = "threeImg.png";
                        img4.Source = "fourImg.png";
                        if (order > server)
                        {
                            if (result.data.order.Order.payment_method == "1")
                                cancelLbl.IsVisible = true;
                        }
                        else
                        {
                            cancelLbl.IsVisible = false;
                        }

                    }
                    if (result.data.order.Order.status == "5")
                    {
                        sts = AppResources.Orderrejectedbydriver;
                        img1.Source = "oneImg.png";
                        img2.Source = "twoImg.png";
                        img3.Source = "threeImg.png";
                        img4.Source = "fourImg.png";
                        cancelLbl.IsVisible = false;

                    }
                    if (result.data.order.Order.status == "6")
                    {
                        sts = AppResources.Driveronway;
                        trackLbl.IsVisible = true;
                        img1.Source = "checked.png";
                        img2.Source = "checked.png";
                        img3.Source = "checked.png";
                        img4.Source = "fourImg.png";
                        cancelLbl.IsVisible = false;

                    }
                    if (result.data.order.Order.status == "9")
                    {
                        sts = AppResources.delivered;
                        img1.Source = "checked.png";
                        img2.Source = "checked.png";
                        img3.Source = "checked.png";
                        img4.Source = "lastChecked.png";
                        cancelLbl.IsVisible = false;
                        trackLbl.IsVisible = false;
                    }


                    if (result.data.order.Order.status == "7")
                    {
                        sts = AppResources.Ordercompleted1;
                        img1.Source = "checked.png";
                        img2.Source = "checked.png";
                        img3.Source = "checked.png";
                        img4.Source = "lastChecked.png";
                        cancelLbl.IsVisible = false;
                    }

                    if (result.data.order.Order.status == "8")
                    {
                        sts = AppResources.Orderacceptedbyseller;
                        trackLbl.IsVisible = true;
                        img1.Source = "checked.png";
                        img2.Source = "checked.png";
                        img3.Source = "threeImg.png";
                        img4.Source = "fourImg.png";
                        cancelLbl.IsVisible = false;

                    }



                    statusLbl.Text = sts;
                    MapPage.PickUpLatitude = result.data.order.Driver.lat;
                    MapPage.PickUpLongitude = result.data.order.Driver.lng;

                    MapPage.DriverImage = result.data.order.Driver.image;
                    MapPage.DriverPhone = result.data.order.Driver.phone;
                    MapPage.DriverName = result.data.order.Driver.first_name+" "+ result.data.order.Driver.last_name;

                    MapPage.DestinationLatitude = result.data.order.OrderAddress.lat;
                    MapPage.DestinationLongitude = result.data.order.OrderAddress.lng;

                    MapPage.ShopLatitude = result.data.order.Shop.latitude;
                    MapPage.ShopLongitude = result.data.order.Shop.longitude;
                    MapPage.ShopName = result.data.order.Shop.company_name;

                    Device.StartTimer(TimeSpan.FromSeconds(10), () =>
                    {

                        Update();
                        return true;
                    });


                    _ptotalPrice = 0;
                    _expandedGroups = new ObservableCollection<CartViewModel>();

                    var _products = App.Database.GetAllProduct();


                    foreach (var group in result.data.order.OrderProduct)
                    {
                        _ptotalPrice += Convert.ToDouble(group.price) * Convert.ToDouble(group.quantity);

                        string img = "";
                        foreach (var food in group.Addon)
                        {
                            if (food.AddonMedia == null)
                            { img = "product_Placeholder.png"; }
                            else
                            {
                                img = string.IsNullOrEmpty(food.AddonMedia.media) ? "product_Placeholder.png" : CommonLib.img_MainUrl + food.AddonMedia.media;
                            }
                        }

                        CartViewModel newGroup = new CartViewModel(group.name, group.quantity, group.price, Convert.ToString(group.id), group.product_id, "", img, "");


                        foreach (var food in group.Addon)
                        {
                            _ptotalPrice += Convert.ToDouble(food.price) * Convert.ToDouble(food.quantity);
                            newGroup.Add(new CartProduct { Note = (!string.IsNullOrEmpty(food.note)) ? AppResources.noteDetails + ":- " + food.note : "", Name = food.name, AddonCategory = "", AddonId = food.addon_id, AddonPrice = food.price, AddonQty = food.quantity, PSQLId = "", ProductId = food.product_id });



                        }

                        _expandedGroups.Add(newGroup);
                    }

                    GroupedView.ItemsSource = _expandedGroups;
                    if (_expandedGroups.Count == 0)
                    {
                        emptyCartLbl.IsVisible = true;
                        totalLyt.IsVisible = false;
                    }
                    else
                    {
                        emptyCartLbl.IsVisible = false;
                        totalLyt.IsVisible = true;
                        totalPrice.Text = "Riyal " + _ptotalPrice;
                    }





                    Loader.CloseAllPopup();



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
        public async void Update()
        {
            try
            {
                LoggedInUser objUser = App.Database.GetLoggedInUser();
                string postData = "order_id=" + id;
                var result = await CommonLib.GetCustomerOrderInfo1(CommonLib.ws_MainUrl + "getUserOrderInfo?" + postData);
                if (result.status == 1)
                {
                    //
                    DateTime order = Convert.ToDateTime(result.data.order.Order.created).AddMinutes(5);
                    DateTime server = Convert.ToDateTime(result.data.server_time);
                    string sts = "";
                    if (result.data.order.Order.status == "0")
                    {
                        sts = AppResources.Ordercreated;
                        img1.Source = "checked.png";
                        img2.Source = "twoImg.png";
                        img3.Source = "threeImg.png";
                        img4.Source = "fourImg.png";
                        if (order > server)
                        {
                            if (result.data.order.Order.payment_method == "1")
                                cancelLbl.IsVisible = true;
                        }
                        else
                        {
                            cancelLbl.IsVisible = false;
                        }
                    }
                    if (result.data.order.Order.status == "1")
                    {
                        sts = AppResources.Orderacceptedbyseller;
                        img1.Source = "checked.png";
                        img2.Source = "checked.png";
                        img3.Source = "threeImg.png";
                        img4.Source = "fourImg.png";
                        if (order > server)
                        {
                            if (result.data.order.Order.payment_method == "1")
                                cancelLbl.IsVisible = true;
                        }
                        else
                        {
                            cancelLbl.IsVisible = false;
                        }

                    }
                    if (result.data.order.Order.status == "2")
                    {
                        sts = AppResources.Orderrejectedby;
                        img1.Source = "oneImg.png";
                        img2.Source = "twoImg.png";
                        img3.Source = "threeImg.png";
                        img4.Source = "fourImg.png";
                        cancelLbl.IsVisible = false;

                    }
                    if (result.data.order.Order.status == "3")
                    {
                        sts = AppResources.Orderassignedtodriver;
                        img1.Source = "checked.png";
                        img2.Source = "checked.png";
                        img3.Source = "threeImg.png";
                        img4.Source = "fourImg.png";
                        if (order > server)
                        {
                            if (result.data.order.Order.payment_method == "1")
                                cancelLbl.IsVisible = true;
                        }
                        else
                        {
                            cancelLbl.IsVisible = false;
                        }
                    }
                    if (result.data.order.Order.status == "-1" || result.data.order.Order.status == "-2" || result.data.order.Order.status == "-3" || result.data.order.Order.status == "-4")
                    {
                        sts = AppResources.Ordercancel;
                        img1.Source = "oneImg.png";
                        img2.Source = "twoImg.png";
                        img3.Source = "threeImg.png";
                        img4.Source = "fourImg.png";
                        cancelLbl.IsVisible = false;

                    }
                    if (result.data.order.Order.status == "4")
                    {
                        sts = AppResources.Orderacceptedbydriver;
                        img1.Source = "checked.png";
                        img2.Source = "checked.png";
                        img3.Source = "threeImg.png";
                        img4.Source = "fourImg.png";
                        if (order > server)
                        {
                            if (result.data.order.Order.payment_method == "1")
                                cancelLbl.IsVisible = true;
                        }
                        else
                        {
                            cancelLbl.IsVisible = false;
                        }
                    }
                    if (result.data.order.Order.status == "5")
                    {
                        sts = AppResources.Orderrejectedbydriver;
                        img1.Source = "oneImg.png";
                        img2.Source = "twoImg.png";
                        img3.Source = "threeImg.png";
                        img4.Source = "fourImg.png";
                    }
                    if (result.data.order.Order.status == "6")
                    {
                        sts = AppResources.Driveronway;
                        trackLbl.IsVisible = true;
                        img1.Source = "checked.png";
                        img2.Source = "checked.png";
                        img3.Source = "checked.png";
                        img4.Source = "fourImg.png";
                        cancelLbl.IsVisible = false;
                    }
                    if (result.data.order.Order.status == "9")
                    {
                        sts = AppResources.delivered;
                        img1.Source = "checked.png";
                        img2.Source = "checked.png";
                        img3.Source = "checked.png";
                        img4.Source = "lastChecked.png";
                        cancelLbl.IsVisible = false;
                        trackLbl.IsVisible = false;
                    }
                    if (result.data.order.Order.status == "7")
                    {
                        sts = AppResources.Ordercompleted1;
                        img1.Source = "checked.png";
                        img2.Source = "checked.png";
                        img3.Source = "checked.png";
                        img4.Source = "lastChecked.png";
                        cancelLbl.IsVisible = false;
                    }
                    if (result.data.order.Order.status == "8")
                    {
                        sts = AppResources.Orderacceptedbyseller;
                        trackLbl.IsVisible = true;
                        img1.Source = "checked.png";
                        img2.Source = "checked.png";
                        img3.Source = "threeImg.png";
                        img4.Source = "fourImg.png";
                        cancelLbl.IsVisible = false;

                    }
                    statusLbl.Text = sts;
                    MapPage.PickUpLatitude = result.data.order.Driver.lat;
                    MapPage.PickUpLongitude = result.data.order.Driver.lng;

                    MapPage.DriverImage = result.data.order.Driver.image;
                    MapPage.DriverPhone = result.data.order.Driver.phone;
                    MapPage.DriverName = result.data.order.Driver.first_name + " " + result.data.order.Driver.last_name;


                    MapPage.DestinationLatitude = result.data.order.OrderAddress.lat;
                    MapPage.DestinationLongitude = result.data.order.OrderAddress.lng;

                    MapPage.ShopLatitude = result.data.order.Shop.latitude;
                    MapPage.ShopLongitude = result.data.order.Shop.longitude;
                    MapPage.ShopName = result.data.order.Shop.company_name;
                }


            }
            catch (Exception ex)
            {
            }
        }


        private void UpdateListContent()
        {
            _ptotalPrice = 0;
            _expandedGroups = new ObservableCollection<CartViewModel>();

            var _products = App.Database.GetAllProduct();


            foreach (var group in _products)
            {
                _ptotalPrice += Convert.ToDouble(group.productPrice);
                CartViewModel newGroup = new CartViewModel(group.productName, group.productQty, group.productPrice, Convert.ToString(group.ID), group.productId, group.productCategory, group.productImage, Convert.ToString(group.ID));

                var _addons = App.Database.GetAllProductAddon(Convert.ToString(group.ID));

                foreach (var food in _addons)
                {
                    _ptotalPrice += Convert.ToDouble(food.AddonPrice);
                    newGroup.Add(new CartProduct { Name = food.AddonName, AddonCategory = food.AddonCategory, AddonId = food.AddonId, AddonPrice = food.AddonPrice, AddonQty = food.AddonQty, PSQLId = food.productId, ProductId = food.productId });
                }

                _expandedGroups.Add(newGroup);
            }

            GroupedView.ItemsSource = _expandedGroups;
            if (_expandedGroups.Count == 0)
            {
                emptyCartLbl.IsVisible = true;
                totalLyt.IsVisible = false;
            }
            else
            {
                emptyCartLbl.IsVisible = false;
                totalLyt.IsVisible = true;
                totalPrice.Text = "Riyal " + _ptotalPrice;
            }

        }

        private void GroupedView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            GroupedView.SelectedItem = null;
        }

        private async void TrackLbl_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new MapPage());
        }

        private async void CancelLbl_Clicked(object sender, EventArgs e)
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

                string postData = "order_id=" + id;
                var result = await CommonLib.GetCustomerOrderInfo(CommonLib.ws_MainUrl + "customerOrderCancel?" + postData);
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
                    await Navigation.PopAsync();
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