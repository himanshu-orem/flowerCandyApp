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
    
    public class ProductAddonService
    {
        public static bool loaderCheck = false;
        public async Task<List<Addon>> GetItemsAsync(int pageIndex, int pageSize, string productId, string catId, string userId)
        {
            if (loaderCheck)
            {

               // await App.Current.MainPage.Navigation.PushPopupAsync(new Loader());
            }



            pageIndex = pageIndex + 1;

            string postData = "cat_id=" + catId + "&user_id=" + userId + "&page=" + pageIndex + "&product_id=" + productId;
            var result = await CommonLib.GetProductAddon(CommonLib.ws_MainUrl + "getCategoryShopAddon?" + postData);
            ProductAddonViewModel.rowCount = result.data.addon_total_pages;

            List<Addon> obj = new List<Addon>();
            if (result.data.addon != null)
            {
                foreach (var item in result.data.addon)
                {
                    string image = "";
                    if (item.ProductMedia.Count == 0) { image = "product_Placeholder.png"; }
                    foreach (var cn in item.ProductMedia)
                    {
                        image =string.IsNullOrEmpty(cn.media)? "product_Placeholder.png" : CommonLib.img_MainUrl + cn.media; break;
                    }

                    obj.Add(new Addon {image= image,checkbox = "ico_check.png", qty="1", category_id = item.ProductAddon.category_id, created = item.ProductAddon.created, id = item.ProductAddon.id,   modified = item.ProductAddon.modified, name = item.ProductAddon.name, price = item.ProductAddon.price, quantity = item.ProductAddon.quantity, user_id = item.ProductAddon.user_id });
                }
            }


            if (loaderCheck) {
               // Loader.CloseAllPopup(); 
            }
            loaderCheck = false;

            return obj;


        }
    }
}
