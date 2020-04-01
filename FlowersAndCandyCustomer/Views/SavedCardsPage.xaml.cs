using FlowersAndCandyCustomer.ViewModels;
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
    public partial class SavedCardsPage : ContentPage
    {
        public SavedCardsPage()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);

            this.BindingContext = new SavedCardsViewModel(Navigation);
        }
    }
}