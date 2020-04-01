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

    public class ShopListViewModel : INotifyPropertyChanged
    {
        private bool _isBusy;
        private const int PageSize = 1;
        readonly ShopListService _dataService = new ShopListService();
        public static int rowCount = 0;
        public InfiniteScrollCollection<ShopList> Items { get; }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public ShopListViewModel(string search)
        {

            Items = new InfiniteScrollCollection<ShopList>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    var page = Items.Count / PageSize;

                    var items = await _dataService.GetItemsAsync(page, PageSize, search);

                    IsBusy = false;

                    // return the items that need to be added
                    return items;
                },
                OnCanLoadMore = () =>
                {
                    return Items.Count < rowCount;
                }
            };

            DownloadDataAsync(search);
        }

        private async Task DownloadDataAsync(string search)
        {
            var items = await _dataService.GetItemsAsync(pageIndex: 0, pageSize: PageSize, search: search);

            Items.AddRange(items);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}