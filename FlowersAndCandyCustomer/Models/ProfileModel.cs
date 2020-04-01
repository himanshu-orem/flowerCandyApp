using System;
using System.ComponentModel;

namespace FlowersAndCandyCustomer.Models
{
    public class ProfileModel : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Image { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
