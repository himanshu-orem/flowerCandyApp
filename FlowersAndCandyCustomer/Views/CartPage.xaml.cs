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

namespace FlowersAndCandyCustomer.Views
{
    public partial class CartPage : ContentPage
    {
        public static string deffaultAdd = "";
        public static double _ptotalPrice = 0;
        private ObservableCollection<CartViewModel> _expandedGroups;
        public CartPage()
        {
            InitializeComponent();
            //App.check = true;
            NavigationPage.SetHasBackButton(this, false);

            UpdateListContent();
            deffaultAdd = "";
            //  loadDefaultAddress();
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            deffaultAdd = "";
            //DeliveryAddress();
        }
        private void UpdateListContent()
        {
            _ptotalPrice = 0;
            _expandedGroups = new ObservableCollection<CartViewModel>();

            var _products = App.Database.GetAllProduct();


            foreach (var group in _products)
            {
                _ptotalPrice += Convert.ToDouble(group.productPrice)* Convert.ToDouble(group.productQty);
                CartViewModel newGroup = new CartViewModel( group.productName, group.productQty, group.productPrice, Convert.ToString(group.ID), group.productId, group.productCategory, group.productImage, Convert.ToString(group.ID));

                var _addons = App.Database.GetAllProductAddon(Convert.ToString(group.ID));

                foreach (var food in _addons)
                {
                    _ptotalPrice += Convert.ToDouble(food.AddonPrice)* Convert.ToDouble(food.AddonQty);
                    newGroup.Add(new CartProduct {  Name = food.AddonName, AddonCategory = food.AddonCategory, AddonId = food.AddonId, AddonPrice = food.AddonPrice, AddonQty = food.AddonQty, PSQLId = food.productId, ProductId = food.productId });
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


        private async void delete_Tapped(object sender, EventArgs e)
        {
            try
            {
                var getBtn = sender as Frame;
                var aa = getBtn.BindingContext;

                // var getProduct= App.Database.GetProduct(Convert.ToInt32(aa));

                var deleteProductAddon = App.Database.DeleteCartProductAddon(Convert.ToString(aa));

                var deleteProduct = App.Database.DeleteProduct(Convert.ToInt32(aa));

                UpdateListContent();

            }
            catch (Exception)
            {
            }



        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void addAddredd_Clicked(object sender, EventArgs e)
        {
            //  await App.Current.MainPage.Navigation.PushAsync(new AddressListPage());
            DeliveryAddressPopUp deliveryAddressPopUp = new DeliveryAddressPopUp();
            deliveryAddressPopUp.Disappearing += DeliveryAddressPopUp_Disappearing; 
            await App.Current.MainPage.Navigation.PushPopupAsync(deliveryAddressPopUp);
        }

        private  void DeliveryAddressPopUp_Disappearing(object sender, EventArgs e)
        {
            addressLbl.Text = DeliveryAddressPopUp.Txt;
        }

        private void GroupedView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            GroupedView.SelectedItem = null;
        }
        //public async void loadDefaultAddress()
        //{








        //    try
        //    {


        //        if (!CommonLib.checkconnection())
        //        {

        //            await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(AppResources._connection));
        //            await Task.Delay(1000);
        //            ShowMessage.CloseAllPopup();
        //            return;
        //        }


        //        await App.Current.MainPage.Navigation.PushPopupAsync(new Loader());

        //        LoggedInUser objUser = App.Database.GetLoggedInUser();

        //        string postData = "user_id=" + objUser.userId + "&id=0";
        //        var result = await CommonLib.GetCustomerAddressData(CommonLib.ws_MainUrl + "getAddressInfo?" + postData);
        //        if (result.status == 1)
        //        {
        //            deffaultAdd= result.data.UsersAddress.id;
        //            defaultAdd.Text = result.data.UsersAddress.full_address;

        //            Loader.CloseAllPopup();

        //        }
        //        else
        //        {
        //            Loader.CloseAllPopup();


        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Loader.CloseAllPopup();
        //    }





        //}

        private async void Next_Clicked(object sender, EventArgs e)
        {
            // emptyCartLbl cart
            var emptyCartLbl = App.Database.GetAllProduct();
            if (emptyCartLbl.Count == 0)
            {
                return;
            }
            //empty address
            if (string.IsNullOrEmpty(addressLbl.Text))
            {
                await DisplayAlert("F & C", AppResources.selectDeliveryAddress, "OK");
                return;
            }

            //string products = "";
            //string paymentMethod = "1";
            //try
            //{
            //    products = "[";
            //    var _products = App.Database.GetAllProduct();

            //    foreach (var group in _products)
            //    {
            //        products += "{";
            //        products += '"' + "id" + '"' + ":" + group.productId + ",";
            //        products += '"' + "quantity" + '"' + ":" + group.productQty + ",";
            //        products += '"' + "addon" + '"' + ":" + "[";

            //        var _addons = App.Database.GetAllProductAddon(Convert.ToString(group.ID));
            //        foreach (var food in _addons)
            //        {
            //            products += "{";
            //            products += '"' + "id" + '"' + ":" + food.AddonId + ",";
            //            products += '"' + "quantity" + '"' + ":" + food.AddonQty;
            //            products += "},";
            //        }
            //        //
            //        if (_addons.Count != 0)
            //            products = products.Substring(0, products.Length - 1);
            //        products += "]";


            //        products += "},";
            //    }
            //    //
            //    products = products.Substring(0, products.Length - 1);
            //    products += "]";


            //}
            //catch (Exception)
            //{
            //}




            //var ans = await App.Current.MainPage.DisplayAlert("", AppResources.createOrder, AppResources.yes, AppResources.no);
            //if (ans)
            //{

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

                var result = await CommonLib.GetSettingResult(CommonLib.ws_MainUrl + "setting");
                if (result.status == 1)
                {
                    Loader.CloseAllPopup();
                    CustomerPaymentPage.deliveryPrice = Convert.ToDouble(result.data.Setting.delivery_fee);
                    CustomerPaymentPage.deliveryPriceTemp = Convert.ToDouble(result.data.Setting.delivery_fee);
                    CustomerPaymentPage.dictancePrice = Convert.ToDouble(result.data.Setting.distance_charge);
                    CustomerPaymentPage.radius = Convert.ToInt32(result.data.Setting.delivery_fee_radius);
                    App.Current.MainPage.Navigation.PushAsync(new CustomerPaymentPage());


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

            //}
        }

        private void AddressPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selected = adsDelivery.SelectedItem as UsersAddress;
                deffaultAdd = selected.id;
                CustomerPaymentPage.customerAddress = selected.full_address;
            }
            catch (Exception)
            {
            }
        }
        public async void DeliveryAddress()
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
                string postData = "user_id=" + objUser.userId;
                var result = await CommonLib.GetCustomerAddress(CommonLib.ws_MainUrl + "getAllAddresses?" + postData);
                if (result.status == 1)
                {
                    List<UsersAddress> obj = new List<UsersAddress>();
                    foreach (var item in result.data)
                    {
                        obj.Add(new UsersAddress { img = item.UsersAddress.is_default == "1" ? "check_1.png" : "check_2.png", city = item.UsersAddress.city, country = item.UsersAddress.country, full_address = item.UsersAddress.full_address, full_name = item.UsersAddress.full_name, id = item.UsersAddress.id, is_default = item.UsersAddress.is_default, landmark = item.UsersAddress.landmark, lat = item.UsersAddress.lat, lng = item.UsersAddress.lng, state = item.UsersAddress.state, user_id = item.UsersAddress.user_id, zipcode = item.UsersAddress.zipcode });
                    }
                    adsDelivery.ItemsSource = obj;


                    Loader.CloseAllPopup();

                }
                else
                {
                    Loader.CloseAllPopup();


                }

            }
            catch (Exception ex)
            {
                Loader.CloseAllPopup();
            }
        }
    }
}
