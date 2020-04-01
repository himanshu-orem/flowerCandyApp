using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using FlowersAndCandyCustomer.Models;

namespace FlowersAndCandyCustomer.ViewModels
{





    public class CartViewModel : ObservableCollection<CartProduct>, INotifyPropertyChanged
    {



        public string Title { get; set; }
        public string Qty { get; set; }
        public string Price { get; set; }
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string CategoryId { get; set; }
        public string ProductImage { get; set; }

        public string SqlId { get; set; }


        public CartViewModel(string title,string qty,string price,string id,string productId,string categoryId,string productImage,string sqlId)
        {
            Title = title;
            Qty = qty;
            Price = price;
            Id = id;
            ProductId = productId;
            CategoryId = categoryId;
            ProductImage = productImage;
            SqlId = sqlId;
        }

        public static ObservableCollection<CartViewModel> All { private set; get; }

        static CartViewModel()
        {
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
