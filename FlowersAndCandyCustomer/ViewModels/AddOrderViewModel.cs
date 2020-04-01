using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FlowersAndCandyCustomer.DependencyInterface;
using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.Resources;
using FlowersAndCandyCustomer.Views;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace FlowersAndCandyCustomer.ViewModels
{
    public class AddOrderViewModel : INotifyPropertyChanged
    {
        public static string userId = "";
        public static string catid = "";
        public static string id = "";

        public static string productQty = "";
        public static string productPrice = "";

        public static string baseProductId = "";
        public static string productId = "";
        public static string productName = "";
        public static string productImage = "";

        public static string shopAddress = "";
        public static string shopId = "";


        public static int rowCount = 0;


        public INavigation _navigation;
        public static int flowerQty = 1;

        ObservableCollection<ProductListingModel> _list;

        private ObservableCollection<ProductListingModel> _productList;
        public ObservableCollection<ProductListingModel> ProductList
        {
            get
            {
                return _productList;

            }
            set
            {
                _productList = value;
                OnPropertyChanged();
            }
        }


        private string _shopName;
        public string ShopName
        {
            get
            {
                return _shopName;
            }
            set
            {
                _shopName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ShopName"));
            }
        }

        private string _location;
        public string Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Location"));
            }
        }


        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }

        private string _image;
        public string Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Image"));
            }
        }

        private string _mprice;
        public string MPrice
        {
            get
            {
                return _mprice;
            }
            set
            {
                _mprice = value;
                PropertyChanged(this, new PropertyChangedEventArgs("MPrice"));
            }
        }
        private string _lprice;
        public string LPrice
        {
            get
            {
                return _lprice;
            }
            set
            {
                _lprice = value;
                PropertyChanged(this, new PropertyChangedEventArgs("LPrice"));
            }
        }
        private string _sprice;
        public string SPrice
        {
            get
            {
                return _sprice;
            }
            set
            {
                _sprice = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SPrice"));
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Description"));
            }
        }

        private string _flowerQty;
        public string FlowerQty
        {
            get
            {
                return _flowerQty;
            }
            set
            {
                _flowerQty = value;
                PropertyChanged(this, new PropertyChangedEventArgs("FlowerQty"));
            }
        }

        private string _note;
        public string Note
        {
            get
            {
                return _note;
            }
            set
            {
                _note = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Note"));
            }
        }

        private string _totalPrice;
        public string TotalPrice
        {
            get
            {
                return _totalPrice;
            }
            set
            {
                _totalPrice = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TotalPrice"));
            }
        }

        private double _listViewHeight;
        public double ListViewHeight
        {
            get
            {
                return _listViewHeight;
            }
            set
            {
                _listViewHeight = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ListViewHeight"));
            }
        }

        public AddOrderViewModel(INavigation navigation)
        {
            _navigation = navigation;

            load();
            flowerQty = 1;




        }
        public async void load()
        {
            try
            {


                if (!CommonLib.checkconnection())
                {

                    await _navigation.PushPopupAsync(new ShowMessage(AppResources._connection));
                    await Task.Delay(1000);
                    await _navigation.PopPopupAsync();
                    return;
                }


                await App.Current.MainPage.Navigation.PushPopupAsync(new Loader());



                string postData = "cat_id=" + catid + "&id=" + id + "&lat=" + ShopListPage.Lat + "&lng=" + ShopListPage.Lng;
                var result = await CommonLib.GetProductInfo(CommonLib.ws_MainUrl + "getAddonInfo?" + postData);
                if (result.status == 1)
                {
                    //rowCount= result.data.addon_total_pages;
                    string price = "";
                    if (result.data.addonInfo.ProductAddon.price_small != "0.00")
                    {
                        price = result.data.addonInfo.ProductAddon.price_small;
                        AddOrderPage.priceType = "S";
                    }
                    if (result.data.addonInfo.ProductAddon.price_large != "0.00")
                    {
                        price = result.data.addonInfo.ProductAddon.price_large;
                        AddOrderPage.priceType = "L";
                    }
                    if (result.data.addonInfo.ProductAddon.price_medium != "0.00")
                    {
                        price = result.data.addonInfo.ProductAddon.price_medium;
                        AddOrderPage.priceType = "M";
                    }

                    Loader.CloseAllPopup();

                    TotalPrice = AppResources.total_package_price + ":  " + price;

                    AddOrderPage.price = price;


                    productPrice = price;

                    baseProductId = result.data.addonInfo.ProductAddon.product_id;

                    productId = result.data.addonInfo.ProductAddon.id;
                    productName = result.data.addonInfo.ProductAddon.name;




                    productQty = result.data.addonInfo.ProductAddon.quantity;

                    FlowerQty = "1";
                    ShopName = result.data.addonInfo.shops.company_name;

                    shopAddress = result.data.addonInfo.shops.address;
                    shopId = result.data.addonInfo.shops.id;

                    Location = result.data.addonInfo.ProductAddon.distance_in_km + " Km";

                    productImage = "";

                    string image = "";
                    if (result.data.addonInfo.ProductMedia.Count == 0) { image = "product_Placeholder.png"; }
                    foreach (var item in result.data.addonInfo.ProductMedia)
                    {
                        image = string.IsNullOrEmpty(item.media) ? "product_Placeholder.png" : CommonLib.img_MainUrl + item.media;
                        break;
                    }

                    productImage = image;


                    Image = image;

                    Name = result.data.addonInfo.ProductAddon.name;
                    MPrice = result.data.addonInfo.ProductAddon.price_medium;
                    LPrice = result.data.addonInfo.ProductAddon.price_large;
                    SPrice = result.data.addonInfo.ProductAddon.price_small;

                    AddOrderPage.priceM = result.data.addonInfo.ProductAddon.price_medium;
                    AddOrderPage.priceL = result.data.addonInfo.ProductAddon.price_large;
                    AddOrderPage.priceS = result.data.addonInfo.ProductAddon.price_small;


                    Description = "";


                    MessagingCenter.Send(
                           price.ToString(), "testPriceType");


                }
                else
                {
                    Loader.CloseAllPopup();

                    if (App.Lng == "ar-AE")
                    {
                        await _navigation.PushPopupAsync(new ShowMessage(result.msg_ar));

                    }
                    else
                    {
                        await _navigation.PushPopupAsync(new ShowMessage(result.msg_en));

                    }
                    await Task.Delay(1000);
                    await _navigation.PopPopupAsync();
                }

            }
            catch (Exception ex)
            {
                Loader.CloseAllPopup();
            }
        }

        public Command BackImgTapped
        {
            get
            {
                return new Command((e) =>
                {
                    AddOrderPage.price = "";
                    App.Current.MainPage.Navigation.PopAsync();
                });
            }
        }
        public Command DownloadImgTapped
        {
            get
            {
                return new Command((e) =>
                {
                    if (!string.IsNullOrEmpty(productImage) && productImage != "product_Placeholder.png")
                    {
                        ImageSource img = productImage;
                        DependencyService.Get<IDownloadService>().Share(img);
                    }
                });
            }
        }
        public Command CartImgTapped
        {

            get
            {
                return new Command((e) =>
                {
                    LoggedInUser objUser = App.Database.GetLoggedInUser();
                    if (objUser != null)
                    {
                        _navigation.PushAsync(new CartPage());
                    }
                });
            }
        }

        public Command FlowerQtyDecrease
        {
            get
            {
                return new Command((e) =>
                {
                    if (flowerQty > 1)
                    {
                        App.check = false;

                        flowerQty--;
                        FlowerQty = flowerQty.ToString();

                        double price = (Convert.ToDouble(AddOrderPage.price) - Convert.ToDouble(productPrice));

                        AddOrderPage.price = price.ToString();
                        AddOrderPage homePage = new AddOrderPage();
                        MessagingCenter.Send(
                            price.ToString(), "test");


                        // flowerQty--;
                    }
                });
            }
        }

        public Command FlowerQtyIncrease
        {
            get
            {
                return new Command((e) =>
                {
                    if (flowerQty <= Convert.ToInt32(productQty))
                    {
                        App.check = false;


                        flowerQty++;
                        FlowerQty = flowerQty.ToString();

                        double price = (Convert.ToDouble(AddOrderPage.price) + Convert.ToDouble(productPrice));

                        AddOrderPage.price = price.ToString();
                        AddOrderPage homePage = new AddOrderPage();
                        MessagingCenter.Send(
                            price.ToString(), "test");
                    }

                });
            }
        }

        public Command buy_now_command
        {
            get
            {
                return new Command(async (e) =>
                {
                    LoggedInUser objUser = App.Database.GetLoggedInUser();
                    if (objUser != null)
                    {

                        var ans = await App.Current.MainPage.DisplayAlert("", AppResources.addtocart_text, AppResources.yes, AppResources.no);
                        if (ans)
                        {
                            string productQty = flowerQty.ToString();
                            CartProductDetail objcart = new CartProductDetail();
                            objcart.productCategory = catid;
                            objcart.productId = productId;
                            objcart.baseProductId = baseProductId;
                            objcart.productImage = productImage;
                            objcart.productName = productName;
                            objcart.productPrice = productPrice;
                            objcart.typePrice = AddOrderPage.priceType;
                            objcart.productQty = productQty;
                            objcart.note = Note;
                            objcart.shopAddress = shopAddress;
                            objcart.shopId = Convert.ToInt32(shopId);
                            App.Database.SaveCartProduct(objcart);

                            CartProductDetail objLastProduct = App.Database.GetLastProduct();

                            var productAddons = ProductAddonViewModel.addonsCart;
                            foreach (var item in productAddons)
                            {
                                CartProductAddonDetail obj = new CartProductAddonDetail();
                                obj.AddonCategory = item.category_id;
                                obj.AddonId = item.id;
                                obj.AddonName = item.name;
                                obj.AddonPrice = item.price;
                                obj.AddonQty = item.qty;
                               // obj.note = item.qty;
                                obj.PSQLId = objLastProduct.ID.ToString();
                                obj.productId = productId;
                                obj.image = item.image;
                                App.Database.SaveCartProductAddon(obj);
                            }
                            ProductAddonViewModel.addonsCart.Clear();
                            App.Current.MainPage.Navigation.PushAsync(new CartPage());

                        }
                    }
                    else
                    {
                        var cn = await App.Current.MainPage.DisplayAlert("Flower&Candy", AppResources.loginfirst, AppResources.yes, AppResources.no);
                        if (cn)
                        {
                            App.Current.MainPage = new NavigationPage(new LoginPage());
                        }
                    }
                });
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
