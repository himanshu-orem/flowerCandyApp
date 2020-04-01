using FlowersAndCandyCustomer.DependencyInterface;
using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.Resources;
using FlowersAndCandyCustomer.Views;
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
using Xamarin.Forms.Xaml;

namespace FlowersAndCandyCustomer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNotePage : ContentPage
    {
        public static List<Notes> notes = new List<Notes>();
        public static string id = "";
        public static string NoteFor = "0";
        public static byte[] mysfile;
        public AddNotePage()
        {

            InitializeComponent();
            mysfile = null;
            NavigationPage.SetHasBackButton(this, false);
            NoteFor = "0";
            load();
            if(Device.OS==TargetPlatform.iOS){
                doneBtn.BorderRadius = 20;
            }
           
        }
        private async void UploadImg_Tapped(object sender, EventArgs e)
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
                    //     mysfile = myfile;

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
                    //         mysfile = myfile;


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
        private void seller_Tapped(object sender, EventArgs e)
        {

            sellerImg.Source = "ico_round_checked.png";
            driverImg.Source = "ico_round_check.png";
            NoteFor = "0";
        }
        private void driver_Tapped(object sender, EventArgs e)
        {

            sellerImg.Source = "ico_round_check.png";
            driverImg.Source = "ico_round_checked.png";
            NoteFor = "1";
        }

        private void back_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void DoneBtn_Clicked(object sender, EventArgs e)
        {

            if (notes.Count > 0)
            {
                var result = notes.Where(x => x.id == id).FirstOrDefault();
                if (result != null)
                {
                    result.noteFor = txtNote.Text;
                    result.noteFor = NoteFor;
                    result.noteImg = mysfile;
                }
                else
                {
                    notes.Add(new Notes { id = id, noteFor = NoteFor, noteImg = mysfile, noteTxt = txtNote.Text });
                }
            }
            else
            {
                notes.Add(new Notes { id = id, noteFor = NoteFor, noteImg = mysfile, noteTxt = txtNote.Text });
            }
            Navigation.PopAsync();
        }
        public void load()
        {
            if (notes.Count > 0)
            {
                var result = notes.Where(x => x.id == id).FirstOrDefault();
                if (result != null)
                {
                    txtNote.Text = result.noteTxt;
                    userImage.Source = ImageSource.FromStream(() => new MemoryStream(result.noteImg));
                    if (result.noteFor == "0")
                    {
                        sellerImg.Source = "ico_round_checked.png"; driverImg.Source = "ico_round_check.png"; NoteFor = "0";
                    }
                    else
                    {
                        driverImg.Source = "ico_round_checked.png"; sellerImg.Source = "ico_round_check.png"; NoteFor = "1";
                    }
                }
            }
        }
    }
}