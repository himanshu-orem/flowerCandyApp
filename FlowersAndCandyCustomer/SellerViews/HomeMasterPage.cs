using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Resources;
using FlowersAndCandyCustomer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace FlowersAndCandyCustomer.SellerViews
{
    public class HomeMasterPage : MasterDetailPage
    {
        MenuList masterPage;
        public HomeMasterPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            masterPage = new MenuList();
            Master = masterPage;

            Detail = new NavigationPage(new HomePage());


            masterPage.ListView.ItemSelected += OnItemSelected;
        }


        protected override bool OnBackButtonPressed()
        {
            back();
            return true;
        }
        public async void back()
        {
            var ans = await App.Current.MainPage.DisplayAlert("", AppResources.closeApp, AppResources.yes, AppResources.no);
            if (ans)
            {
                System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
            }

        }
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {

                if (item.Title == "Logout" || item.Title == "الخروج")
                {
                    var ans = await App.Current.MainPage.DisplayAlert("", AppResources.logout_text, AppResources.yes, AppResources.no);
                    if (ans == true)
                    {
                        try
                        {
                            App.Database.ClearLoginDetails();

                            masterPage.ListView.SelectedItem = null;
                            IsPresented = false;
                            Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                            App.Current.MainPage = new NavigationPage(new LoginPage());

                        }
                        catch (Exception ex)
                        {
                        }

                    }
                    else
                    {
                        masterPage.ListView.SelectedItem = null;
                        IsPresented = false;
                    }
                }
                else
                {
                    masterPage.ListView.SelectedItem = null;
                    IsPresented = false;
                    Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                }

            }
        }

    }
}