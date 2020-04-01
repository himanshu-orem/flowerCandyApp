using FlowersAndCandyCustomer.DependencyInterface;
using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.Resources;
using FlowersAndCandyCustomer.SellerViews;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlowersAndCandyCustomer.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChangeLanguagePage : ContentPage
	{
		public ChangeLanguagePage ()
		{
			InitializeComponent ();
            Title = AppResources.setting;
            NavigationPage.SetHasBackButton(this, false);
            load();
        }
        public async void load()
        {
            Language langObj = App.Database.GetLanguage();
            if (langObj != null)
            {
                if (langObj.LangKey == "ar-AE")
                {
                    eng.Source = "checkbox_2.png";
                    arb.Source = "checkbox.png";
                }
                else
                {
                    arb.Source = "checkbox_2.png";
                    eng.Source = "checkbox.png";
                }
            }
            LoggedInUser objUser = App.Database.GetLoggedInUser();
            //
            if (objUser != null)
            {
                if (objUser.userType == "2")
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



                        string postData = "user_id=" + objUser.userId;
                        var result = await CommonLib.GetUserDetails(CommonLib.ws_MainUrl + "getUser?" + postData);
                        if (result.status == 1)
                        {
                            sellerLyt.IsVisible = true;
                            Loader.CloseAllPopup();
                            if (result.data.User.is_busy == "1")
                            {
                                statusToggel.IsToggled = true;
                            }
                            else
                            {
                                statusToggel.IsToggled = false;
                            }
                            if (result.data.User.is_close == "1")
                            {
                                shopToggel.IsToggled = true;
                            }
                            else
                            {
                                shopToggel.IsToggled = false;
                            }


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
        }
        private void ar_Tapped(object sender, EventArgs e)
        {
            eng.Source = "checkbox_2.png";
            arb.Source = "checkbox.png";

            string lang = "ar-AE";

            App.Lng = lang;


            Language objUser = new Language();
            objUser.ID = 1;
            objUser.LangKey = lang;
            App.lang = lang;
            App.Database.SaveLanguage(objUser);

            L10n.SetLocale();
            var netLanguage = DependencyService.Get<ILocale>().GetCurrent();
            AppResources.Culture = new CultureInfo(lang);


            LoggedInUser objUser1 = App.Database.GetLoggedInUser();
            if (objUser1 != null)
            {
                if (objUser1.userType == "2")
                {
                    if (objUser1.is_shop == "0")
                    {
                        App.Current.MainPage = new NavigationPage(new AddShopPage());
                    }
                    else
                    {
                        App.Current.MainPage = new NavigationPage(new HomeMasterPage());
                    }
                }
                if (objUser1.userType == "1")
                {
                    App.Current.MainPage = new NavigationPage(new MainPage());

                }
            }
            else
            {
                App.Current.MainPage = new NavigationPage(new MainPage());
            }
        }
        private void en_Tapped(object sender, EventArgs e)
        {
            arb.Source = "checkbox_2.png";
            eng.Source = "checkbox.png";


            string lang = "en-US";
            App.Lng = lang;
            Language objUser = new Language();
            objUser.ID = 1;
            objUser.LangKey = lang;
            App.lang = lang;
            App.Database.SaveLanguage(objUser);


            L10n.SetLocale();
            var netLanguage = DependencyService.Get<ILocale>().GetCurrent();
            AppResources.Culture = new CultureInfo(lang);

            LoggedInUser objUser1 = App.Database.GetLoggedInUser();
            if (objUser1 != null)
            {
                if (objUser1.userType == "2")
                {
                    if (objUser1.is_shop == "0")
                    {
                        App.Current.MainPage = new NavigationPage(new AddShopPage());
                    }
                    else
                    {
                        App.Current.MainPage = new NavigationPage(new HomeMasterPage());
                    }
                }
                if (objUser1.userType == "1")
                {
                    App.Current.MainPage = new NavigationPage(new MainPage());

                }
            }
            else
            {
                App.Current.MainPage = new NavigationPage(new MainPage());
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void status_Toggled(object sender, ToggledEventArgs e)
        {
            string status_data = "";
            if (statusToggel.IsToggled)
            {
                status_data = "1";
            }
            else
            {
                status_data = "0";
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
                string postData = "user_id=" + objUser.userId + "&is_busy=" + status_data;
                var result = await CommonLib.GetUserDetails(CommonLib.ws_MainUrl + "statusUpdate?" + postData);
                if (result.status == 1)
                {

                    Loader.CloseAllPopup();

                    if (statusToggel.IsToggled)
                    {
                        statusTxt.Text = "ONLINE";
                        statusTxt.TextColor = Color.Green;
                    }
                    else
                    {
                        statusTxt.Text = "OFFLINE";
                        statusTxt.TextColor = Color.Black;
                    }

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
        private async void shop_Toggled(object sender, ToggledEventArgs e)
        {
            string shop_data = "";
            if (shopToggel.IsToggled)
            {
                shop_data = "1";
            }
            else
            {
                shop_data = "0";
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
                string postData = "user_id=" + objUser.userId + "&is_close=" + shop_data;
                var result = await CommonLib.GetUserDetails(CommonLib.ws_MainUrl + "statusUpdate?" + postData);
                if (result.status == 1)
                {

                    Loader.CloseAllPopup();

                    if (shopToggel.IsToggled)
                    {
                        shopTxt.Text = AppResources.open;
                        shopTxt.TextColor = Color.Green;
                    }
                    else
                    {
                        shopTxt.Text = AppResources.close;
                        shopTxt.TextColor = Color.Black;
                    }

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
}