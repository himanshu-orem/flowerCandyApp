using System;
using System.Collections.Generic;
using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.ViewModels;
using Xamarin.Forms;

namespace FlowersAndCandyCustomer.Views
{
    public partial class ProfileContentView : ContentView
    {
        public static string phone = "";
        public static string name = "";

        public ProfileContentView()
        {
            InitializeComponent();
            load();
            BindingContext = new ProfileViewModel();
            LoggedInUser objUser = App.Database.GetLoggedInUser();
            if (objUser != null)
            {
                img.Source = string.IsNullOrEmpty(objUser.image) ? "user_placeholder.png" : objUser.image;
            }
            else
            {
                img.Source = "user_placeholder.png";
            }
           
        }
        public async void load()
        {
            try
            {
                
                var result = await CommonLib.GetContactUs(CommonLib.ws_MainUrl + "getAdminContact?");
                if (result.status == 1)
                {

                    phone = result.data.phone;
                    name = result.data.name;

                }
            }
            catch (Exception ex)
            {
                Loader.CloseAllPopup();
            }
        }
    }
}