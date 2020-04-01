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
     
    public class HomePageProductService
    {
        public static bool loaderCheck = false;
        public async Task<List<HomeProduct>> GetItemsAsync(int pageIndex, int pageSize,string catId)
        {
            if (loaderCheck) {
              
                await App.Current.MainPage.Navigation.PushPopupAsync(new Loader());
            }

            

            pageIndex = pageIndex + 1;
            LoggedInUser objUser = App.Database.GetLoggedInUser();
            string postData = "shop_id=" + ShopProducts.shopId + "&page=" + pageIndex + "&lat=" + ShopListPage.Lat + "&lng=" + ShopListPage.Lng + "&is_offer=" + ShopProducts.shopOffer;
            var result = await CommonLib.HomePageProduct(CommonLib.ws_MainUrl + "getCategoryProducts?" + postData);
            HomePageProductViewModel.rowCount = result.data.total_pages;

            List<HomeProduct> obj = new List<HomeProduct>();
            if (result.data.products != null)
            {
                foreach (var item in result.data.products)
                {
                    string image = "";
                    if (item.ProductMedia.Count==0) { image = "product_Placeholder.png"; }
                    foreach (var img in item.ProductMedia)
                    {
                        image = string.IsNullOrEmpty(img.media)? "product_Placeholder.png" : CommonLib.img_MainUrl + img.media; break;
                    }

                    obj.Add(new HomeProduct {prepare_time=item.Product.prepare_time,   shopaddress = item.shops.address, distance_in_km = item.Product.distance_in_km+" Km", deleted = item.Product.deleted, shop_id = item.Product.shop_id, category_id = item.Product.category_id, created = item.Product.created, description = item.Product.description, id = item.Product.id, image = image, modified = item.Product.modified, name = item.Product.name, price = item.Product.price, quantity = item.Product.quantity, user_id = item.Product.user_id });
                }
            }


            if (loaderCheck) { Loader.CloseAllPopup(); }
            loaderCheck = false;

            return obj;


        }
    }
}
