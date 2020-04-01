using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.Resources;
using FlowersAndCandyCustomer.ViewModels;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlowersAndCandyCustomer.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditDeliveryAddressPage : ContentPage
	{
        public static bool check = false;
        public static string Latitude = string.Empty;
        public static string Longitude = string.Empty;
        public static string Address = string.Empty;

        public static string id = "";
		public EditDeliveryAddressPage ()
		{
            check = false;
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);

            if (Device.RuntimePlatform == Device.iOS)
            {
                doneBtn.CornerRadius = 20;
                fullNameTxt.Margin = new Thickness(0, 15, 0, 0);
                zipcodeTxt.Margin = new Thickness(0, 15, 0, 0);
                cityTxt.Margin = new Thickness(0, 15, 0, 0);
                stateTxt.Margin = new Thickness(0, 15, 0, 0);
                addressTxt.Margin = new Thickness(0, 15, 0, 0);
                landmarkTxt.Margin = new Thickness(0, 15, 0, 0);
                countryPicker.Margin = new Thickness(0, 15, 0, 0);

                cityLbl.Margin = new Thickness(0, 30, 0, 0);
                stateLbl.Margin = new Thickness(0, 30, 0, 0);
                addressLbl.Margin = new Thickness(0, 30, 0, 0);
                zipcodeLbl.Margin = new Thickness(0, 30, 0, 0);
                countryLbl.Margin = new Thickness(0, 30, 0, 0);
                fullNameLbl.Margin = new Thickness(0, 30, 0, 0);
                landmarkLbl.Margin = new Thickness(0, 30, 0, 0);
            }

            
            getCountryCodes();
            load();
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

                string postData = "user_id=" + objUser.userId + "&id=" + id;
                var result = await CommonLib.GetCustomerAddressData(CommonLib.ws_MainUrl + "getAddressInfo?" + postData);
                if (result.status == 1)
                {

                    fullNameTxt.Text = result.data.UsersAddress.full_name;
                    countryPicker.SelectedItem = result.data.UsersAddress.country;
                    stateTxt.Text = result.data.UsersAddress.state;
                    cityTxt.Text = result.data.UsersAddress.city;
                    addressTxt.Text = result.data.UsersAddress.full_address;
                    landmarkTxt.Text = result.data.UsersAddress.landmark;
                    zipcodeTxt.Text = result.data.UsersAddress.zipcode;
                    Latitude = result.data.UsersAddress.lat;
                    Longitude = result.data.UsersAddress.lng;

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

        public void getCountryCodes()
        {
            var getList = CommonLib.LoadJson();
            getList = getList.OrderBy(j => j.name).ToList();
            foreach (var item in getList)
            {
                countryPicker.Items.Add(item.name);
            }
        }

        private void back_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        public string CheckValidations()
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(fullNameTxt.Text))
            {
                msg += AppResources.please_enter_full_name_validation + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(stateTxt.Text))
            {
                msg += AppResources.please_enter_state_validation + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(cityTxt.Text))
            {
                msg += AppResources.please_enter_city_validation + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(addressTxt.Text))
            {
                msg += AppResources.please_enter_address_validation + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(landmarkTxt.Text))
            {
                msg += AppResources.please_enter_landmark_validation + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(zipcodeTxt.Text))
            {
                msg += AppResources.please_enter_zipcode_validation;
            }
            if (countryPicker.SelectedItem == null)
            {
                msg += AppResources.selectCountry;
            }

            return msg;
        }
        private async void DoneBtn_Clicked(object sender, EventArgs e)
        {
            var returnMessage = CheckValidations();
            if (!string.IsNullOrEmpty(returnMessage))
            {
                await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(returnMessage));
                await Task.Delay(1000);
                ShowMessage.CloseAllPopup();
                return;
            }
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

                string postData = "user_id=" + objUser.userId + "&full_name=" + fullNameTxt.Text + "&country=" + countryPicker.SelectedItem + "&state=" + stateTxt.Text + "&city=" + cityTxt.Text + "&lat=" + Latitude + "&lng=" + Longitude + "&full_address=" + addressTxt.Text + "&zipcode=" + zipcodeTxt.Text + "&id=" + id + "&landmark=" + landmarkTxt.Text;
                var result = await CommonLib.DefaultCustomerAddress(CommonLib.ws_MainUrl + "addAddress?" + postData);
                if (result.status == 1)
                {


                    Loader.CloseAllPopup();

                    Navigation.PopAsync();

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
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if(check==true)
            addressTxt.Text = Address;
        }
        private async void AddressTxt_Focused(object sender, FocusEventArgs e)
        {
            SearchhPage.check = 1;
            check = true;
            await App.Current.MainPage.Navigation.PushAsync(new Views.SearchhPage());
        }
    }
}
