using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.Resources;
using FlowersAndCandyCustomer.Services;
using FlowersAndCandyCustomer.ViewModels;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlowersAndCandyCustomer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShopProducts : ContentPage
    {
        private ObservableCollection<ExpendProductViewModel> _expandedGroups;

        public static string shopId = "";
        public static string shopOffer = "";
        public ShopProducts()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);

            load();

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
                string postData = "shop_id=" + shopId + "&lat=" + ShopListPage.Lat + "&lng=" + ShopListPage.Lng;
                var result = await CommonLib.GetExpendProduct(CommonLib.ws_MainUrl + "getCategoryProducts?" + postData);
                if (result.status == 1)
                {

                    _expandedGroups = new ObservableCollection<ExpendProductViewModel>();

                    var _products = App.Database.GetAllProduct();


                    foreach (var group in result.data.products)
                    {



                        ExpendProductViewModel newGroup = new ExpendProductViewModel(group.Product.name, group.Product.id);


                        foreach (var food in group.addons)
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
                            if (food.ProductAddon.status == "0")
                            { isvisible = "True"; }
                            if (Convert.ToInt32(food.ProductAddon.quantity) <= 0)
                            { isvisible = "True"; }

                            newGroup.Add(new ProductPageItem { isvisible = isvisible, category_id = food.ProductAddon.category_id, user_id = food.ProductAddon.user_id, ProductName = food.ProductAddon.name, ProductId = food.ProductAddon.id, ProductImage = image, ProductPrice = _price, ShopAddress = group.Product.distance_in_km + " Km" });

                        }

                        _expandedGroups.Add(newGroup);
                    }

                    GroupedView.ItemsSource = _expandedGroups;

                    if (_expandedGroups.Count == 0)
                    {
                        nodataImg.IsVisible = true;
                    }
                    else
                    {
                        nodataImg.IsVisible = false;
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

        private async void GroupedView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (GroupedView.SelectedItem != null)
                {
                    var selectedData = GroupedView.SelectedItem as ProductPageItem;
                    if (selectedData.isvisible == "False")
                    {
                        AddOrderViewModel.userId = selectedData.user_id;
                        AddOrderViewModel.catid = selectedData.category_id;
                        AddOrderViewModel.id = selectedData.ProductId;

                        App.check = true;

                        await App.Current.MainPage.Navigation.PushAsync(new AddOrderPage());
                    }
                }
            }
            catch (Exception ex)
            {
            }
            GroupedView.SelectedItem = null;
        }
        private void back_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}