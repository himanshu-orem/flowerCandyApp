using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.Resources;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlowersAndCandyCustomer.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PaymentPopUp : PopupPage
    {
        public static string payType = "2";
        public static string checkoitId = "ghfghfgh";
        public PaymentPopUp ()
		{
			InitializeComponent ();
            load();
            payType = "2";
        }
        public async void load()
        {
            var getLanguage = App.Database.GetLanguage();
            payment.Source = "http://flwercandy.com/flower/api/apis/hyperCheckout?checkout_id=" + checkoitId+"&lng="+getLanguage.LangKey;
            // await Navigation.PushPopupAsync(new Loader());
            // await Task.Delay(5000);
            // Loader.CloseAllPopup();
        }

        private void payment_Navigated(object sender, WebNavigatedEventArgs e)
        {

        }

        private async void Payment_Navigating(object sender, WebNavigatingEventArgs e)
        {
            try
            {
                string result = e.Url;
                string[] tokens = result.Split('=');

                if (tokens[0] == "https://flwercandy.com/flower/api/apis/hyperCallback?id")
                {
                    //notes clear
                    AddNotePage.notes.Clear();
                    App.Database.ClearProduc();
                    App.Database.ClearAddon();
                    await DisplayAlert("", "تم تقديم طلبك بنجاح", "OK");
                    CloseAllPopup();
                    App.Current.MainPage = new NavigationPage(new MainPage());
                }
            }
            catch (Exception)
            {
            }
        }
        //public string CheckValidations()
        //{
        //    string msg = string.Empty;
        //    if (string.IsNullOrEmpty(cardTxt.Text))
        //    {
        //        msg += AppResources.Entercardnumber  + Environment.NewLine;
        //    }
        //    if (string.IsNullOrEmpty(holderTxt.Text))
        //    {
        //        msg += AppResources.Enterholdername + Environment.NewLine;
        //    }
        //    if (string.IsNullOrEmpty(cvvTxt.Text))
        //    {
        //        msg += AppResources.Entercvv1 + Environment.NewLine;
        //    }


        //    return msg;
        //}
        public static async void CloseAllPopup()
        {
            await App.Current.MainPage.Navigation.PopPopupAsync(false);
        }
        //private void visa_Tapped(object sender, EventArgs e)
        //{
        //    payType = "1";
        //    visaImg.Source = "ico_round_checked.png";
        //    masterImg.Source = "ico_round_check.png";

        //}
        //private void master_Tapped(object sender, EventArgs e)
        //{
        //    payType = "2";
        //    visaImg.Source = "ico_round_check.png";
        //    masterImg.Source = "ico_round_checked.png";

        //}


        //void Handle_CardTextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        //{
        //    string _text = cardTxt.Text;
        //    if (cardTxt.Text.Length >= 16)
        //    {
        //        if (cardTxt.Text.Length > 16)
        //        {
        //            _text = _text.Remove(_text.Length - 1);  // Remove Last character
        //            cardTxt.Text = _text;

        //        }
        //    }
        //    else
        //    {
        //        cardTxt.Focus();
        //    }
        //}

        //void Handle_CvvTextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        //{
        //    string _text = cvvTxt.Text;
        //    if (cvvTxt.Text.Length >= 3)
        //    {
        //        if (cvvTxt.Text.Length > 3)
        //        {
        //            _text = _text.Remove(_text.Length - 1);  // Remove Last character
        //            cvvTxt.Text = _text;

        //        }
        //    }
        //    else
        //    {
        //        cvvTxt.Focus();
        //    }
        //}


        //public void load()
        //{
        //    List<string> expirYears = new List<string>();
        //    int crtYear = DateTime.Now.Year;
        //    for (int i = crtYear; i < crtYear + 10; i++)
        //    {

        //        expirYears.Add(i.ToString());
        //    }
        //    expireYear_picker.ItemsSource = expirYears;
        //}



        private async void Pay_Clicked(object sender, EventArgs e)
        { CloseAllPopup(); }


        //    var returnMessage = CheckValidations();
        //    if (!string.IsNullOrEmpty(returnMessage))
        //    {
        //        await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(returnMessage));
        //        await Task.Delay(1000);
        //        ShowMessage.CloseAllPopup();
        //        return;
        //    }







        //    string products = "";
        //    var _note = AddNotePage.notes;

        //    try
        //    {
        //        products = "[";
        //        var _products = App.Database.GetAllProduct();

        //        foreach (var group in _products)
        //        {
        //            products += "{";
        //            products += '"' + "id" + '"' + ":" + group.productId + ",";
        //            products += '"' + "quantity" + '"' + ":" + group.productQty + ",";
        //            products += '"' + "addon" + '"' + ":" + "[";

        //            var _addons = App.Database.GetAllProductAddon(Convert.ToString(group.ID));
        //            foreach (var food in _addons)
        //            {
        //                products += "{";
        //                products += '"' + "id" + '"' + ":" + food.AddonId + ",";
        //                products += '"' + "quantity" + '"' + ":" + food.AddonQty;
        //                products += "},";
        //            }
        //            //
        //            if (_addons.Count != 0)
        //                products = products.Substring(0, products.Length - 1);
        //            products += "]";


        //            products += "},";
        //        }
        //        //
        //        products = products.Substring(0, products.Length - 1);
        //        products += "]";


        //    }
        //    catch (Exception)
        //    {
        //    }


        //    string note_Json = "";

        //    if (AddNotePage.notes.Count > 0)
        //    {

        //        try
        //        {
        //            note_Json = "[";

        //            foreach (var cn in _note)
        //            {
        //                //id
        //                note_Json += "{";
        //                note_Json += '"' + "shop_id" + '"' + ":" + cn.id + ",";
        //                note_Json += '"' + "description" + '"' + ":" + '"' + cn.noteTxt + '"';
        //                note_Json += "},";

        //            }
        //            note_Json = note_Json.Substring(0, note_Json.Length - 1);
        //            note_Json += "]";


        //        }
        //        catch (Exception)
        //        {
        //        }




        //    }




        //    var ans = await App.Current.MainPage.DisplayAlert("", AppResources.createOrder, AppResources.yes, AppResources.no);
        //    if (ans)
        //    {




        //        try
        //        {
        //            if (!CommonLib.checkconnection())
        //            {

        //                await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(AppResources._connection));
        //                await Task.Delay(1000);
        //                ShowMessage.CloseAllPopup();
        //                return;
        //            }

        //            await App.Current.MainPage.Navigation.PushPopupAsync(new Loader());

        //            LoggedInUser objUser = App.Database.GetLoggedInUser();










        //            string postData = "user_id=" + objUser.userId + "&items=" + products + "&payment_method=2"  + "&full_address=" + DeliveryAddressPopUp.Txt + "&lat=" + DeliveryAddressPopUp.Lat1 + "&lng=" + DeliveryAddressPopUp.Lng1 + "&notes=" + note_Json + "&card_type=" + payType + "&card_no=" + cardTxt.Text.Trim() + "&card_holder=" + holderTxt.Text.Trim() + "&expiry_month=" + expireMonth_picker.SelectedItem + "&expiry_year=" + expireYear_picker.SelectedItem + "&card_cvv=" + cvvTxt.Text.Trim();


        //            HttpClient httpClient = new HttpClient();
        //            string boundary = "---8d0f01e6b3b5dafaaadaad";
        //            MultipartFormDataContent multipartContent = new MultipartFormDataContent(boundary);
        //            try
        //            {
        //                foreach (var cn in _note)
        //                {
        //                    if (cn.noteImg != null)
        //                    {
        //                        var fileContent = new ByteArrayContent(cn.noteImg);
        //                        fileContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/octet-stream");
        //                        fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
        //                        {
        //                            Name = "note_image[]",
        //                            FileName = cn.id + "_orderNote" + ".png",
        //                        };
        //                        multipartContent.Add(fileContent);
        //                    }
        //                    else
        //                    {
        //                        string fileName = "Icon";
        //                        byte[] array = Encoding.ASCII.GetBytes(fileName);
        //                        var fileContent = new ByteArrayContent(array);
        //                        fileContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/octet-stream");
        //                        fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
        //                        {
        //                            Name = "note_image[]",
        //                            FileName = cn.id + "_emptyNote" + ".png",
        //                        };
        //                        multipartContent.Add(fileContent);
        //                    }
        //                }
        //            }
        //            catch { }




        //            HttpResponseMessage response = await httpClient.PostAsync(CommonLib.ws_MainUrl + "createOrder?" + postData, multipartContent);
        //            if (response.IsSuccessStatusCode)
        //            {
        //                string content = await response.Content.ReadAsStringAsync();
        //                if (content != null)
        //                {


        //                    CreateOrderResult objData = new CreateOrderResult();
        //                    objData = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateOrderResult>(content);

        //                    if (objData.status == 1)
        //                    {

        //                        //notes clear
        //                        AddNotePage.notes.Clear();

        //                        App.Database.ClearProduc();
        //                        App.Database.ClearAddon();

        //                        Loader.CloseAllPopup();

        //                        await DisplayAlert("", objData.msg_ar, "OK");
        //                        CloseAllPopup();
        //                        App.Current.MainPage = new NavigationPage(new MainPage());


        //                    }
        //                    else
        //                    {
        //                        Loader.CloseAllPopup();

        //                        await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(objData.msg_ar));
        //                        await Task.Delay(1000);
        //                        ShowMessage.CloseAllPopup();

        //                    }
        //                }
        //            }
        //            else
        //            {
        //                Loader.CloseAllPopup();


        //                await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage(response.ReasonPhrase.ToString()));
        //                await Task.Delay(1000);
        //                ShowMessage.CloseAllPopup();
        //            }



        //        }
        //        catch (Exception ex)
        //        {
        //            Loader.CloseAllPopup();
        //        }




        //    }
        //}

    }
}