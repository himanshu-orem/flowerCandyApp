using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Views;
using Xamarin.Forms;

namespace FlowersAndCandyCustomer.ViewModels
{
    public class OrderReviewViewModel: INotifyPropertyChanged
    {
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

        private string _price;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Price"));
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

        public OrderReviewViewModel()
        {
            _image = "flower.png";
            _name = "Flower Shop One";
            _price = "Riyal 20";
            _totalPrice = "Riyal 103";

            _list = new ObservableCollection<ProductListingModel>();
            for (int i = 0; i < 3; i++)
            {
                _list.Add(new ProductListingModel
                {
                    Qty = "X5",
                    Name = "Rose Ribbon",
                    Price = "Riyal 20"
                });
            }
            ProductList = _list;
            _listViewHeight = _list.Count * 60;
            RaisePropertyChanged(nameof(ProductList));
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

        public Command Add_Delivery_Address_Command
        {
            get
            {
                return new Command((e) =>                  
                {
                    App.Current.MainPage.Navigation.PushAsync(new AddDeliveryAddressPage());
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
    }
}
