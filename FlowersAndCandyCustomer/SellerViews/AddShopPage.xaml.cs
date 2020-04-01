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
    public partial class AddShopPage : ContentPage
    {
        public static byte[] mysfileBanner;
        public static byte[] mysfile;
        public static string countryCode = "";
        public static string delivery = "0";



        public static string Latitude = string.Empty;
        public static string Longitude = string.Empty;
        public static string Address = string.Empty;


        public AddShopPage()
        {
            delivery = "0";
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
         //   getCountryCodes();
            if(Device.OS==TargetPlatform.iOS){
                doneBtn.BorderRadius = 20;
            }
            loadLatLng();
        }
        public async void loadLatLng()
        {

            try
            {
                var timeout = TimeSpan.FromSeconds(1);
                var locator = CrossGeolocator.Current;
                if (locator.IsGeolocationEnabled)
                {
                    locator.DesiredAccuracy = 50;
                    int tm = 1000;
                    if (Device.OS == TargetPlatform.Android) { tm = 100000; }
                    var position = await locator.GetPositionAsync(tm);
                    if (position != null)
                    {
                        Latitude = position.Latitude.ToString();
                        Longitude = position.Longitude.ToString();

                        Geocoder geoCoder = new Geocoder();
                        var positionAds = new Position(position.Latitude, position.Longitude);
                        var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(positionAds);
                        foreach (var address in possibleAddresses)
                        {
                            Address = address;
                            addressNumberTxt.Text = Address; break;
                        }




                       

                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        //public void getCountryCodes()
        //{

        //    try
        //    {
        //        var getList = CommonLib.LoadJson();
        //        if (getList != null)
        //        {

        //            var getList1 = getList.OrderBy(x => x.dial_code);
        //            phoneCodePicker.SelectedIndex = 0;
        //            phoneCodePicker.Title = "+966";
        //            foreach (var item in getList1)
        //            {

        //                if (item.dial_code != 0)
        //                {
        //                    phoneCodePicker.Items.Add("+" + item.dial_code);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    countryCode = phoneCodePicker.Title.Replace("+", "");
        //}
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            App.Database.ClearLoginDetails();
            App.Current.MainPage = new LoginPage();
        }
         

        //private void PhoneCodePicker_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string Code = phoneCodePicker.SelectedItem.ToString();
        //        countryCode = Code.Replace("+", "");
        //        phoneCodePicker.Title = phoneCodePicker.SelectedItem.ToString();
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}
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
        //private void imgThird_Tapped(object sender, EventArgs e)
        //{
        //    thirdLyt.IsVisible = false;
        //}
        //private void imgSecond_Tapped(object sender, EventArgs e)
        //{
        //    secondLyt.IsVisible = false;
        //}
        //private void plus_Tapped(object sender, EventArgs e)
        //{
        //    if (thirdLyt.IsVisible == true)
        //    {
        //        secondLyt.IsVisible = true;
        //    }
        //    thirdLyt.IsVisible = true;
        //}
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
            if (mysfile==null)
            {
                msg += AppResources.uploadCompanyLogo + Environment.NewLine;
            }
            if (mysfileBanner == null)
            {
                msg += AppResources.uploadCompanyBanner + Environment.NewLine;
            }

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

                string postData = "user_id=" + obj.userId + "&company_name=" + companNameTxt.Text + "&communication_number=" + phn + "&delivery_system=" + delivery + "&country_code=" + countryCode + "&license_number=" + licenceNumberTxt.Text + "&shop_address=" + addressNumberTxt.Text + "&lat=" +Latitude + "&lng=" + Longitude;

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

                            // Save data in sqlite for login check
                            LoggedInUser objUser = new LoggedInUser();
                            objUser.ID = obj.ID;
                            objUser.userId = obj.userId;
                            objUser.fname = obj.fname;
                            objUser.lname = obj.lname;
                            objUser.email = obj.email;
                            objUser.image = "";
                            objUser.phone = obj.phone;
                            objUser.userType = obj.userType;
                            objUser.country_code = obj.country_code;
                            objUser.is_shop = "1";
                            App.Database.SaveLoggedInUser(objUser);

                         
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