using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FlowersAndCandyCustomer.DependencyInterface;
using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.Resources;
using FlowersAndCandyCustomer.SellerViews;
using FlowersAndCandyCustomer.ViewModels;
using Plugin.ImageResizer;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace FlowersAndCandyCustomer.Views
{
    public partial class EditProfilePage : ContentPage
    {
        public static byte[] mysfile;
        public static string countryCode = "";
        public static string countryISO = "";

        public EditProfilePage()
        {
            InitializeComponent();
            // loadISO();
            load();
            NavigationPage.SetHasBackButton(this, false);

            if (Device.RuntimePlatform == Device.iOS)
            {
                fNameTxt.Margin = new Thickness(0, 15, 0, 0);
                lNameTxt.Margin = new Thickness(0, 15, 0, 0);
                emailTxt.Margin = new Thickness(0, 15, 0, 0);
                phoneNumberTxt.Margin = new Thickness(0, 15, 0, 0);

                fNameLbl.Margin = new Thickness(0, 30, 0, 0);
                lNameLbl.Margin = new Thickness(0, 30, 0, 0);

                emailLbl.Margin = new Thickness(0, 30, 0, 0);
                phoneNumberLbl.Margin = new Thickness(0, 30, 0, 0);

                changePasswordBtn.BorderRadius = 20;
            }

            getCountryCodes();


        }
        public void load()
        {
            try
            {


                LoggedInUser objUser = App.Database.GetLoggedInUser();
                userImage.Source = string.IsNullOrEmpty(objUser.image) ? "user_placeholder2.png" : objUser.image;
                fNameTxt.Text = objUser.fname;
                lNameTxt.Text = objUser.lname;
                emailTxt.Text = objUser.email;
                phoneCodePicker.SelectedItem = "+" + objUser.country_code;
                phoneNumberTxt.Text = objUser.phone;
                surnameTxt.Text = objUser.surName;
                streetaddressTxt.Text = objUser.streetAddress;
                cityTxt.Text = objUser.city;
                stateTxt.Text = objUser.state;
                try
                {
                    List<CountryIso> lst = countryTxt.ItemsSource as List<CountryIso>;
                    int index = lst.FindIndex(a => a.iso == objUser.country);
                    countryTxt.SelectedIndex = index;
                }
                catch (Exception)
                {
                }
                postcodeTxt.Text = objUser.postCode;
                if (objUser.userType == "1")
                {
                    //surnameLbl.IsVisible = true;
                    //surnameTxt.IsVisible = true;
                    //streetaddressLbl.IsVisible = true;
                    //streetaddressTxt.IsVisible = true;
                    //cityLbl.IsVisible = true;
                    //cityTxt.IsVisible = true;
                    //stateLbl.IsVisible = true;
                    //stateTxt.IsVisible = true;
                    //countryLbl.IsVisible = true;
                    //countryTxt.IsVisible = true;
                    //postcodeLbl.IsVisible = true;
                    //postcodeTxt.IsVisible = true;


                }
            }
            catch (Exception)
            {
            }
        }
        public void getCountryCodes()
        {

            try
            {
                var getList = CommonLib.LoadJson();
                if (getList != null)
                {

                    var getList1 = getList.OrderBy(x => x.dial_code);
                    phoneCodePicker.SelectedIndex = 0;
                    phoneCodePicker.Title = "+966";
                    foreach (var item in getList1)
                    {
                        if (item.dial_code != 0)
                        {
                            phoneCodePicker.Items.Add("+" + item.dial_code);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            countryCode = phoneCodePicker.Title.Replace("+", "");
        }
        private void back_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        private void PhoneCodePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string Code = phoneCodePicker.SelectedItem.ToString();
                countryCode = Code.Replace("+", "");
                phoneCodePicker.Title = phoneCodePicker.SelectedItem.ToString();
            }
            catch (Exception)
            {
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

        public string CheckValidations()
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(fNameTxt.Text))
            {
                msg += AppResources.enter_first_name + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(lNameTxt.Text))
            {
                msg += AppResources.enter_last_name + Environment.NewLine;
            }

            LoggedInUser objUser = App.Database.GetLoggedInUser();

            //if (objUser.userType == "1")
            //{
            //    if (string.IsNullOrEmpty(surnameTxt.Text))
            //    {
            //        msg += AppResources.entersurname + Environment.NewLine;
            //    }
            //    if (string.IsNullOrEmpty(streetaddressTxt.Text))
            //    {
            //        msg += AppResources.enterstreetaddress + Environment.NewLine;
            //    }
            //    if (string.IsNullOrEmpty(cityTxt.Text))
            //    {
            //        msg += AppResources.enter_city + Environment.NewLine;
            //    }
            //    if (string.IsNullOrEmpty(stateTxt.Text))
            //    {
            //        msg += AppResources.enter_state + Environment.NewLine;
            //    }

            //    if (string.IsNullOrEmpty(postcodeTxt.Text))
            //    {
            //        msg += AppResources.enterpostcode + Environment.NewLine;
            //    }
            //}







            if (string.IsNullOrEmpty(emailTxt.Text))
            {
                msg += AppResources.enter_email_validation + Environment.NewLine;
            }
            if (!string.IsNullOrEmpty(emailTxt.Text))
            {
                if (!CommonLib.CheckValidEmail(emailTxt.Text))
                {
                    msg += AppResources.enter_valid_email_validation + Environment.NewLine;
                }
            }
            if (string.IsNullOrEmpty(phoneNumberTxt.Text))
            {
                msg += AppResources.enter_phone_number_validation + Environment.NewLine;
            }
            return msg;
        }
        //public async void loadISO()
        //{
        //    try
        //    {

        //var result = await CommonLib.GetISO(CommonLib.ws_MainUrl + "getCountries");
        //if (result.status == 1)
        //{

        //         countryTxt.ItemsSource = result.data.countries;
        //    countryTxt.SelectedIndex = 1;
        // load();
        // }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        private async void Btn_Clicked(object sender, EventArgs e)
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

                LoggedInUser obj = App.Database.GetLoggedInUser();

                await Navigation.PushPopupAsync(new Loader());

                string postData = "first_name=" + fNameTxt.Text + "&last_name=" + lNameTxt.Text + "&password=" + "&email=" + emailTxt.Text + "&phone=" + phoneNumberTxt.Text + "&country_code=" + phoneCodePicker.SelectedItem + "&deviceType=" + "&deviceToken=" + "&userType=" + obj.userType + "&id=" + obj.userId + "&surname=" + surnameTxt.Text + "&streetaddress=" + streetaddressTxt.Text + "&city=" + cityTxt.Text + "&state=" + stateTxt.Text + "&country=" + countryISO + "&postcode=" + postcodeTxt.Text;

                HttpClient httpClient = new HttpClient();
                string boundary = "---8d0f01e6b3b5dafaaadaad";
                MultipartFormDataContent multipartContent = new MultipartFormDataContent(boundary);

                try
                {
                    var fileContent = new ByteArrayContent(mysfile);
                    fileContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/octet-stream");
                    fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                    {
                        Name = "image",
                        FileName = "userimage" + ".png",
                    };
                    multipartContent.Add(fileContent);
                }
                catch { }
                HttpResponseMessage response = await httpClient.PostAsync(CommonLib.ws_MainUrl + "registerUser?" + postData, multipartContent);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (content != null)
                    {
                        mysfile = null;

                        UserResult objData = new UserResult();
                        objData = Newtonsoft.Json.JsonConvert.DeserializeObject<UserResult>(content);

                        if (objData.status == 1)
                        {

                            Loader.CloseAllPopup();

                            // Save data in sqlite for login check
                            LoggedInUser objUser = new LoggedInUser();
                            objUser.ID = obj.ID;
                            objUser.userId = Convert.ToInt32(objData.data.User.id);
                            objUser.fname = objData.data.User.first_name;
                            objUser.lname = objData.data.User.last_name;
                            objUser.email = objData.data.User.email;
                            objUser.image = CommonLib.img_MainUrl + objData.data.User.image;
                            objUser.phone = objData.data.User.phone;
                            objUser.userType = objData.data.User.userType;
                            objUser.country_code = objData.data.User.country_code;
                            objUser.is_shop = objData.data.User.is_shop;

                            objUser.surName = objData.data.User.surname;
                            objUser.streetAddress = objData.data.User.streetaddress;
                            objUser.city = objData.data.User.city;
                            objUser.state = objData.data.User.state;
                            objUser.country = objData.data.User.country;
                            objUser.postCode = objData.data.User.postcode;



                            App.Database.SaveLoggedInUser(objUser);

                            if (App.Lng == "ar-AE")
                            {
                                await DisplayAlert("", objData.msg_ar, "OK");

                            }
                            else
                            {
                                await DisplayAlert("", objData.msg_en, "OK");

                            }
                            if (obj.userType == "1")
                            {
                                App.Current.MainPage = new NavigationPage(new MainPage());
                            }
                            else
                            {
                                App.Current.MainPage = new NavigationPage(new HomeMasterPage());
                            }
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


                    await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage("Try again"));
                    await Task.Delay(1000);
                    ShowMessage.CloseAllPopup();
                }



            }
            catch (Exception ex)
            {
                Loader.CloseAllPopup();
            }



        }

        private void CountryTxt_SelectedIndexChanged(object sender, EventArgs e)
        {
            var result = countryTxt.SelectedItem as CountryIso;
            countryISO = result.iso;
        }
    }
}
