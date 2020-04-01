using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.Resources;
using FlowersAndCandyCustomer.Services;
using FlowersAndCandyCustomer.ViewModels;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlowersAndCandyCustomer.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddressListPage : ContentPage
	{
		public AddressListPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasBackButton(this, false);
            //language
            if (App.lang == "ar-AE")
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        private async void AddressList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            //if (addressList.SelectedItem != null)
            //{
            //    var data = addressList.SelectedItem as UsersAddress;

                 

            //        try
            //        {


            //            if (!CommonLib.checkconnection())
            //            {

            //                await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(AppResources._connection));
            //                await Task.Delay(1000);
            //                ShowMessage.CloseAllPopup();
            //                return;
            //            }


            //            await App.Current.MainPage.Navigation.PushPopupAsync(new Loader());

            //            LoggedInUser objUser = App.Database.GetLoggedInUser();

            //            string postData = "id=" + data.id + "&user_id=" + objUser.userId;
            //            var result = await CommonLib.DefaultCustomerAddress(CommonLib.ws_MainUrl + "addressDefault?" + postData);
            //        if (result.status == 1)
            //        {


            //            Loader.CloseAllPopup();

            //            AddressService.loaderCheck = true;
            //            CustomerAddressViewModel model = new CustomerAddressViewModel();
            //            addressList.BindingContext = model;

            //        }
            //        else
            //        {
            //            Loader.CloseAllPopup();

            //            await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(result.msg_ar));
            //            await Task.Delay(1000);
            //            ShowMessage.CloseAllPopup();
            //        }

            //        }
            //        catch (Exception ex)
            //        {
            //            Loader.CloseAllPopup();
            //        }


                
            //}

            addressList.SelectedItem = null;

        }

        private void AddressList_Refreshing(object sender, EventArgs e)
        {
            CustomerAddressViewModel model = new CustomerAddressViewModel();
            addressList.BindingContext = model;
            addressList.EndRefresh();
        }

        private void back_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void edit_Tapped(object sender, EventArgs e)
        {
            try
            {
                var getBtn = sender as Image;
                var aa = getBtn.BindingContext;
                EditDeliveryAddressPage.id = aa.ToString();

                await App.Current.MainPage.Navigation.PushAsync(new EditDeliveryAddressPage());

            }
            catch (Exception)
            {
            }



        }
        private async void delete_Tapped(object sender, EventArgs e)
        {
           
                var getBtn = sender as Image;
                var id = getBtn.BindingContext;
            var ans = await App.Current.MainPage.DisplayAlert("", AppResources.deleteAddressMsg, AppResources.yes, AppResources.no);
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

                    string postData = "id=" + id + "&user_id=" + objUser.userId;
                    var result = await CommonLib.DeleteCustomerAddress(CommonLib.ws_MainUrl + "deleteAddress?" + postData);
                    if (result.status == 1)
                    {


                        Loader.CloseAllPopup();



                        AddressService.loaderCheck = true;
                        CustomerAddressViewModel model = new CustomerAddressViewModel();
                        addressList.BindingContext = model;

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

        private async void addAddress_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new AddDeliveryAddressPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AddressService.loaderCheck = true;
            CustomerAddressViewModel model = new CustomerAddressViewModel();
            addressList.BindingContext = model;
        }
    }
}