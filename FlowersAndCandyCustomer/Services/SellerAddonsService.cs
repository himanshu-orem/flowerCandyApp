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
using FlowersAndCandyCustomer.SellerViews;
using FlowersAndCandyCustomer.Resources;

namespace FlowersAndCandyCustomer.Services
{

    public class SellerAddonsService
    {
        public static bool loaderCheck = false;
        public async Task<List<addAddons>> GetItemsAsync(int pageIndex, int pageSize)
        {
            List<addAddons> obj = new List<addAddons>();
            try
            {
                AddAddonsViewModel.addonsList.Clear();
                obj.Clear();
            }
            catch (Exception)
            {
            }

            if (!string.IsNullOrEmpty(EditProduct.id))
            {
                await App.Current.MainPage.Navigation.PushPopupAsync(new Loader());

                string postData = "id=" + EditProduct.id;
                var result = await CommonLib.EditProductDetails(CommonLib.ws_MainUrl + "productDetail?" + postData);
                if (result.data.addon.Count != 0)
                {
                    AddAddonsViewModel.addonsList.Clear();


                    bool check = true;
                    int id = 1;
                    foreach (var item in result.data.addon)
                    {


                        string image = "";
                        if (item.ProductMedia.Count == 0) { image = "product_Placeholder.png"; }
                        foreach (var img in item.ProductMedia)
                        {
                            image = string.IsNullOrEmpty(img.media) ? "product_Placeholder.png" : CommonLib.img_MainUrl + img.media; break;
                        }

                        obj.Add(new addAddons { acdcImg = item.ProductAddon.status == "1" ? "active_ic.png" : "deactive_ic.png", acdcLbl = item.ProductAddon.status == "1" ? AppResources.show : AppResources.hide, name = item.ProductAddon.name, priceS = item.ProductAddon.price_small, priceL = item.ProductAddon.price_large, priceM = item.ProductAddon.price_medium, qty = item.ProductAddon.quantity, AddonsId = item.ProductAddon.id, id = id, addButton = check == true ? "True" : "False", deleteButton = "True", imgSource = image });
                        check = false;
                        id++;


                    }
                    Loader.CloseAllPopup();
                    return obj;
                }
                else
                {
                    AddAddonsViewModel.addonsList.Clear();

                    obj.Add(new addAddons { acdcImg = "active_ic.png", acdcLbl = AppResources.show, AddonsId = "", id = 1, addButton = "True", deleteButton = "False", imgSource = "icon_add_file.png" });
                    Loader.CloseAllPopup();

                    return obj;

                }
            }
            else
            {
                AddAddonsViewModel.addonsList.Clear();

                obj.Add(new addAddons { acdcImg = "active_ic.png", acdcLbl = AppResources.show, AddonsId = "", id = 1, addButton = "True", deleteButton = "False", imgSource = "icon_add_file.png" });
                return obj;
            }

        }
    }
}
