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

namespace FlowersAndCandyCustomer.ViewModels
{
   
    public class CustomerOrdersViewModel : INotifyPropertyChanged
    {
        private bool _isBusy;
        private const int PageSize = 1;
        readonly CustomerOrder _dataService = new CustomerOrder();
        public static int rowCount = 0;
        public InfiniteScrollCollection<CustomerOrdersCls> Items { get; }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public CustomerOrdersViewModel(string type)
        {

            Items = new InfiniteScrollCollection<CustomerOrdersCls>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    var page = Items.Count / PageSize;

                    var items = await _dataService.GetItemsAsync(page, PageSize, type);

                    IsBusy = false;

                    // return the items that need to be added
                    return items;
                },
                OnCanLoadMore = () =>
                {
                    return Items.Count < rowCount;
                }
            };

            DownloadDataAsync(type);
        }

        private async Task DownloadDataAsync(string type)
        {
            var items = await _dataService.GetItemsAsync(pageIndex: 0, pageSize: PageSize, type: type);

            Items.AddRange(items);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
