using System;
using System.ComponentModel;

namespace FlowersAndCandyCustomer.Models
{
    public class ProductListingModel: INotifyPropertyChanged
    {
        public string Image { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public string Qty { get; set; }

        public string Description { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
