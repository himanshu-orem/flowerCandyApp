using FlowersAndCandyCustomer.Models;
using FlowersAndCandyCustomer.Repository;
using FlowersAndCandyCustomer.Resources;
using FlowersAndCandyCustomer.Views;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlowersAndCandyCustomer.ViewModels
{
    public class SavedCardsViewModel : INotifyPropertyChanged
    {
        public INavigation _navigation;
        public LoggedInUser objUser;
        public ObservableCollection<SavedCardModel> _cardlst;
        private ObservableCollection<SavedCardModel> _SavedCardList;
        public ObservableCollection<SavedCardModel> SavedCardList
        {
            get
            {
                return _SavedCardList;

            }
            set
            {
                _SavedCardList = value;
                OnPropertyChanged();
            }
        }

        public SavedCardsViewModel(INavigation navigation)
        {
            _navigation = navigation;
            loadcards();
        }

        private async void loadcards()
        {
            try
            {

                await App.Current.MainPage.Navigation.PushPopupAsync(new Loader());
                objUser = App.Database.GetLoggedInUser();

                CardResponse cardResponse;
                CardgatewayResponse CardgatewayResponse;
                string postData = "user_id=" + objUser.userId;
                cardResponse = await CommonLib.GetCards(CommonLib.ws_MainUrl + "cardList?" + postData);
                if (cardResponse.status == 1)
                {
                    _cardlst = new ObservableCollection<SavedCardModel>();
                    foreach (var item in cardResponse.data)
                    {
                        if (!string.IsNullOrEmpty(item.ParentOrder.gateway_response))
                        {
                            CardgatewayResponse = JsonConvert.DeserializeObject<CardgatewayResponse>(item.ParentOrder.gateway_response);
                            if (CardgatewayResponse != null)
                            {

                                _cardlst.Add(new SavedCardModel()
                                {
                                    card_type = CardgatewayResponse.paymentBrand,
                                    card_registrationsid = CardgatewayResponse.registrationId,
                                    card_expiry_month = CardgatewayResponse.card.expiryMonth,
                                    card_expiry_year = CardgatewayResponse.card.expiryYear,
                                    card_holder_name = CardgatewayResponse.card.holder,
                                    card_number = string.Format("**** **** **** {0}", CardgatewayResponse.card.last4Digits),
                                    card_type_image = CardgatewayResponse.paymentBrand.ToLower() == "visa" ? "ic_visa.png" : CardgatewayResponse.paymentBrand.ToLower() == "mastercard" ? "ic_master.png" : CardgatewayResponse.paymentBrand.ToLower() == "mada" ? "ic_mada.png" : CardgatewayResponse.paymentBrand.Contains("american") ? "ic_american.png" : "",
                                    card_expiry_value = string.Format("{0}/{1}", CardgatewayResponse.card.expiryMonth, CardgatewayResponse.card.expiryYear)
                                });
                            }
                        }
                    }
                    SavedCardList = _cardlst;

                    RaisePropertyChanged(nameof(SavedCardList));

                }
                Loader.CloseAllPopup();
            }
            catch (Exception ex)
            {
                Loader.CloseAllPopup();
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

        public Command DeleteCardCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    try
                    {
                        var ans = await App.Current.MainPage.DisplayAlert("", AppResources.delete_card, AppResources.yes, AppResources.no);
                        if (ans)
                        {
                            await _navigation.PushPopupAsync(new Loader());
                            var data = e as SavedCardModel;
                            string postData = "user_id=" + objUser.userId + "&registrationsid=" + data.card_registrationsid;
                            var result = await CommonLib.DeleteCards(CommonLib.ws_MainUrl + "deleteCard?" + postData);
                            await _navigation.PopPopupAsync();
                            if (result.status == 1)
                            {
                                SavedCardList.Remove(data);

                                RaisePropertyChanged(nameof(SavedCardList));
                            }
                            else
                            {
                                if (App.Lng == "ar-AE")
                                {
                                    await _navigation.PushPopupAsync(new ShowMessage(result.msg_ar));

                                }
                                else
                                {
                                    await _navigation.PushPopupAsync(new ShowMessage(result.msg_en));

                                }
                                await Task.Delay(1000);
                                await _navigation.PopPopupAsync();
                                //ShowMessage.CloseAllPopup();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await _navigation.PopPopupAsync();
                    }
                });
            }
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
