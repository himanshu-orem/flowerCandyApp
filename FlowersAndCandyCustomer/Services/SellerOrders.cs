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
using FlowersAndCandyCustomer.Resources;

namespace FlowersAndCandyCustomer.Services
{

    public class SellerOrders
    {
        public static bool loaderCheck = false;
        public async Task<List<OrderSeller>> GetItemsAsync(int pageIndex, int pageSize, string type)
        {
            pageIndex = pageIndex + 1;
            LoggedInUser objUser = App.Database.GetLoggedInUser();
            string postData = "user_id=" + objUser.userId + "&type=" + type;
            var result = await CommonLib.GetSellerOrder(CommonLib.ws_MainUrl + "getSellerOrders?" + postData);
            List<OrderSeller> obj = new List<OrderSeller>();
            if (result.data != null)
            {

                foreach (var item in result.data.orders)
                {
                    obj.Add(new OrderSeller { created = item.Order.created, driver_id = item.Order.driver_id, id = item.Order.id, modified = item.Order.modified, payment_method = item.Order.payment_method == "1" ? AppResources.cash : AppResources.card, payment_status = item.Order.payment_status, status = item.Order.status, success_time = item.Order.success_time, total_cost = item.Order.total_cost, transaction_id = item.Order.transaction_id, user_id = item.Order.user_id });
                }

            }
            return obj;


        }
    }
}
