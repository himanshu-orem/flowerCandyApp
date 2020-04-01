using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using FlowersAndCandyCustomer.DependencyInterface;
using FlowersAndCandyCustomer.Resources;
using Xamarin.Forms;

namespace FlowersAndCandyCustomer
{
    public class L10n
    {
        public static void SetLocale()
        {
            DependencyService.Get<ILocale>().SetLocale();
        }

        /// <remarks>
        /// Maybe we can cache this info rather than querying every time
        /// </remarks>
        public static string Locale()
        {
            AppResources.Culture = new CultureInfo(DependencyService.Get<ILocale>().GetCurrent());
            return DependencyService.Get<ILocale>().GetCurrent();
        }

        public static string Localize(string key, string comment)
        {

            var netLanguage = Locale();
            // Platform-specific
            ResourceManager temp = new ResourceManager("FlowersAndCandyCustomer.Resources.AppResources", typeof(L10n).GetTypeInfo().Assembly);

            string result = temp.GetString(key, new CultureInfo(netLanguage));

            return result;
        }
    }
}
