using System;
using System.ComponentModel;

namespace FlowersAndCandyCustomer.Models
{
    public class ProductsModel: INotifyPropertyChanged
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public string OurLocation { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
