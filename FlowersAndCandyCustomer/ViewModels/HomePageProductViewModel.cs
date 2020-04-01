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
     
    public class HomePageProductViewModel : INotifyPropertyChanged
    {
        private bool _isBusy;
        private const int PageSize = 1;
        readonly HomePageProductService _dataService = new HomePageProductService();
        public static int rowCount = 0;
        public InfiniteScrollCollection<HomeProduct> Items { get; }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public HomePageProductViewModel(string catId)
        {

            Items = new InfiniteScrollCollection<HomeProduct>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    var page = Items.Count / PageSize;

                    var items = await _dataService.GetItemsAsync(page, PageSize,catId);

                    IsBusy = false;

                    // return the items that need to be added
                    return items;
                },
                OnCanLoadMore = () =>
                {
                    return Items.Count < rowCount;
                }
            };

            DownloadDataAsync(catId);
        }

        private async Task DownloadDataAsync(string catId)
        {
            var items = await _dataService.GetItemsAsync(pageIndex: 0, pageSize: PageSize, catId: catId);

            Items.AddRange(items);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
