using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Views;
using Xamarin.Forms;

namespace FlowersAndCandyCustomer.ViewModels
{
    public class ProductListingViewModel: INotifyPropertyChanged
    {
        ObservableCollection<ProductListingModel> _list;

        private ObservableCollection<ProductListingModel> _productList;
        public ObservableCollection<ProductListingModel> ProductListing
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

        private ProductListingModel _productListSelected;

        public ProductListingModel ProductListSelected
        {
            get { return _productListSelected; }
            set
            {
                _productListSelected = value;
                if (_productListSelected != null)
                {
                    OnPropertyChanged();
                    ShowDetailPage();
                }
            }
        }

        public ProductListingViewModel()
        {
            _list = new ObservableCollection<ProductListingModel>();
            for (int i = 0; i < 5; i++)
            {
                _list.Add(new ProductListingModel
                {
                    Image = "flower.png",
                    Name = "Flower Shop One",
                    Price = "Riyal 20",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry."
                });
            }
            ProductListing = _list;

            RaisePropertyChanged(nameof(ProductListing));

            _shopName = "Flower Shop One";
            _location = "2.5";
        }

        void ShowDetailPage()
        {
            App.Current.MainPage.Navigation.PushAsync(new AddOrderPage());
        }

        public Command BackImgTapped
        {
            get
            {
                return new Command((e) =>
                {
                    App.Current.MainPage.Navigation.PopAsync();
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
