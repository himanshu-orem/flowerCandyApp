using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.Resources;
using FlowersAndCandyCustomer.ViewModels;
using FlowersAndCandyCustomer.Views;
using Plugin.ImageResizer;
using Plugin.Media;
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

namespace FlowersAndCandyCustomer.SellerViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditProduct : ContentPage
	{
        public static int is_offer = 0;
        public static string id = "";
        public static byte[] mysfile;
        public EditProduct ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, true);
            NavigationPage.SetHasBackButton(this, false);
            load();
            if (Device.OS == TargetPlatform.iOS)
            {
                addBtn.BorderRadius = 20;
            }
        }
        private void back_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        public string CheckValidations()
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(productNameTxt.Text))
            {
                msg += AppResources.enterproductcategoryname + Environment.NewLine;
            }
            //if (string.IsNullOrEmpty(productPriceTxt.Text))
            //{
            //    msg += AppResources.enterProducrPrice + Environment.NewLine;
            //}
            //if (string.IsNullOrEmpty(productquantityTxt.Text))
            //{
            //    msg += AppResources.enterProductQuantity + Environment.NewLine;
            //}
            //if (string.IsNullOrEmpty(productDetailTxt.Text))
            //{
            //    msg += AppResources.enterProductDetail + Environment.NewLine;
            //}

            return msg;
        }
        private async void AddBtn_Clicked(object sender, EventArgs e)
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



                //string postData = "user_id=" + obj.userId + "&name=" + productNameTxt.Text + "&price=" + productPriceTxt.Text + "&description=" + productDetailTxt.Text + "&quantity=" + productquantityTxt.Text + "&id=" + id + "&prepare_time=" + producttimeTxt.Text + "&is_offer=" + is_offer;
                string postData = "user_id=" + obj.userId + "&name=" + productNameTxt.Text + "&id=" + id;

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
                            Name = "images[]",
                            FileName = "userimage" + ".png",
                        };
                        multipartContent.Add(fileContent);
                    }
                }
                catch { }
                string url = CommonLib.ws_MainUrl + "addProduct?" + postData;
                HttpResponseMessage response = await httpClient.PostAsync(url, multipartContent);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (content != null)
                    {
                        mysfile = null;

                        AddProductResult objData = new AddProductResult();
                        objData = Newtonsoft.Json.JsonConvert.DeserializeObject<AddProductResult>(content);

                        if (objData.status == 1)
                        {

                            Loader.CloseAllPopup();

                            AddAddonsViewModel.productId = objData.data.Product.id;

                            // await DisplayAlert("", objData.msg_ar, "OK");

                            await App.Current.MainPage.Navigation.PushAsync(new AddAddonsPage());


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


                    await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage("Upload 1 MB Image"));
                    await Task.Delay(1000);
                    ShowMessage.CloseAllPopup();
                }



            }
            catch (Exception ex)
            {
                Loader.CloseAllPopup();
            }

        }


        //private async void UploadImg_Tapped(object sender, EventArgs e)
        //{


        //    try
        //    {



        //        var action = await App.Current.MainPage.DisplayActionSheet(AppResources.choose_image, AppResources.cancel, null,
        //                     AppResources.take_picture, AppResources.choose_from_gallery);
        //        string _selectedColor = string.Empty;
        //        if (action == AppResources.take_picture)
        //        {
        //            await CrossMedia.Current.Initialize();
        //            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsPickPhotoSupported)
        //            {
        //                App.Current.MainPage.DisplayAlert("No Camera", "No camera avaialble.", "OK");
        //                return;
        //            }
        //            if (Device.RuntimePlatform == "iOS")
        //            {
        //                await Task.Delay(1000);
        //            }
        //            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
        //            {
        //                Directory = "Sample",
        //                Name = "test.jpg"
        //            });
        //            if (file == null)
        //                return;
        //            using (var memoryStream = new MemoryStream())
        //            {

        //                file.GetStream().CopyTo(memoryStream);
        //                var myfile = memoryStream.ToArray();
        //                mysfile = myfile;

        //                mysfile = await CrossImageResizer.Current.ResizeImageWithAspectRatioAsync(myfile, 500, 500);

        //                file.Dispose();
        //            }
        //            ProductImg4.Source = ImageSource.FromStream(() => new MemoryStream(mysfile));
        //        }
        //        if (action == AppResources.choose_from_gallery)
        //        {
        //            await CrossMedia.Current.Initialize();
        //            if (!CrossMedia.Current.IsPickPhotoSupported)
        //            {
        //                await DisplayAlert("", "Image not support", "OK");
        //                return;
        //            }
        //            var file = await CrossMedia.Current.PickPhotoAsync();
        //            if (file != null)
        //            {
        //                using (var memoryStream = new MemoryStream())
        //                {

        //                    file.GetStream().CopyTo(memoryStream);
        //                    var myfile = memoryStream.ToArray();
        //                    mysfile = myfile;
        //                    mysfile = await CrossImageResizer.Current.ResizeImageWithAspectRatioAsync(myfile, 500, 500);


        //                    file.Dispose();
        //                }
        //                ProductImg4.Source = ImageSource.FromStream(() => new MemoryStream(mysfile));

        //            }
        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //    }


        //    // imgLyt.Children.Clear();

        //    //foreach (var item in uploadProductImgs)
        //    //{
        //    //    Image image = new Image()
        //    //    {
        //    //        Source = item.img,
        //    //        Aspect = Aspect.Fill
        //    //    };



        //    //    Grid grid = new Grid()
        //    //    {
        //    //        RowDefinitions =
        //    //    {
        //    //        new RowDefinition { Height = 80 },
        //    //    },
        //    //        ColumnDefinitions =
        //    //    {
        //    //        new ColumnDefinition { Width = 80 },
        //    //    } 
        //    //    };

        //    //    grid.Children.Add(new Frame
        //    //    {
        //    //        HasShadow = false,Padding=0,
        //    //        CornerRadius = 4,
        //    //        Content = image
        //    //        }

        //    //        ,0,0);
        //    //    grid.Children.Add(new Image
        //    //    {
        //    //        Source = "delete_address.png", HeightRequest = 25, WidthRequest = 25,Margin=new Thickness(-28,-55,20,0)
        //    //    }

        //    //       , 1, 0);



        //    //    imgLyt.Children.Add(grid);

        //    //}
        //    //imgLyt.Children.Remove(imgGrid4);
        //    //imgLyt.Children.Add(imgGrid4);





        //}

        //private void Offer_Tapped(object sender, EventArgs e)
        //{
        //    string path = offers.Source.ToString(); ;
        //    if (path == "File: ico_check.png")
        //    {
        //        offers.Source = "ico_checked.png";
        //        is_offer = 1;
        //    }
        //    else
        //    {
        //        offers.Source = "ico_check.png";
        //        is_offer = 0;
        //    }
        //}
        public async void load()
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

                string postData = "id=" + id;
                var result = await CommonLib.EditProductDetails(CommonLib.ws_MainUrl + "productDetail?" + postData);
                if (result.status == 1)
                {
                    //is_offer =Convert.ToInt32(result.data.product.Product.is_offer);
                    //if (is_offer == 1)
                    //{
                    //    offers.Source = "ico_checked.png";
                    //}
                    //else
                    //{
                    //    offers.Source = "ico_check.png";
                    //}

                    productNameTxt.Text = result.data.product.Product.name;
                    //productPriceTxt.Text = result.data.product.Product.price;
                    //productquantityTxt.Text = result.data.product.Product.quantity;
                    //productDetailTxt.Text = result.data.product.Product.description;
                    //producttimeTxt.Text = result.data.product.Product.prepare_time;


                    //string image = "";
                    //if (result.data.product.ProductMedia.Count == 0) { image = "product_Placeholder.png"; }
                    //foreach (var img in result.data.product.ProductMedia)
                    //{
                    //    image = string.IsNullOrEmpty(img.media) ? "product_Placeholder.png" : CommonLib.img_MainUrl + img.media; break;
                    //}
                    //ProductImg4.Source = image;



                    Loader.CloseAllPopup();



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
    }
}