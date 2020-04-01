using FlowersAndCandyCustomer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace FlowersAndCandyCustomer.ViewModels
{
    
    public class ExpendProductViewModel : ObservableCollection<ProductPageItem>, INotifyPropertyChanged
    {



        public string Name { get; set; }
        public string Id { get; set; }


        public ExpendProductViewModel(string name, string id)
        {
            Name = name;
            Id = id;
        }

        public static ObservableCollection<ExpendProductViewModel> All { private set; get; }

        static ExpendProductViewModel()
        {
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
