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
using Plugin.Media;
using System.IO;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.Resources;
using System.Net.Http;
using FlowersAndCandyCustomer.SellerViews;
using Plugin.ImageResizer;
using FlowersAndCandyCustomer.DependencyInterface;
using Plugin.Media.Abstractions;

namespace FlowersAndCandyCustomer.ViewModels
{

    public class AddAddonsViewModel : INotifyPropertyChanged
    {

        public static ImageSource imageSource = "";
        public static string productId = "";

        public static byte[] mysfile;

        public static List<addAddons> addonsList = new List<addAddons>();

        private bool _isBusy;
        private const int PageSize = 1;
        readonly SellerAddonsService _dataService = new SellerAddonsService();
        public static int rowCount = 0;
        public InfiniteScrollCollection<addAddons> Items { get; }
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }


        public AddAddonsViewModel()
        {
            is_error = "";

            Items = new InfiniteScrollCollection<addAddons>
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


        public Command addMoreButton
        {
            get
            {
                return new Command((e) =>
                {

                    var item = (addAddons)e;

                    var cn = Items.OrderByDescending(x => x.id).FirstOrDefault();
                    int index = Items.Count();
                    Items.Insert(index, new addAddons { acdcImg = "active_ic.png", acdcLbl = AppResources.show, AddonsId = "", id = cn.id + 1, addButton = "False", deleteButton = "True", imgSource = "icon_add_file.png" });
                });
            }
        }


        public Command deleteAddonsButton
        {
            get
            {
                return new Command(async (e) =>
                {
                    var item = (addAddons)e;

                    if (!string.IsNullOrEmpty(item.AddonsId))
                    {
                        try
                        {


                            if (!CommonLib.checkconnection())
                            {

                                await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(AppResources._connection));
                                await Task.Delay(1000);
                                ShowMessage.CloseAllPopup();
                                return;
                            }


                            await App.Current.MainPage.Navigation.PushPopupAsync(new Loader());

                            LoggedInUser objUser = App.Database.GetLoggedInUser();

                            string postData = "user_id=" + objUser.userId + "&id=" + item.AddonsId + "&product_id=" + productId;
                            var result = await CommonLib.Login(CommonLib.ws_MainUrl + "deleteAddon?" + postData);
                            if (result.status == 1)
                            {


                                Loader.CloseAllPopup();

                                int index = Items.IndexOf(item);

                                Items.RemoveAt(index);
                                if (index == 0)
                                {
                                  
                                    Items.Insert(0, new addAddons { acdcImg = "active_ic.png", acdcLbl = AppResources.show, AddonsId = "", id = 1, addButton = "True", deleteButton = "True", imgSource = "icon_add_file.png" });
                                }


                            }
                            else
                            {
                                Loader.CloseAllPopup();

                                if (App.Lng == "ar-AE")
                                {
                                    await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(result.msg_ar));

                                }
                                else
                                {
                                    await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(result.msg_en));

                                }
                                await Task.Delay(1000);
                                ShowMessage.CloseAllPopup();
                            }

                        }
                        catch (Exception ex)
                        {
                            Loader.CloseAllPopup();
                        }

                    }
                    else
                    {
                        int index = Items.IndexOf(item);
                        Items.RemoveAt(index);
                        if (index == 0)
                        {
                            Items.Insert(0, new addAddons { acdcImg = "active_ic.png", acdcLbl = AppResources.show, AddonsId = "", id = 1, addButton = "True", deleteButton = "True", imgSource = "icon_add_file.png" });
                        }
                    }


                });
            }
        }

        public Command UploadAddonImg_Tapped
        {
            get
            {
                return new Command(async (e) =>
                {

                    var item = (addAddons)e;
                    int index = Items.IndexOf(item);

                    try
                    {

                        mysfile = null;

                        var action = await App.Current.MainPage.DisplayActionSheet(AppResources.choose_image, AppResources.cancel, null,
                        AppResources.take_picture, AppResources.choose_from_gallery);

                        string _selectedColor = string.Empty;
                        if (action == AppResources.take_picture)
                        {
                            //await CrossMedia.Current.Initialize();
                            //if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsPickPhotoSupported)
                            //{
                            //    App.Current.MainPage.DisplayAlert("No Camera", "No camera avaialble.", "OK");
                            //    return;
                            //}
                            //if (Device.RuntimePlatform == "iOS")
                            //{
                            //    await Task.Delay(1000);
                            //}
                            //var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                            //{
                            //    Directory = "Sample",
                            //    Name = "test.jpg"
                            //});
                            //if (file == null)
                            //    return;
                            //using (var memoryStream = new MemoryStream())
                            //{

                            //    file.GetStream().CopyTo(memoryStream);
                            //    var myfile = memoryStream.ToArray();

                            //    mysfile = await CrossImageResizer.Current.ResizeImageWithAspectRatioAsync(myfile, 500, 500);


                            //    file.Dispose();
                            //}
                            //imageSource = ImageSource.FromStream(() => new MemoryStream(mysfile));
                            await CrossMedia.Current.Initialize();
                            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                            {
                                return;
                            }
                            if (Device.RuntimePlatform == "iOS")
                            {
                                await Task.Delay(1000);
                            }
                            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                            {
                                SaveToAlbum = true,
                                DefaultCamera = CameraDevice.Rear,
                                Directory = "Sample",
                                Name = "test.jpg"
                            });

                            if (file == null)
                                return;
                            var cropedBytes = await CrossXMethod.Current.CropImageFromOriginalToBytes(file.Path);

                            if (cropedBytes != null)
                                imageSource = ImageSource.FromStream(() =>
                                {
                                    var cropedImage = new MemoryStream(cropedBytes);
                                    var myfile = cropedImage.ToArray();
                                    mysfile = myfile;

                                    var _imgData = Items[index];
                                    _imgData.img = mysfile;

                                    file.Dispose();
                                    return cropedImage;
                                });
                            else
                            {
                                file.Dispose();
                            }



                            Items.RemoveAt(index);
                            Items.Insert(index, new addAddons { priceS = item.priceS, acdcLbl = item.acdcLbl, acdcImg = item.acdcImg, AddonsId = item.AddonsId, id = item.id, addButton = item.id == 1 ? "True" : "False", deleteButton = item.id == 1 ? "True" : "True", imgSource = imageSource, name = item.name, priceL = item.priceL, priceM = item.priceM, qty = item.qty, img = mysfile });



                        }
                        if (action == AppResources.choose_from_gallery)
                        {
                            //await CrossMedia.Current.Initialize();
                            //if (!CrossMedia.Current.IsPickPhotoSupported)
                            //{
                            //    App.Current.MainPage.DisplayAlert("", "Image not support", "OK");
                            //    return;
                            //}
                            //var file = await CrossMedia.Current.PickPhotoAsync();
                            //if (file != null)
                            //{
                            //    using (var memoryStream = new MemoryStream())
                            //    {

                            //        file.GetStream().CopyTo(memoryStream);
                            //        var myfile = memoryStream.ToArray();

                            //        mysfile = await CrossImageResizer.Current.ResizeImageWithAspectRatioAsync(myfile, 500, 500);


                            //        file.Dispose();
                            //    }
                            //    imageSource= ImageSource.FromStream(() => new MemoryStream(mysfile));

                            await CrossMedia.Current.Initialize();
                            if (!CrossMedia.Current.IsPickPhotoSupported)
                            {
                                return;
                            }
                            var file = await CrossMedia.Current.PickPhotoAsync();

                            if (file != null)
                            {

                                var cropedBytes = await CrossXMethod.Current.CropImageFromOriginalToBytes(file.Path);

                                if (cropedBytes != null)

                                    imageSource = ImageSource.FromStream(() =>
                                    {
                                        var cropedImage = new MemoryStream(cropedBytes);
                                        var myfile = cropedImage.ToArray();
                                        mysfile = myfile;

                                        var _imgData = Items[index];
                                        _imgData.img = mysfile;

                                        file.Dispose();
                                        return cropedImage;
                                    });
                                else
                                {
                                    file.Dispose();
                                }

                                Items.RemoveAt(index);



                                Items.Insert(index, new addAddons { acdcLbl = item.acdcLbl, acdcImg = item.acdcImg, AddonsId = item.AddonsId, id = item.id, addButton = item.id == 1 ? "True" : "False", deleteButton = item.id == 1 ? "True" : "True", imgSource = imageSource, name = item.name, priceL = item.priceL, priceM = item.priceM, priceS = item.priceS, qty = item.qty, img = mysfile });


                            }
                        }


                    }
                    catch (Exception ex)
                    {

                    }


                });
            }
        }



        public static string is_error = "";
        public Command saveButton
        {
            get
            {
                return new Command(async (e) =>
                {

                   // string is_error = "";
                    await App.Current.MainPage.Navigation.PushPopupAsync(new Loader());
                    foreach (var item in Items)
                    {
                        if (string.IsNullOrEmpty(item.name))
                        {
                            is_error = "1";
                            Loader.CloseAllPopup1();
                            await App.Current.MainPage.DisplayAlert("", AppResources.enterProductName, "OK");
                            return;
                        }
                        if (string.IsNullOrEmpty(item.priceL) && string.IsNullOrEmpty(item.priceM) && string.IsNullOrEmpty(item.priceS))
                        {
                            is_error = "1";
                            Loader.CloseAllPopup1();
                            await App.Current.MainPage.DisplayAlert("", AppResources.enterProducrPrice, "OK");
                            return;
                        }
                        if (string.IsNullOrEmpty(item.qty))
                        {
                            is_error = "1";
                            Loader.CloseAllPopup1();
                            await App.Current.MainPage.DisplayAlert("", AppResources.enterProductQuantity, "OK");
                            return;
                        }

                        if (!string.IsNullOrEmpty(item.name) && !string.IsNullOrEmpty(item.qty) && (!string.IsNullOrEmpty(item.priceL) || !string.IsNullOrEmpty(item.priceM) || !string.IsNullOrEmpty(item.priceS)))
                        {
                            try
                            {
                                //edit
                                if (!string.IsNullOrEmpty(item.AddonsId))
                                {
                                    is_error = "";
                                }

                                LoggedInUser obj = App.Database.GetLoggedInUser();

                                string activeDactive = item.acdcImg == "active_ic.png" ? "1" : "0";

                                string postData = "user_id=" + obj.userId + "&name=" + item.name + "&priceL=" + item.priceL + "&priceM=" + item.priceM + "&priceS=" + item.priceS + "&description=" + "&quantity=" + item.qty + "&product_id=" + productId + "&is_error=" + is_error + "&id=" + item.AddonsId + "&status=" + activeDactive;

                                HttpClient httpClient = new HttpClient();
                                string boundary = "---8d0f01e6b3b5dafaaadaad";
                                MultipartFormDataContent multipartContent = new MultipartFormDataContent(boundary);

                                try
                                {
                                    
                                    if (item.img != null)
                                    {
                                        item.img = await CrossImageResizer.Current.ResizeImageWithAspectRatioAsync(item.img, 500, 500);

                                        var fileContent = new ByteArrayContent(item.img);
                                        fileContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/octet-stream");
                                        fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                                        {
                                            Name = "images[]",
                                            FileName = "userimage" + ".png",
                                        };
                                        multipartContent.Add(fileContent);
                                    }
                                }
                                catch { }
                                HttpResponseMessage response = await httpClient.PostAsync(CommonLib.ws_MainUrl + "addAddon?" + postData, multipartContent);
                                if (response.IsSuccessStatusCode)
                                {
                                    string content = await response.Content.ReadAsStringAsync();
                                    if (content != null)
                                    {
                                        item.img = null;

                                        SellerAddonDataResult objData = new SellerAddonDataResult();
                                        objData = Newtonsoft.Json.JsonConvert.DeserializeObject<SellerAddonDataResult>(content);

                                        if (objData.status == 1)
                                        {

                                            is_error = "";
                                        }
                                        else
                                        {

                                            is_error = "1";
                                            if (App.Lng == "ar-AE")
                                            {
                                                await App.Current.MainPage.DisplayAlert("", objData.msg_ar, "OK");

                                            }
                                            else
                                            {
                                                await App.Current.MainPage.DisplayAlert("", objData.msg_en, "OK");

                                            }

                                        }
                                    }
                                }
                                else
                                {

                                    is_error = "1";
                                    await App.Current.MainPage.DisplayAlert("", "Try again", "OK");
                                }



                            }
                            catch (Exception ex)
                            {
                                is_error = "1";
                            }
                        }

                    }
                    Loader.CloseAllPopup1();

                    App.Current.MainPage = new NavigationPage(new HomeMasterPage());

                });
            }
        }



        public Command AcDc_Tapped
        {
            get
            {
                return new Command(async (e) =>
                {

                    var item = (addAddons)e;
                    int index = Items.IndexOf(item);



                    try
                    {



                        await App.Current.MainPage.Navigation.PushPopupAsync(new Loader());

                        var _imgData = Items[index];

                        if (!string.IsNullOrEmpty(_imgData.AddonsId))
                        {


                            string postData = "id=" + _imgData.AddonsId;
                            var result = await CommonLib.Login(CommonLib.ws_MainUrl + "addonStatusUpdate?" + postData);
                            if (result.status == 1)
                            {

                                Loader.CloseAllPopup();



                                _imgData.acdcImg = _imgData.acdcImg == "active_ic.png" ? "deactive_ic.png" : "active_ic.png";
                                _imgData.acdcLbl = _imgData.acdcLbl == AppResources.show ? AppResources.hide : AppResources.show;
                                var imgSource = item.imgSource;


                                Items.RemoveAt(index);
                                Items.Insert(index, new addAddons { acdcImg = item.acdcImg, acdcLbl = item.acdcLbl, priceS = item.priceS, AddonsId = item.AddonsId, id = item.id, addButton = item.id == 1 ? "True" : "False", deleteButton = item.id == 1 ? "True" : "True", imgSource = item.imgSource, name = item.name, priceL = item.priceL, priceM = item.priceM, qty = item.qty, img = mysfile });


                            }
                            else
                            {
                                Loader.CloseAllPopup();
                                if (App.Lng == "ar-AE")
                                {
                                    await App.Current.MainPage.DisplayAlert("", result.msg_ar, "OK");

                                }
                                else
                                {
                                    await App.Current.MainPage.DisplayAlert("", result.msg_en, "OK");

                                }
                            }
                        }
                        else
                        {
                            Loader.CloseAllPopup();
                            _imgData.acdcImg = _imgData.acdcImg == "active_ic.png" ? "deactive_ic.png" : "active_ic.png";
                            _imgData.acdcLbl = _imgData.acdcLbl == AppResources.show ? AppResources.hide : AppResources.show;
                            var imgSource = item.imgSource;


                            Items.RemoveAt(index);
                            Items.Insert(index, new addAddons { acdcImg = item.acdcImg, acdcLbl = item.acdcLbl, priceS = item.priceS, AddonsId = item.AddonsId, id = item.id, addButton = item.id == 1 ? "True" : "False", deleteButton = item.id == 1 ? "True" : "True", imgSource = item.imgSource, name = item.name, priceL = item.priceL, priceM = item.priceM, qty = item.qty, img = mysfile });


                        }
                    }
                    catch (Exception ex)
                    {
                        Loader.CloseAllPopup();
                    }


                   


                });
            }
        }
    }
}
