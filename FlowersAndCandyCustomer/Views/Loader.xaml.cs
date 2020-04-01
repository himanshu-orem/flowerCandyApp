using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlowersAndCandyCustomer.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Loader : PopupPage
    {
		public Loader ()
		{
			InitializeComponent ();
		}
        public static async void CloseAllPopup()
        {
            await App.Current.MainPage.Navigation.PopPopupAsync(false);
        }
        public static async void CloseAllPopup1()
        {
            await App.Current.MainPage.Navigation.PopAllPopupAsync(false);
        }
    }
}