using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.ViewModels;
using FlowersAndCandyCustomer.Views;

namespace FlowersAndCandyCustomer.Services
{

    public class ShopListService
    {
        public static bool loaderCheck = false;
        public async Task<List<ShopList>> GetItemsAsync(int pageIndex, int pageSize, string search)
        {
            if (loaderCheck)
            {
                try
                {
                    await App.Current.MainPage.Navigation.PushPopupAsync(new Loader());
                    await Task.Delay(4000);
                }
                catch (Exception)
                {
                }
            }



            pageIndex = pageIndex + 1;
            LoggedInUser objUser = App.Database.GetLoggedInUser();
            string postData = "&page=" + pageIndex + "&lat=" + ShopListPage.Lat + "&lng=" + ShopListPage.Lng + "&cat_id=" + ShopListPage.catId + "&is_offer=" + ShopProducts.shopOffer + "&text=" + search;
            var result = await CommonLib.GetShopList(CommonLib.ws_MainUrl + "getShops?" + postData);
            ShopListViewModel.rowCount = result.data.total_pages;

            List<ShopList> obj = new List<ShopList>();
            if (result.data.shops != null)
            {
                foreach (var item in result.data.shops)
                {

                    string image = string.IsNullOrEmpty(item.Shop.logo) ? "product_Placeholder.png" : CommonLib.img_MainUrl + item.Shop.logo;
                    string imageBanner = string.IsNullOrEmpty(item.Shop.banner) ? "product_Placeholder.png" : CommonLib.img_MainUrl + item.Shop.banner;

                    obj.Add(new ShopList {is_close=item.User.is_close=="1"?"False":"True", banner= imageBanner, address = item.Shop.address, logo = image, category_id = item.Shop.category_id, communication_number = item.Shop.communication_number, company_name = item.Shop.company_name, country_code = item.Shop.country_code, created = item.Shop.created, delivery_system = item.Shop.delivery_system, id = item.Shop.id, latitude = item.Shop.latitude, license_number = item.Shop.license_number, longitude = item.Shop.longitude, user_id = item.Shop.user_id });
                }
            }


            if (loaderCheck)
            {
                try
                {
                    Loader.CloseAllPopup1();
                }
                catch (Exception)
                {
                }
            }
            loaderCheck = false;

            return obj;


        }
    }
}