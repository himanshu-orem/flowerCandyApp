using FFImageLoading.Forms;
using FFImageLoading.Work;
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
    public class MenuList : ContentPage
    {

        public ListView ListView { get { return listView; } }
        public static ListView listView;

        public static string _chatCount;

        public MenuList()
        {
            var masterPageItems = new List<MasterPageItem>();

            masterPageItems.Add(new MasterPageItem
            {
                Title = AppResources.home,
                IconSource = "home.png",
                TargetType = typeof(HomePage),

            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = AppResources.profile,
                IconSource = "profile.png",
                TargetType = typeof(SellerProfilePage),

            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = AppResources.shop,
                IconSource = "add_on.png",
                TargetType = typeof(EditShopPage),

            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = AppResources.orders,
                IconSource = "products.png",
                TargetType = typeof(SellerOrderPage),

            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = AppResources.logout,
                IconSource = "logout.png",
                TargetType = typeof(LoginPage),
            });


            listView = new ListView
            {

                ItemsSource = masterPageItems,
                ItemTemplate = new DataTemplate(typeof(CustomCell)),
                VerticalOptions = LayoutOptions.FillAndExpand,
                SeparatorVisibility = SeparatorVisibility.None,
                HasUnevenRows = true,
                BackgroundColor = Color.Transparent,

            };

            Padding = new Thickness(0, 40, 0, 0);
            Icon = "hamburger.png";
            Title = "Personal Organiser";

            BackgroundImage = "drawer_bg.png";
            BackgroundColor = Color.Transparent;


            LoggedInUser objUser = App.Database.GetLoggedInUser();
            string name = "";
            string email = "";
            string image = "";
            if (objUser != null)
            {
                name = objUser.fname+" "+ objUser.lname;
                email = objUser.email;
                image = string.IsNullOrEmpty(objUser.image) ? "user_placeholder2.png" : objUser.image;
            }

            CachedImage img = new CachedImage()
            {
                Source = image,
                HorizontalOptions = LayoutOptions.Center,
                HeightRequest = 100,
                WidthRequest = 100,
                Aspect = Aspect.AspectFit,
                LoadingPlaceholder = "user_placeholder2.png",
                Transformations = new List<ITransformation>()
                {
                   new FFImageLoading.Transformations.CircleTransformation()
                   {
                       BorderSize=5, BorderHexColor="#FE1F78"
                   }
                },
            };
            Label namelbl = new Label()
            {
                Text = name,
                FontFamily = "CALIBRI",
                StyleId = "CALIBRI",
                TextColor = Color.FromHex("#FE1F78"),
                FontSize = 20,
                HorizontalTextAlignment = TextAlignment.Center
            };
             



            Image lineimg = new Image()
            {
                Source = "line.png",

            };

            StackLayout _imglinelayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.Transparent,
                Padding = new Thickness(0, 25, 0, 25),
                Children = {
                 lineimg
                }
            };
            

            StackLayout _layout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Transparent,
                Spacing = 0,

                Children = {
                    img,namelbl,listView
                }
            };

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Transparent,

                Children = {
                   _layout
                }
            };

        }


        


        // override onappear

        

        public class CustomCell : ViewCell
        {
            int fontSize = 16;
            public CustomCell()
            {
                Image img = new Image
                {
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Margin = new Thickness(20, 0, 0, 0),
                    WidthRequest = 20,
                    HeightRequest = 20,
                };
                img.SetBinding(Image.SourceProperty, new Binding("IconSource"));

                Label lbl = new Label
                {
                    FontSize = fontSize,     FontFamily = "CALIBRI", 
                                             StyleId = "CALIBRI",
                    HorizontalTextAlignment = TextAlignment.Start,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    TextColor = Color.Gray,
                    Margin = new Thickness(20, 10, 0, 10),



                };
                lbl.SetBinding(Label.TextProperty, new Binding("Title"));
                Image lineimg = new Image()
                {
                    Source = "line.png",

                };
                var sl_projectinfoHor = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.Start,

                    Padding = new Thickness(0, 0, 0, 0),
                    Spacing = 0,
                    Orientation = StackOrientation.Horizontal,
                    Children =
                    {
                        img, lbl 
                    }
                };
                var sl_projectinfo = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Spacing = 0,
                    Orientation = StackOrientation.Vertical,
                    Children =
                    {
                        sl_projectinfoHor
                    }
                };

                View = sl_projectinfo;
            }

        }
    }
}