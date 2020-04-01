using System;
using System.Collections.Generic;
using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.Resources;
using FlowersAndCandyCustomer.Services;
using FlowersAndCandyCustomer.ViewModels;
using Xamarin.Forms;

namespace FlowersAndCandyCustomer.Views
{
    public partial class AddOrderPage : ContentPage
    {
        public static string price = "";
        public static string priceType = "M";
        public static string priceM = "0.00";
        public static string priceL = "0.00";
        public static string priceS = "0.00";

        public AddOrderPage()
        {
            InitializeComponent();
            LoggedInUser objUser = App.Database.GetLoggedInUser();
            if (objUser == null)
            {

                crtImg.Source = "";
            }
            if (Device.OS == TargetPlatform.iOS)
            {
                crtImg.Margin = new Thickness(0, 6, 0, 0);
            }
            //  load();



            NavigationPage.SetHasBackButton(this, false);

            firstLbl.WidthRequest = (App.ScreenWidth / 2) + 50;
            secondLbl.WidthRequest = (App.ScreenWidth / 2) - 50;


            if (App.check == true)
            {
                BindingContext = new AddOrderViewModel(Navigation);



            }

            try
            {
                MessagingCenter.Subscribe<string>(this, "test", (sender) =>
                {

                    var objScan = sender as string;
                    firstLbl.Text = AppResources.total_package_price + ":  " + objScan;
                    qtyTextVal.Text = Convert.ToString(AddOrderViewModel.flowerQty);

                });
            }
            catch (Exception)
            {
            }

            try
            {
                MessagingCenter.Subscribe<string>(this, "testPriceType", (sender) =>
                {
                    if (priceType == "S")
                    {
                        Simg.Source = "checkbox.png";
                        Mimg.Source = "checkbox_2.png";
                        Limg.Source = "checkbox_2.png";
                    }
                    if (priceType == "L")
                    {
                        Limg.Source = "checkbox.png";
                        Mimg.Source = "checkbox_2.png";
                        Simg.Source = "checkbox_2.png";
                    }
                    if (priceType == "M")
                    {
                        Limg.Source = "checkbox_2.png";
                        Simg.Source = "checkbox_2.png";
                        Mimg.Source = "checkbox.png";
                    }
                    if (priceS == "0.00")
                    {
                        sLty.IsVisible = false;
                    }
                   
                    if (priceL == "0.00")
                    {
                        lLty.IsVisible = false;
                    }
                    if (priceM == "0.00")
                    {
                        mLty.IsVisible = false;
                    }

                });
            }
            catch (Exception)
            {
            }


        }



        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var _products = App.Database.GetAllCount();
                cartCountLbl.Text = _products.ToString();
            }
            catch (Exception)
            {
            }
        }

        private void M_Tapped(object sender, EventArgs e)
        {

            Limg.Source = "checkbox_2.png";
            Simg.Source = "checkbox_2.png";
            Mimg.Source = "checkbox.png";
            AddOrderPage.price = mPriceTxt.Text;
            AddOrderViewModel.productPrice = mPriceTxt.Text;
            AddOrderViewModel.flowerQty = 1;
            qtyTextVal.Text = "1";
            firstLbl.Text = AppResources.total_package_price + ":  " + mPriceTxt.Text;
            priceType = "M";
        }
        private void L_Tapped(object sender, EventArgs e)
        {
            Limg.Source = "checkbox.png";
            Mimg.Source = "checkbox_2.png";
            Simg.Source = "checkbox_2.png";

            AddOrderPage.price = lPriceTxt.Text;
            AddOrderViewModel.productPrice = lPriceTxt.Text;
            AddOrderViewModel.flowerQty = 1;
            qtyTextVal.Text = "1";
            firstLbl.Text = AppResources.total_package_price + ":  " + lPriceTxt.Text;
            priceType = "L";
        }
        private void S_Tapped(object sender, EventArgs e)
        {
            Simg.Source = "checkbox.png";
            Mimg.Source = "checkbox_2.png";
            Limg.Source = "checkbox_2.png";
            AddOrderPage.price = sPriceTxt.Text;
            AddOrderViewModel.productPrice = sPriceTxt.Text;
            AddOrderViewModel.flowerQty = 1;
            qtyTextVal.Text = "1";
            firstLbl.Text = AppResources.total_package_price + ":  " + sPriceTxt.Text;
            priceType = "S";
        }
    }
}
