using System;
using System.Threading;
using FlowersAndCandyCustomer.DependencyInterface;
using FlowersAndCandyCustomer.Droid.DependencyInterface;
using FlowersAndCandyCustomer.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(Locale_Android))]
namespace FlowersAndCandyCustomer.Droid.DependencyInterface
{
    public class Locale_Android : ILocale
    {
        public void SetLocale()
        {
            try
            {
                var androidLocale = Java.Util.Locale.Default; // user's preferred locale
                var netLocale = androidLocale.ToString().Replace("_", "-");
                var ci = new System.Globalization.CultureInfo(netLocale);
                Thread.CurrentThread.CurrentCulture = ci;
                Thread.CurrentThread.CurrentUICulture = ci;
            }
            catch (Exception)
            {


            }
        }

        /// <remarks>
        /// Not sure if we can cache this info rather than querying every time
        /// </remarks>
        public string GetCurrent()
        {
            var androidLocale = Java.Util.Locale.Default; // user's preferred locale

            // en, es, ja
            var netLanguage = androidLocale.Language.Replace("_", "-");
            // en-US, es-ES, ja-JP
            var netLocale = androidLocale.ToString().Replace("_", "-");
            try
            {
                //var sqlLiteResult = App.Database.GetLng();
                //if (sqlLiteResult != null)
                //{
                //    netLanguage = sqlLiteResult.Language;
                //    netLocale = sqlLiteResult.Language;
                //}
                //else  //ar-AE
                //{
                netLanguage = "ar-AE";
                netLocale = "ar-AE";
                Language langObj = App.Database.GetLanguage();
                if (langObj != null)
                {
                    netLanguage = langObj.LangKey;
                    netLocale = langObj.LangKey;
                }
                //}
            }
            catch (Exception)
            {
            }

            #region Debugging output
            Console.WriteLine("android:  " + androidLocale.ToString());
            Console.WriteLine("netlang:  " + netLanguage);
            Console.WriteLine("netlocale:" + netLocale);

            var ci = new System.Globalization.CultureInfo(netLocale);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            Console.WriteLine("thread:  " + Thread.CurrentThread.CurrentCulture);
            Console.WriteLine("threadui:" + Thread.CurrentThread.CurrentUICulture);
            #endregion

            return netLocale;
        }
    }
}
