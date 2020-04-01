using FlowersAndCandyCustomer.DependencyInterface;
using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.Resources;
using FlowersAndCandyCustomer.Views;
using Plugin.Geolocator;
using Plugin.ImageResizer;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace FlowersAndCandyCustomer.SellerViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditShopPage : ContentPage
	{
        public static byte[] mysfile;
        public static byte[] mysfileBanner;
        public static string countryCode = "966";
        public static string delivery = "0";

        public static string Id = "";

        public static string Latitude = string.Empty;
        public static string Longitude = string.Empty;
        public static string Address = string.Empty;
        public EditShopPage ()
		{
            delivery = "0";
            InitializeComponent();
            Title = AppResources.editShop;
           
            if (Device.OS == TargetPlatform.iOS)
            {
                doneBtn.BorderRadius = 20;
            }
            load();
        }
      public async void load()
        {
            try
            {

                await App.Current.MainPage.Navigation.PushPopupAsync(new Loader());
                LoggedInUser objUser = App.Database.GetLoggedInUser();


                string postData = "user_id=" + objUser.userId;
                var result = await CommonLib.GetShop(CommonLib.ws_MainUrl + "getShopInfo?" + postData);
                if (result.status == 1)
                {
                    Latitude = result.data.shop.Shop.latitude;
                    Longitude = result.data.shop.Shop.longitude;
                    userImage.Source = CommonLib.img_MainUrl+result.data.shop.Shop.logo;
                    bannerImage.Source = CommonLib.img_MainUrl + result.data.shop.Shop.banner;
                    addressNumberTxt.Text = result.data.shop.Shop.address;
                    licenceNumberTxt.Text = result.data.shop.Shop.license_number;
                    companNameTxt.Text = result.data.shop.Shop.company_name;
                    Address = result.data.shop.Shop.address;
                    Id = result.data.shop.Shop.id;
                }
                Loader.CloseAllPopup();
            }
            catch (Exception ex)
            {
                Loader.CloseAllPopup();
            }
        }
       
 


       
        private async void Cimg_Tapped(object sender, EventArgs e)
        {
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
                    //    mysfile = myfile;
                    //    mysfile = await CrossImageResizer.Current.ResizeImageWithAspectRatioAsync(myfile, 500, 500);

                    //    file.Dispose();
                    //}
                    //userImage.Source = ImageSource.FromStream(() => new MemoryStream(mysfile));
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
                        userImage.Source = ImageSource.FromStream(() =>
                        {
                            var cropedImage = new MemoryStream(cropedBytes);
                            var myfile = cropedImage.ToArray();
                            mysfile = myfile;
                            file.Dispose();
                            return cropedImage;
                        });
                    else
                    {
                        file.Dispose();
                    }
                }
                if (action == AppResources.choose_from_gallery)
                {
                    //await CrossMedia.Current.Initialize();
                    //if (!CrossMedia.Current.IsPickPhotoSupported)
                    //{
                    //    await DisplayAlert("", "Image not support", "OK");
                    //    return;
                    //}
                    //var file = await CrossMedia.Current.PickPhotoAsync();
                    //if (file != null)
                    //{
                    //    using (var memoryStream = new MemoryStream())
                    //    {

                    //        file.GetStream().CopyTo(memoryStream);
                    //        var myfile = memoryStream.ToArray();
                    //        mysfile = myfile;
                    //        mysfile = await CrossImageResizer.Current.ResizeImageWithAspectRatioAsync(myfile, 500, 500);


                    //        file.Dispose();
                    //    }
                    //    userImage.Source = ImageSource.FromStream(() => new MemoryStream(mysfile));

                    //}
                    await CrossMedia.Current.Initialize();
                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                        return;
                    }
                    var file = await CrossMedia.Current.PickPhotoAsync();

                    if (file == null)
                        return;

                    var cropedBytes = await CrossXMethod.Current.CropImageFromOriginalToBytes(file.Path);

                    if (cropedBytes != null)

                        userImage.Source = ImageSource.FromStream(() =>
                        {
                            var cropedImage = new MemoryStream(cropedBytes);
                            var myfile = cropedImage.ToArray();
                            mysfile = myfile;
                            file.Dispose();
                            return cropedImage;
                        });
                    else
                    {
                        file.Dispose();
                    }
                }


            }
            catch (Exception ex)
            {

            }
        }
        private async void Bimg_Tapped(object sender, EventArgs e)
        {
            try
            {

                mysfileBanner = null;

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
                    //    mysfileBanner = myfile;
                    //    mysfileBanner = await CrossImageResizer.Current.ResizeImageWithAspectRatioAsync(myfile, 500, 500);

                    //    file.Dispose();
                    //}
                    //bannerImage.Source = ImageSource.FromStream(() => new MemoryStream(mysfileBanner));
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
                        bannerImage.Source = ImageSource.FromStream(() =>
                        {
                            var cropedImage = new MemoryStream(cropedBytes);
                            var myfile = cropedImage.ToArray();
                            mysfileBanner = myfile;
                            file.Dispose();
                            return cropedImage;
                        });
                    else
                    {
                        file.Dispose();
                    }
                }
                if (action == AppResources.choose_from_gallery)
                {
                    //await CrossMedia.Current.Initialize();
                    //if (!CrossMedia.Current.IsPickPhotoSupported)
                    //{
                    //    await DisplayAlert("", "Image not support", "OK");
                    //    return;
                    //}
                    //var file = await CrossMedia.Current.PickPhotoAsync();
                    //if (file != null)
                    //{
                    //    using (var memoryStream = new MemoryStream())
                    //    {

                    //        file.GetStream().CopyTo(memoryStream);
                    //        var myfile = memoryStream.ToArray();
                    //        mysfileBanner = myfile;
                    //        mysfileBanner = await CrossImageResizer.Current.ResizeImageWithAspectRatioAsync(myfile, 500, 500);


                    //        file.Dispose();
                    //    }
                    //    bannerImage.Source = ImageSource.FromStream(() => new MemoryStream(mysfileBanner));

                    //}
                    await CrossMedia.Current.Initialize();
                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                        return;
                    }
                    var file = await CrossMedia.Current.PickPhotoAsync();

                    if (file == null)
                        return;

                    var cropedBytes = await CrossXMethod.Current.CropImageFromOriginalToBytes(file.Path);

                    if (cropedBytes != null)

                        bannerImage.Source = ImageSource.FromStream(() =>
                        {
                            var cropedImage = new MemoryStream(cropedBytes);
                            var myfile = cropedImage.ToArray();
                            mysfileBanner = myfile;
                            file.Dispose();
                            return cropedImage;
                        });
                    else
                    {
                        file.Dispose();
                    }
                }


            }
            catch (Exception ex)
            {

            }
        }

        public string CheckValidations()
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(companNameTxt.Text))
            {
                msg += AppResources.enterCompanyName + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(licenceNumberTxt.Text))
            {
                msg += AppResources.enterLicenceNumber + Environment.NewLine;
            }
            //if (string.IsNullOrEmpty(phoneNumberTxt.Text))
            //{
            //    msg += AppResources.enter_phone_number + Environment.NewLine;
            //}
            if (string.IsNullOrEmpty(addressNumberTxt.Text))
            {
                msg += AppResources.enterShopAddress + Environment.NewLine;
            }
            //if (secondLyt.IsVisible == true)
            //{
            //    if (string.IsNullOrEmpty(phoneNumber2Txt.Text))
            //    {
            //        msg += AppResources.enter_phone_number + Environment.NewLine;
            //    }
            //}
            //if (thirdLyt.IsVisible == true)
            //{
            //    if (string.IsNullOrEmpty(phoneNumber3Txt.Text))
            //    {
            //        msg += AppResources.enter_phone_number + Environment.NewLine;
            //    }
            //}

            return msg;
        }

        private async void DoneBtn_Clicked(object sender, EventArgs e)
        {
            if (!CommonLib.checkconnection())
            {

                await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(AppResources._connection));
                await Task.Delay(1000);
                ShowMessage.CloseAllPopup();
                return;
            }
            var returnMessage = CheckValidations();
            if (!string.IsNullOrEmpty(returnMessage))
            {
                await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(returnMessage));
                await Task.Delay(1000);
                ShowMessage.CloseAllPopup();
                return;
            }

            try
            {

                string phn = "[";
                phn += phn = "]";

                LoggedInUser obj = App.Database.GetLoggedInUser();

                await Navigation.PushPopupAsync(new Loader());

                string postData = "user_id=" + obj.userId + "&company_name=" + companNameTxt.Text + "&communication_number=" + phn + "&delivery_system=" + delivery + "&country_code=" + countryCode + "&license_number=" + licenceNumberTxt.Text + "&shop_address=" + addressNumberTxt.Text + "&lat=" + Latitude + "&lng=" + Longitude + "&id=" + Id;

                HttpClient httpClient = new HttpClient();
                string boundary = "---8d0f01e6b3b5dafaaadaad";
                MultipartFormDataContent multipartContent = new MultipartFormDataContent(boundary);

                try
                {
                    if (mysfile != null)
                    {
                        var fileContent = new ByteArrayContent(mysfile);
                        fileContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/octet-stream");
                        fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                        {
                            Name = "logo",
                            FileName = "logoShop" + ".png",
                        };
                        multipartContent.Add(fileContent);
                    }

                    if (mysfileBanner != null)
                    {
                        var fileContentBanner = new ByteArrayContent(mysfileBanner);
                        fileContentBanner.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/octet-stream");
                        fileContentBanner.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                        {
                            Name = "banner",
                            FileName = "bannerShop" + ".png",
                        };
                        multipartContent.Add(fileContentBanner);
                    }
                }
                catch { }
                HttpResponseMessage response = await httpClient.PostAsync(CommonLib.ws_MainUrl + "shopRegister?" + postData, multipartContent);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (content != null)
                    {
                        mysfile = null;
                        mysfileBanner = null;

                        ShopRegisterResult objData = new ShopRegisterResult();
                        objData = Newtonsoft.Json.JsonConvert.DeserializeObject<ShopRegisterResult>(content);

                        if (objData.status == 1)
                        {

                            Loader.CloseAllPopup();
 


                            if (App.Lng == "ar-AE")
                            {

                                await DisplayAlert("", objData.msg_ar, "OK");
                            }
                            else
                            {
                                await DisplayAlert("", objData.msg_en, "OK");
                            }
                            App.Current.MainPage = new NavigationPage(new HomeMasterPage());

                        }
                        else
                        {
                            Loader.CloseAllPopup();

                            if (App.Lng == "ar-AE")
                            {

                                await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(objData.msg_ar));

                            }
                            else
                            {
                                await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(objData.msg_en));

                            }
                            await Task.Delay(1000);
                            ShowMessage.CloseAllPopup();

                        }
                    }
                }
                else
                {
                    Loader.CloseAllPopup();


                    await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(response.RequestMessage.ToString()));
                    await Task.Delay(1000);
                    ShowMessage.CloseAllPopup();
                }



            }
            catch (Exception ex)
            {
                Loader.CloseAllPopup();
            }
        }

        private async void AddressNumberTxt_Focused(object sender, FocusEventArgs e)
        {

            await App.Current.MainPage.Navigation.PushAsync(new ShopAddressSearch());
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            addressNumberTxt.Text = Address;
        }
    }
}