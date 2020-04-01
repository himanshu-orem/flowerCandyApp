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

    public class ProductAddonViewModel : INotifyPropertyChanged
    {

       public static List<Addon> addonsCart = new List<Addon>();

        private bool _isBusy;
        private const int PageSize = 1;
        readonly ProductAddonService _dataService = new ProductAddonService();
        public static int rowCount = 0;
        public InfiniteScrollCollection<Addon> Items { get; }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public ProductAddonViewModel(string productId,string catId, string userId)
        {

            Items = new InfiniteScrollCollection<Addon>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    var page = Items.Count / PageSize;

                    var items = await _dataService.GetItemsAsync(page, PageSize, productId, catId, userId);

                    IsBusy = false;

                    // return the items that need to be added
                    return items;
                },
                OnCanLoadMore = () =>
                {
                    return Items.Count < rowCount;
                }
            };

            DownloadDataAsync(productId,catId, userId);
        }

        private async Task DownloadDataAsync(string productId, string catId, string userId)
        {
            var items = await _dataService.GetItemsAsync(pageIndex: 0, pageSize: PageSize, productId: productId, catId: catId, userId: userId);

            Items.AddRange(items);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public Command CheckboxCommond
        {
            get
            {
                return new Command((e) =>
                {

                   





                    var item = (Addon)e;

                    if (!string.IsNullOrEmpty(item.id) && item.qty != "0")
                    {
                        App.check = false;
                        var index = Items.ToList().FindIndex(x => x.id == item.id);
                        Items.RemoveAt(index);
                        string check = item.checkbox == "ico_check.png" ? "ico_checked.png" : "ico_check.png";

                        if (check == "ico_checked.png")
                        {
                            double iPrice = Convert.ToDouble(item.price);
                            double qt = Convert.ToDouble(item.qty);
                            double hPrice = Convert.ToDouble(AddOrderPage.price);

                            double prc = (iPrice * qt) + hPrice;

                            AddOrderPage.price = prc.ToString();
                            AddOrderPage homePage = new AddOrderPage();
                            MessagingCenter.Send(
                                prc.ToString(), "test");
                        }
                        else
                        {
                            double iPrice = Convert.ToDouble(item.price);
                            double qt = Convert.ToDouble(item.qty);
                            double hPrice = Convert.ToDouble(AddOrderPage.price);

                            double prc = hPrice - (iPrice * qt);
                            AddOrderPage.price = prc.ToString();
                            AddOrderPage homePage = new AddOrderPage();
                            MessagingCenter.Send(
                                prc.ToString(), "test");
                        }

                        Items.Insert(index, new Addon { image = item.image, checkbox = check, qty = item.qty, category_id = item.category_id, created = item.created, id = item.id, modified = item.modified, name = item.name, price = item.price, quantity = item.quantity, user_id = item.user_id });


                        //
                        if (check == "ico_checked.png") { 
                        var getaddons = addonsCart.Where(x => x.id == item.id).FirstOrDefault();
                        if (getaddons == null)
                        {
                            addonsCart.Add(new Addon { image = item.image, checkbox = check, qty = item.qty, category_id = item.category_id, created = item.created, id = item.id, modified = item.modified, name = item.name, price = item.price, quantity = item.quantity, user_id = item.user_id });
                        }
                        else
                        {
                            addonsCart.Remove(getaddons);
                            addonsCart.Add(new Addon { image = item.image, checkbox = check, qty = item.qty, category_id = item.category_id, created = item.created, id = item.id, modified = item.modified, name = item.name, price = item.price, quantity = item.quantity, user_id = item.user_id });
                        }
                    }
                        //


                    }

                });
            }
        }

        public Command btnPCommond
        {
            get
            {
                return new Command((e) =>
                {
                    var item = (Addon)e;
                    if (!string.IsNullOrEmpty(item.id))
                    {
                        var index = Items.ToList().FindIndex(x => x.id == item.id);
                       
                        if (Convert.ToInt32(item.qty) <= Convert.ToInt32(item.quantity))
                        {
                            App.check = false;
                            int qt = Convert.ToInt32(item.qty) + 1;
                            if (item.checkbox == "ico_checked.png")
                            {
                                
                                double qtPlus = Convert.ToDouble(item.price);
                                double hPrice = Convert.ToDouble(AddOrderPage.price);

                                double prc = hPrice+ qtPlus;

                                AddOrderPage.price = prc.ToString();
                                AddOrderPage homePage = new AddOrderPage();
                                MessagingCenter.Send(
                                    prc.ToString(), "test");
                            }






                         
                            Items.RemoveAt(index);
                            Items.Insert(index, new Addon { image = item.image, checkbox = item.checkbox, qty = qt.ToString(), category_id = item.category_id, created = item.created, id = item.id, modified = item.modified, name = item.name, price = item.price, quantity = item.quantity, user_id = item.user_id });
                        
                        
                        
                            //
                             
                                var getaddons = addonsCart.Where(x => x.id == item.id).FirstOrDefault();
                                if (getaddons == null)
                                {
                                addonsCart.Add(new Addon { image = item.image, checkbox = item.checkbox, qty = qt.ToString(), category_id = item.category_id, created = item.created, id = item.id, modified = item.modified, name = item.name, price = item.price, quantity = item.quantity, user_id = item.user_id});
                                }
                                else
                                {
                                    addonsCart.Remove(getaddons);
                                addonsCart.Add(new Addon { image = item.image, checkbox = item.checkbox, qty = qt.ToString(), category_id = item.category_id, created = item.created, id = item.id, modified = item.modified, name = item.name, price = item.price, quantity = item.quantity, user_id = item.user_id });
                                }
                             
                            //
                        
                        
                        
                        }
                    }

                });
            }
        }
        public Command btnMCommond
        {
            get
            {
                return new Command((e) =>
                {
                    var item = (Addon)e;
                    if (!string.IsNullOrEmpty(item.id))
                    {
                        if (item.qty != "1")
                        {
                            App.check = false;
                            var index = Items.ToList().FindIndex(x => x.id == item.id);
                            Items.RemoveAt(index);
                            int qt = Convert.ToInt32(item.qty) - 1;

                            if (item.checkbox == "ico_checked.png")
                            {

                                double qtPlus = Convert.ToDouble(item.price);
                                double hPrice = Convert.ToDouble(AddOrderPage.price);

                                double prc = hPrice - qtPlus;

                                AddOrderPage.price = prc.ToString();
                                AddOrderPage homePage = new AddOrderPage();
                                MessagingCenter.Send(
                                    prc.ToString(), "test");
                            }


                            Items.Insert(index, new Addon { image = item.image, checkbox = qt==0? "ico_check.png" : item.checkbox, qty = qt.ToString(), category_id = item.category_id, created = item.created, id = item.id, modified = item.modified, name = item.name, price = item.price, quantity = item.quantity, user_id = item.user_id });
                       
                        
                        
                            //

                            var getaddons = addonsCart.Where(x => x.id == item.id).FirstOrDefault();
                            if (getaddons == null)
                            {
                                addonsCart.Add(new Addon { image = item.image, checkbox = qt == 0 ? "ico_check.png" : item.checkbox, qty = qt.ToString(), category_id = item.category_id, created = item.created, id = item.id, modified = item.modified, name = item.name, price = item.price, quantity = item.quantity, user_id = item.user_id});
                            }
                            else
                            {
                                addonsCart.Remove(getaddons);
                                addonsCart.Add(new Addon { image = item.image, checkbox = qt == 0 ? "ico_check.png" : item.checkbox, qty = qt.ToString(), category_id = item.category_id, created = item.created, id = item.id, modified = item.modified, name = item.name, price = item.price, quantity = item.quantity, user_id = item.user_id});
                            }

                            //
                        
                        
                        }
                        
                    }

                });
            }
        }



    }
}
