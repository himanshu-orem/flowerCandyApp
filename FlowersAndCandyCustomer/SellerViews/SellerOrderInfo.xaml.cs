using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.Resources;
using FlowersAndCandyCustomer.ViewModels;
using FlowersAndCandyCustomer.Views;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace FlowersAndCandyCustomer.SellerViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SellerOrderInfo : ContentPage
    {
        public static string CustomerPhone = "";

        public static string id = "";
        public static double _ptotalPrice = 0;
        private ObservableCollection<CartViewModel> _expandedGroups;
        public SellerOrderInfo()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
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



                    //
                    if (result.data.order.Order.status == "0")
                    {
                        acptBtns.IsVisible = true;
                        statusBtns.IsVisible = false;
                        completeLbl.IsVisible = false;
                        deliverLbl.IsVisible = false;
                    }
                    if (result.data.order.Order.status != "0")
                    {
                        acptBtns.IsVisible = false;
                        statusBtns.IsVisible = true;
                        completeLbl.IsVisible = false;
                        deliverLbl.IsVisible = false;
                    }
                    //
                    string sts = "";
                    if (result.data.order.Order.status == "2")
                    {
                        sts = AppResources.Orderrejectedby;
                        completeLbl.IsVisible = false;
                        deliverLbl.IsVisible = false;
                    }
                    if (result.data.order.Order.status == "3")
                    {
                        sts = AppResources.Orderassignedtodriver;
                        completeLbl.IsVisible = false;
                        deliverLbl.IsVisible = false;
                    }
                    if (result.data.order.Order.status == "-1" || result.data.order.Order.status == "-2" || result.data.order.Order.status == "-3")
                    {
                        sts = AppResources.Ordercancel;
                        completeLbl.IsVisible = false;
                        deliverLbl.IsVisible = false;
                    }
                    if (result.data.order.Order.status == "4")
                    {
                        sts = AppResources.Orderacceptedbydriver;
                        completeLbl.IsVisible = false;
                        deliverLbl.IsVisible = false;//true;
                    }
                    if (result.data.order.Order.status == "5")
                    {
                        sts = AppResources.Orderrejectedbydriver;
                        completeLbl.IsVisible = false;
                        deliverLbl.IsVisible = false;
                    }
                    if (result.data.order.Order.status == "6")
                    {
                        sts = AppResources.delivered;
                        completeLbl.IsVisible = false;
                        deliverLbl.IsVisible = false;
                    }
                    if (result.data.order.Order.status == "7")
                    {
                        sts = AppResources.Ordercompleted1;
                        completeLbl.IsVisible = false;
                        deliverLbl.IsVisible = false;
                    }
                    if (result.data.order.Order.status == "8")
                    {
                        sts = AppResources.Orderacceptedbyseller;
                        completeLbl.IsVisible = true;
                        deliverLbl.IsVisible = false;
                    }

                    if (result.data.order.Order.status == "9")
                    {
                        sts = AppResources.delivered;
                        if (result.data.order.Order.self_pick == "1")
                        {
                            completeLbl.IsVisible = true;
                        }
                        else
                        {
                            completeLbl.IsVisible = false;
                        }
                        deliverLbl.IsVisible = false;
                    }

                    statusLbl.Text = sts;



                    Loader.CloseAllPopup();

                    CustomerPhone = result.data.order.Customer.phone;
                    customerName.Text = result.data.order.Customer.first_name + " " + result.data.order.Customer.last_name;
                    customerImage.Source = CommonLib.img_MainUrl + result.data.order.Customer.image;
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

        private async void AcceptLbl_Clicked(object sender, EventArgs e)
        {
            try
            {
                int count = 1;

                if (!CommonLib.checkconnection())
                {

                    await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(AppResources._connection));
                    await Task.Delay(1000);
                    ShowMessage.CloseAllPopup();
                    return;
                }


                await App.Current.MainPage.Navigation.PushPopupAsync(new Loader());

                LoggedInUser objUser = App.Database.GetLoggedInUser();

            Outer:

                string postData = "order_id=" + id + "&user_id=" + objUser.userId + "&status=1" + "&attempt=" + count;
                var result = await CommonLib.SellerOrderAcceptReject(CommonLib.ws_MainUrl + "sellerOrderAction?" + postData);
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
                    load();

                }
                else if (result.status == 2)
                {
                    count++;
                    goto Outer;
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
                    //App.Current.MainPage = new NavigationPage(new HomeMasterPage());
                    await Navigation.PopAsync();
                }

            }
            catch (Exception ex)
            {
                Loader.CloseAllPopup();
            }
        }

        private async void RejectLbl_Clicked(object sender, EventArgs e)
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

                string postData = "order_id=" + id + "&user_id=" + objUser.userId + "&status=2" + "&attempt=1";
                var result = await CommonLib.SellerOrderAcceptReject(CommonLib.ws_MainUrl + "sellerOrderAction?" + postData);
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

        private async void CompleteLbl_Clicked(object sender, EventArgs e)
        {
            var ans = await App.Current.MainPage.DisplayAlert("", AppResources.haveyoudelivedrorder, AppResources.yes, AppResources.no);
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

                    string postData = "order_id=" + id + "&user_id=" + objUser.userId;
                    var result = await CommonLib.SellerOrderAcceptReject(CommonLib.ws_MainUrl + "sellerOrderComplete?" + postData);
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
        private void Call_Tapped(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CustomerPhone))
                Device.OpenUri(new Uri(String.Format("tel:{0}", CustomerPhone)));
        }
        private async void deliverLbl_Clicked(object sender, EventArgs e)
        {
            var ans = await App.Current.MainPage.DisplayAlert("", AppResources.haveyoudelivedrorder, AppResources.yes, AppResources.no);
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

                    string postData = "order_id=" + id + "&user_id=" + objUser.userId;
                    var result = await CommonLib.SellerOrderAcceptReject(CommonLib.ws_MainUrl + "sellerOrderDeliver?" + postData);
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
}