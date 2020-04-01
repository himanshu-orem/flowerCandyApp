using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.Resources;
using FlowersAndCandyCustomer.Services;
using FlowersAndCandyCustomer.ViewModels;
using FlowersAndCandyCustomer.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlowersAndCandyCustomer.SellerViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
        private ObservableCollection<ExpendProductViewModel> _expandedGroups;

        public HomePage ()
		{
			InitializeComponent ();
            loadProduct();
            Title = AppResources.products;
           // SellerProductService.loaderCheck = true;
            //SellerProductViewModel model = new SellerProductViewModel();
            //productListView.BindingContext = model;
            load();
        }



        public async void loadProduct()
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
                var result = await CommonLib.GetSellerProduct(CommonLib.ws_MainUrl + "getSellerProducts?" + postData);
                if (result.status == 1)
                {

                    _expandedGroups = new ObservableCollection<ExpendProductViewModel>();



                    foreach (var group in result.data.products)
                    {



                        ExpendProductViewModel newGroup = new ExpendProductViewModel(group.Product.name,group.Product.id);


                        foreach (var food in group.addon)
                        {
                            string image = "";
                            if (food.ProductMedia.Count == 0) { image = "product_Placeholder.png"; }
                            foreach (var img in food.ProductMedia)
                            {
                                image = string.IsNullOrEmpty(img.media) ? "product_Placeholder.png" : CommonLib.img_MainUrl + img.media; break;
                            }
                            string _price = "";
                            if (food.ProductAddon.price_large != "0.00")
                            {
                                _price = food.ProductAddon.price_large;
                            }
                            if (food.ProductAddon.price_small != "0.00")
                            {
                                _price = food.ProductAddon.price_small;
                            }
                            if (food.ProductAddon.price_medium != "0.00")
                            {
                                _price = food.ProductAddon.price_medium;
                            }

                            string isvisible = "False";
                          
                            if (Convert.ToInt32(food.ProductAddon.quantity) <= 0)
                            { isvisible = "True"; }

                            newGroup.Add(new ProductPageItem { isvisible = isvisible, category_id = "x " + food.ProductAddon.quantity, user_id = food.ProductAddon.user_id, ProductName = food.ProductAddon.name, ProductId = food.ProductAddon.id, ProductImage = image, ProductPrice = _price,  });

                        }

                        _expandedGroups.Add(newGroup);
                    }

                    productListView.ItemsSource = _expandedGroups;

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






        private async void delete_Tapped(object sender, EventArgs e)
        {
            try
            {
                var getBtn = sender as Frame;
                var aa = getBtn.BindingContext;

                var ans = await App.Current.MainPage.DisplayAlert("", AppResources.deleteSellerProductMsg, AppResources.yes, AppResources.no);
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

                        string postData = "id=" + aa + "&user_id=" + objUser.userId;
                        var result = await CommonLib.DeleteCustomerAddress(CommonLib.ws_MainUrl + "deleteProduct?" + postData);
                        if (result.status == 1)
                        {


                            Loader.CloseAllPopup();



                            loadProduct();


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
            catch (Exception)
            {
            }



        }
        private async void edit_Tapped(object sender, EventArgs e)
        {
            try
            {
                var getBtn = sender as Frame;
                var aa = getBtn.BindingContext;



                EditProduct.id = aa.ToString();

                await App.Current.MainPage.Navigation.PushAsync(new EditProduct());



            }
            catch (Exception)
            {
            }



        }
        private async void addProduct_Tapped(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new AddProductPage());
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

        public async void load()
        {
            try
            {

                var result = await CommonLib.GetContactUs(CommonLib.ws_MainUrl + "getAdminContact?");
                if (result.status == 1)
                {

                    ProfileContentView.phone = result.data.phone;
                    ProfileContentView.name = result.data.name;

                }
            }
            catch (Exception ex)
            {
                Loader.CloseAllPopup();
            }
            if (Device.RuntimePlatform == Device.iOS)
            {
                DeviceTokken(); 
            }
        }
        //private void ProductListView_Refreshing(object sender, EventArgs e)
        //{
        //    SellerProductViewModel model = new SellerProductViewModel();
        //    productListView.BindingContext = model;
        //    productListView.EndRefresh();
        //}

        private void ProductListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            productListView.SelectedItem = null;
        }
    }
}