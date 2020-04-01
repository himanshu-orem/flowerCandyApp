using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Services;
using Xamarin.Forms.Extended;
using System.Windows.Input;
using System.Linq;
using FlowersAndCandyCustomer.Views;

namespace FlowersAndCandyCustomer.ViewModels
{
    
    public class CustomerAddressViewModel : INotifyPropertyChanged
    {

      

        private bool _isBusy;
        private const int PageSize = 1;
        readonly AddressService _dataService = new AddressService();
        public static int rowCount = 0;
        public InfiniteScrollCollection<UsersAddress> Items { get; }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public CustomerAddressViewModel()
        {

            Items = new InfiniteScrollCollection<UsersAddress>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    var page = Items.Count / PageSize;

                    var items = await _dataService.GetItemsAsync(page, PageSize);

                    IsBusy = false;

                    // return the items that need to be added
                    return items;
                },
                OnCanLoadMore = () =>
                {
                    return Items.Count < rowCount;
                }
            };

            DownloadDataAsync();
        }

        private async Task DownloadDataAsync()
        {
            var items = await _dataService.GetItemsAsync(pageIndex: 0, pageSize: PageSize);

            Items.AddRange(items);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


      
       



    }
}
