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
    
    public class SellerProductService
    {
        public static bool loaderCheck = false;
        public async Task<List<ProductSeller>> GetItemsAsync(int pageIndex, int pageSize)
        {
            if (loaderCheck)
            {

                await App.Current.MainPage.Navigation.PushPopupAsync(new Loader());
            }



            pageIndex = pageIndex + 1;
            LoggedInUser objUser = App.Database.GetLoggedInUser();
            string postData = "user_id=" + objUser.userId + "&page=" + pageIndex;
            var result = await CommonLib.GetSellerProducts(CommonLib.ws_MainUrl + "getSellerProducts?" + postData);
            HomePageProductViewModel.rowCount = result.data.total_pages;

            List<ProductSeller> obj = new List<ProductSeller>();
            if (result.data.products != null)
            {
                foreach (var item in result.data.products)
                {
                    string image = "";
                    if (item.ProductMedia.Count == 0) { image = "product_Placeholder.png"; }
                    foreach (var img in item.ProductMedia)
                    {
                        image = string.IsNullOrEmpty(img.media) ? "product_Placeholder.png" : CommonLib.img_MainUrl + img.media; break;
                    }

                    obj.Add(new ProductSeller { category_id=item.Product.category_id,created=item.Product.created, deleted=item.Product.deleted, description=item.Product.description, id=item.Product.id, img= image, modified=item.Product.modified, name=item.Product.name, price=item.Product.price, quantity=item.Product.quantity, shop_id=item.Product.shop_id, status=item.Product.status, user_id=item.Product.user_id});
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
