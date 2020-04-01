using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.Resources;
using FlowersAndCandyCustomer.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlowersAndCandyCustomer.ViewModels
{
    public class TermsConditionsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public INavigation _navigation;

        private string _termsConditionsText;
        public string TermsConditionsText
        {
            get
            {
                return _termsConditionsText;
            }
            set
            {
                _termsConditionsText = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TermsConditionsText"));
            }
        }

        public Command BackImgTapped
        {
            get
            {
                return new Command((e) =>
                {
                    App.Current.MainPage.Navigation.PopAsync();
                });
            }
        }

        public TermsConditionsViewModel(INavigation navigation)
        {
            _navigation = navigation;
            load();
            //  _termsConditionsText = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum." + Environment.NewLine + Environment.NewLine +
            // "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.The point of using Lorem Ipsum is that it has a more - or - less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy.Various versions have evolved over the years, sometimes by accident, sometimes on purpose(injected humour and the like).";
        }

        public async void load()
        {
            try
            {


                if (!CommonLib.checkconnection())
                {

                    await _navigation.PushPopupAsync(new ShowMessage(AppResources._connection));
                    await Task.Delay(1000);
                    await _navigation.PopPopupAsync();
                    return;
                }


                await App.Current.MainPage.Navigation.PushPopupAsync(new Loader());



                string postData = "type=3";
                var result = await CommonLib.PageTxt(CommonLib.ws_MainUrl + "page?" + postData);
                if (result.status == 1)
                {
                    TermsConditionsText = "<html><body> <p>" + result.data.content + "</p></body></html> ";

                    Loader.CloseAllPopup();



                }
                else
                {
                    Loader.CloseAllPopup();


                }

            }
            catch (Exception ex)
            {
                Loader.CloseAllPopup();
            }
        }
    }
}
