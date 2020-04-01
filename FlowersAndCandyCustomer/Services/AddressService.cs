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
     
    public class AddressService
    {
        public static bool loaderCheck = false;
        public async Task<List<UsersAddress>> GetItemsAsync(int pageIndex, int pageSize)
        {
            if (loaderCheck)
            {

                await App.Current.MainPage.Navigation.PushPopupAsync(new Loader());
            }



            pageIndex = pageIndex + 1;
            LoggedInUser objUser = App.Database.GetLoggedInUser();
            string postData = "user_id=" + objUser.userId;
            var result = await CommonLib.GetCustomerAddress(CommonLib.ws_MainUrl + "getAllAddresses?" + postData);
            List<UsersAddress> obj = new List<UsersAddress>();
            if (result.data != null) { 
           
            foreach (var item in result.data)
            {
                obj.Add(new UsersAddress { img = item.UsersAddress.is_default == "1" ? "check_1.png" : "check_1.png", city = item.UsersAddress.city, country = item.UsersAddress.country, full_address = item.UsersAddress.full_address, full_name = item.UsersAddress.full_name, id = item.UsersAddress.id, is_default = item.UsersAddress.is_default, landmark = item.UsersAddress.landmark, lat = item.UsersAddress.lat, lng = item.UsersAddress.lng, state = item.UsersAddress.state, user_id = item.UsersAddress.user_id, zipcode = item.UsersAddress.zipcode });
            }

        }
            if (loaderCheck)
            {
                 Loader.CloseAllPopup(); 
            }
            loaderCheck = false;

            return obj;


        }
    }
}
