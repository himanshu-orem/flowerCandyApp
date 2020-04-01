using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.Resources;
using Newtonsoft.Json.Linq;
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
    public partial class CustomerPaymentPage : ContentPage
    {
        public static string customerAddress = "";
        public static string pickOrders = "";

        public static double deliveryPrice = 0;
        public static double deliveryPriceTemp = 0;
        public static double dictancePrice = 0;

        public static int radius = 0;

        public static double TotalPrice = 0;

        public static string paymentType = "1";
        public CustomerPaymentPage()
        {
            pickOrders = "";
            TotalPrice = 0;
            paymentType = "1";
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            load();
            pickerPayment.Items.Add(AppResources.cash);
            pickerPayment.Items.Add(AppResources.card);
        }

        private void back_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }





        private async void Pay_Clicked(object sender, EventArgs e)
        {
            if (paymentType == "2")
            {


                string products = "";
                var _note = AddNotePage.notes;

                try
                {
                    products = "[";
                    var _products = App.Database.GetAllProduct();

                    List<CartProductDetail> cartProductDetails = new List<CartProductDetail>();
                    cartProductDetails.Clear();
                    foreach (var group in _products)
                    {
                        var resultID = cartProductDetails.Find(x => x.baseProductId == group.baseProductId);
                        if (resultID == null)
                        {

                            products += "{";
                            products += '"' + "id" + '"' + ":" + group.baseProductId + ",";
                            products += '"' + "addon" + '"' + ":" + "[";


                            cartProductDetails.Add(new CartProductDetail { baseProductId = group.baseProductId });
                            foreach (var food in _products)
                            {
                                if (group.baseProductId == food.baseProductId)
                                {
                                    int type = 1;
                                    if (food.typePrice == "M") { type = 1; }
                                    if (food.typePrice == "L") { type = 2; }
                                    if (food.typePrice == "S") { type = 3; }
                                    products += "{";
                                    products += '"' + "id" + '"' + ":" + food.productId + ",";
                                    products += '"' + "quantity" + '"' + ":" + food.productQty + ",";
                                    products += '"' + "note" + '"' + ":" + '"' + food.note + '"' + ",";
                                    products += '"' + "type" + '"' + ":" + type;
                                    products += "},";
                                }
                            }


                            products = products.Substring(0, products.Length - 1);
                            products += "]";


                            products += "},";
                        }
                    }


                    products = products.Substring(0, products.Length - 1);
                    products += "]";


                }
                catch (Exception)
                {
                }


                string note_Json = "";

                if (AddNotePage.notes.Count > 0)
                {

                    try
                    {
                        note_Json = "[";

                        foreach (var cn in _note)
                        {
                            //id
                            note_Json += "{";
                            note_Json += '"' + "shop_id" + '"' + ":" + cn.id + ",";
                            note_Json += '"' + "description" + '"' + ":" + '"' + cn.noteTxt + '"';
                            note_Json += "},";

                        }
                        note_Json = note_Json.Substring(0, note_Json.Length - 1);
                        note_Json += "]";


                    }
                    catch (Exception)
                    {
                    }




                }




                var ans = await App.Current.MainPage.DisplayAlert("", AppResources.createOrder, AppResources.yes, AppResources.no);
                if (ans)
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










                        string postData = "user_id=" + objUser.userId + "&items=" + products + "&payment_method=" + paymentType + "&full_address=" + DeliveryAddressPopUp.Txt + "&lat=" + DeliveryAddressPopUp.Lat1 + "&lng=" + DeliveryAddressPopUp.Lng1 + "&notes=" + note_Json + "&self_pick=" + pickOrders;


                        HttpClient httpClient = new HttpClient();
                        string boundary = "---8d0f01e6b3b5dafaaadaad";
                        MultipartFormDataContent multipartContent = new MultipartFormDataContent(boundary);
                        try
                        {
                            foreach (var cn in _note)
                            {
                                if (cn.noteImg != null)
                                {
                                    var fileContent = new ByteArrayContent(cn.noteImg);
                                    fileContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/octet-stream");
                                    fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                                    {
                                        Name = "note_image[]",
                                        FileName = cn.id + "_orderNote" + ".png",
                                    };
                                    multipartContent.Add(fileContent);
                                }
                                else
                                {
                                    string fileName = "Icon";
                                    byte[] array = Encoding.ASCII.GetBytes(fileName);
                                    var fileContent = new ByteArrayContent(array);
                                    fileContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/octet-stream");
                                    fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                                    {
                                        Name = "note_image[]",
                                        FileName = cn.id + "_emptyNote" + ".png",
                                    };
                                    multipartContent.Add(fileContent);
                                }
                            }
                        }
                        catch { }




                        HttpResponseMessage response = await httpClient.PostAsync(CommonLib.ws_MainUrl + "createOrder?" + postData, multipartContent);
                        if (response.IsSuccessStatusCode)
                        {
                            string content = await response.Content.ReadAsStringAsync();
                            if (content != null)
                            {


                                CreateOrderResult objData = new CreateOrderResult();
                                objData = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateOrderResult>(content);

                                if (objData.status == 1)
                                {
                                    PaymentPopUp.checkoitId = objData.data.checkout_id;
                                    //notes clear
                                    // AddNotePage.notes.Clear();
                                    // App.Database.ClearProduc();
                                    // App.Database.ClearAddon();

                                    Loader.CloseAllPopup();


                                    PaymentPopUp Popup = new PaymentPopUp();
                                    await App.Current.MainPage.Navigation.PushPopupAsync(Popup);

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


                            await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(response.ReasonPhrase.ToString()));
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
            else
            {
                string products = "";
                var _note = AddNotePage.notes;

                try
                {
                    products = "[";
                    var _products = App.Database.GetAllProduct();

                    List<CartProductDetail> cartProductDetails = new List<CartProductDetail>();
                    cartProductDetails.Clear();
                    foreach (var group in _products)
                    {
                        var resultID = cartProductDetails.Find(x => x.baseProductId == group.baseProductId);
                        if (resultID == null)
                        {

                            products += "{";
                            products += '"' + "id" + '"' + ":" + group.baseProductId + ",";
                            //   products += '"' + "quantity" + '"' + ":" + group.productQty + ",";
                            products += '"' + "addon" + '"' + ":" + "[";
                            // var _addons = App.Database.GetAllProductAddon(Convert.ToString(group.ID));


                            cartProductDetails.Add(new CartProductDetail { baseProductId = group.baseProductId });
                            foreach (var food in _products)
                            {
                                if (group.baseProductId == food.baseProductId)
                                {
                                    int type = 1;
                                    if (food.typePrice == "M") { type = 1; }
                                    if (food.typePrice == "L") { type = 2; }
                                    if (food.typePrice == "S") { type = 3; }
                                    products += "{";
                                    products += '"' + "id" + '"' + ":" + food.productId + ",";
                                    products += '"' + "quantity" + '"' + ":" + food.productQty + ",";
                                    products += '"' + "note" + '"' + ":" + '"' + food.note + '"' + ",";
                                    products += '"' + "type" + '"' + ":" + type;
                                    products += "},";
                                }
                            }

                            //
                            //if (_addons.Count != 0)
                            products = products.Substring(0, products.Length - 1);
                            products += "]";


                            products += "},";
                        }
                    }
                    //
                    products = products.Substring(0, products.Length - 1);
                    products += "]";


                }
                catch (Exception)
                {
                }


                string note_Json = "";
                if (AddNotePage.notes.Count > 0)
                {

                    try
                    {
                        note_Json = "[";

                        foreach (var cn in _note)
                        {

                            note_Json += "{";
                            note_Json += '"' + "shop_id" + '"' + ":" + cn.id + ",";
                            note_Json += '"' + "description" + '"' + ":" + '"' + cn.noteTxt + '"';
                            note_Json += "},";

                        }
                        note_Json = note_Json.Substring(0, note_Json.Length - 1);
                        note_Json += "]";


                    }
                    catch (Exception)
                    {
                    }
                }



                var ans = await App.Current.MainPage.DisplayAlert("", AppResources.createOrder, AppResources.yes, AppResources.no);
                if (ans)
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










                        string postData = "user_id=" + objUser.userId + "&items=" + products + "&payment_method=" + paymentType + "&full_address=" + DeliveryAddressPopUp.Txt + "&lat=" + DeliveryAddressPopUp.Lat1 + "&lng=" + DeliveryAddressPopUp.Lng1 + "&notes=" + note_Json + "&self_pick=" + pickOrders;

                        string aa = postData;


                        HttpClient httpClient = new HttpClient();
                        string boundary = "---8d0f01e6b3b5dafaaadaad";
                        MultipartFormDataContent multipartContent = new MultipartFormDataContent(boundary);
                        try
                        {
                            foreach (var cn in _note)
                            {
                                if (cn.noteImg != null)
                                {
                                    var fileContent = new ByteArrayContent(cn.noteImg);
                                    fileContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/octet-stream");
                                    fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                                    {
                                        Name = "note_image[]",
                                        FileName = cn.id + "_orderNote" + ".png",
                                    };
                                    multipartContent.Add(fileContent);
                                }
                                else
                                {
                                    string fileName = "Icon";
                                    byte[] array = Encoding.ASCII.GetBytes(fileName);
                                    var fileContent = new ByteArrayContent(array);
                                    fileContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/octet-stream");
                                    fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                                    {
                                        Name = "note_image[]",
                                        FileName = cn.id + "_emptyNote" + ".png",
                                    };
                                    multipartContent.Add(fileContent);
                                }
                            }
                        }
                        catch { }

                        string aa1 = postData;

                        HttpResponseMessage response = await httpClient.PostAsync(CommonLib.ws_MainUrl + "createOrder?" + postData, multipartContent);
                        if (response.IsSuccessStatusCode)
                        {
                            string content = await response.Content.ReadAsStringAsync();
                            if (content != null)
                            {


                                CreateOrderResult objData = new CreateOrderResult();
                                objData = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateOrderResult>(content);

                                if (objData.status == 1)
                                {

                                    //notes clear
                                    AddNotePage.notes.Clear();

                                    App.Database.ClearProduc();
                                    App.Database.ClearAddon();

                                    Loader.CloseAllPopup();

                                    if (App.Lng == "ar-AE")
                                    {
                                        await DisplayAlert("", objData.msg_ar, "OK");

                                    }
                                    else
                                    {
                                        await DisplayAlert("", objData.msg_en, "OK");

                                    }

                                    App.Current.MainPage = new NavigationPage(new MainPage());


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


                            await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(response.ReasonPhrase.ToString()));
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

        public async void load()
        {
            int getshop = 0;
            int ckeck = 0;
            var _products = App.Database.GetAllProduct();


            foreach (var group in _products.OrderBy(x => x.shopId))
            {
                if (ckeck == 0)
                {
                    getshop = App.Database.GetProductShop(group.shopId);
                }
                ckeck++;

                Label name = new Label()
                {
                    Text = group.productName,
                    TextColor = Color.Gray,
                    FontSize = 14,
                    FontAttributes = FontAttributes.Bold,
                    Margin = new Thickness(0, 10, 0, 0),
                    FontFamily = "CALIBRI",
                    StyleId = "CALIBRI",
                };
                productnameLbl.Children.Add(name);
                Label qty = new Label()
                {
                    Text = "x" + group.productQty,
                    Margin = new Thickness(0, 8, 0, 0),
                    FontAttributes = FontAttributes.Bold,
                    FontFamily = "CALIBRI",
                    StyleId = "CALIBRI",
                    TextColor = Color.Gray,
                    FontSize = 14
                };
                qtylyt.Children.Add(qty);
                TotalPrice = TotalPrice + (Convert.ToDouble(group.productPrice) * Convert.ToDouble(group.productQty));
                Label price = new Label()
                {
                    Text = "Riyal " + group.productPrice,
                    Margin = new Thickness(0, 10, 0, 0),
                    FontAttributes = FontAttributes.Bold,
                    FontFamily = "CALIBRI",
                    StyleId = "CALIBRI",
                    TextColor = Color.Gray,
                    FontSize = 14
                };
                prilyt.Children.Add(price);



                var _addons = App.Database.GetAllProductAddon(Convert.ToString(group.ID));
                foreach (var food in _addons)
                {
                    Label nameA = new Label()
                    {
                        Text = food.AddonName,

                        FontFamily = "CALIBRI",
                        StyleId = "CALIBRI",
                        TextColor = Color.Gray,
                        FontSize = 14
                    };
                    productnameLbl.Children.Add(nameA);
                    Label qtyA = new Label()
                    {
                        Text = "x" + food.AddonQty,

                        FontFamily = "CALIBRI",
                        StyleId = "CALIBRI",
                        TextColor = Color.Gray,
                        FontSize = 14
                    };
                    qtylyt.Children.Add(qtyA);
                    TotalPrice = TotalPrice + (Convert.ToDouble(food.AddonPrice) * Convert.ToDouble(food.AddonQty));
                    Label priceA = new Label()
                    {
                        Text = "Riyal " + food.AddonPrice,

                        FontFamily = "CALIBRI",
                        StyleId = "CALIBRI",
                        TextColor = Color.Gray,
                        FontSize = 14
                    };
                    prilyt.Children.Add(priceA);
                }


                //
                try
                {
                    if (ckeck == getshop)
                    {
                        ckeck = 0;

                        string dic = getDistance(group.shopAddress, customerAddress);

                        double dc = Convert.ToDouble(dic);
                        if (radius < dc)
                        {
                            double datadc = (dc - radius);
                            double ss = Convert.ToDouble(datadc) * dictancePrice;
                            deliveryPrice = deliveryPrice + ss;
                            string resultval= string.Format("{0:F2}", deliveryPrice);
                            deliveryPrice = Convert.ToDouble(resultval);
                        }

                        // double dictanceCharg = (Convert.ToDouble(dic) * dictancePrice);


                        //Label pricedictance = new Label()
                        //{
                        //    Text = "Riyal " + dictanceCharg,
                        //    FontFamily = "CALIBRI",
                        //    StyleId = "CALIBRI",

                        //    TextColor = Color.FromHex("#FE1F78"),
                        //    FontSize = 14
                        //};
                        //prilyt.Children.Add(pricedictance);
                        //    Label lbldictance = new Label()
                        //    {
                        //        Text = "-",
                        //        FontFamily = "CALIBRI",
                        //        StyleId = "CALIBRI",
                        //        TextColor = Color.FromHex("#FE1F78"),
                        //        FontSize = 12
                        //    };
                        //    qtylyt.Children.Add(lbldictance);

                        //    Label namedictance = new Label()
                        //{
                        //    Text = AppResources.DictanceFee, 
                        //    FontFamily= "CALIBRI",
                        //    StyleId= "CALIBRI",
                        //    TextColor = Color.FromHex("#FE1F78"),
                        //    FontSize = 14
                        //};
                        //productnameLbl.Children.Add(namedictance);

                        TotalPrice = TotalPrice + deliveryPrice;

                        Label priceDelivery = new Label()
                        {
                            Text = "Riyal " + deliveryPrice,
                            FontFamily = "CALIBRI",
                            StyleId = "CALIBRI",
                            TextColor = Color.Gray,
                            FontSize = 14
                        };
                        prilyt.Children.Add(priceDelivery);
                        Label dqty = new Label()
                        {
                            Text = "-",
                            Margin = new Thickness(0, 8, 0, 0),
                            FontAttributes = FontAttributes.Bold,
                            FontFamily = "CALIBRI",
                            StyleId = "CALIBRI",
                            TextColor = Color.Gray,
                            FontSize = 14
                        };
                        qtylyt.Children.Add(dqty);

                        Label nameDelivery = new Label()
                        {
                            Text = AppResources.delivery_fee,
                            FontFamily = "CALIBRI",
                            StyleId = "CALIBRI",
                            TextColor = Color.Gray,
                            FontSize = 14
                        };
                        productnameLbl.Children.Add(nameDelivery);





                    }
                    totalPrice.Text = "Riyal " + TotalPrice;
                    deliveryAddress.Text = DeliveryAddressPopUp.Txt;
                }
                catch (Exception)
                {
                }
                //
                deliveryPrice = deliveryPriceTemp;
            }
        }

        private async void LblClose_tgr_Tapped(object sender, EventArgs e)
        {
            //var shopId = sender as Label;
            //AddNotePage.id = shopId.ClassId;
            //await App.Current.MainPage.Navigation.PushAsync(new AddNotePage());
        }


        public string getDistance(string source, string destination)
        {
            double distance = 0;
            //  string content = GetJsonData("https://maps.googleapis.com/maps/api/distancematrix/json?origins=" + source + "&destination=" + destination + "&sensor=false&key=AIzaSyCAW0qqikPYbOKLu_aobSw04z1Dnfhgpv4");
            string content = GetJsonData("https://maps.googleapis.com/maps/api/distancematrix/json?origins=" + destination + "&destinations="+ source + "&key=AIzaSyCyITo41DikrOebhSAHnDoCFXJ-K7uF-vs&sensor=false");


            JObject obj = JObject.Parse(content);
            try
            {
                distance = (double)obj.SelectToken("rows[0].elements[0].distance.value");
                return (distance / 1000).ToString();
            }
            catch (Exception ex)
            {

            }
            return (distance / 1000).ToString();
        }

        protected string GetJsonData(string url)
        {
            string sContents = string.Empty;
            string me = string.Empty;
            try
            {
                if (url.ToLower().IndexOf("https:") > -1)
                {
                    System.Net.WebClient client = new System.Net.WebClient();
                    byte[] response = client.DownloadData(url);
                    sContents = System.Text.Encoding.ASCII.GetString(response);
                }
                else
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(url);
                    sContents = sr.ReadToEnd();
                    sr.Close();
                }
            }
            catch
            {

            }
            return sContents;
        }

        private void changePayment_Tapped(object sender, EventArgs e)
        {
            pickerPayment.Focus();
        }

        private void PickerPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indx = pickerPayment.SelectedIndex;
            if (indx == 0)
            {
                paymentType = "1";
                payImg.Source = "cash_pay.png";
                payLbl.Text = AppResources.cash;
            }
            else
            {
                paymentType = "2";
                payImg.Source = "card_pay.png";
                payLbl.Text = AppResources.card;
            }
        }

        private void SelfToggel_Toggled(object sender, ToggledEventArgs e)
        {
            if (selfToggel.IsToggled)
            {
                pickOrders = "1";
            }
            else
            {
                pickOrders = "";
            }
        }
    }
}