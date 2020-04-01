using System;
using System.Threading;
using FlowersAndCandyCustomer.DependencyInterface;
using FlowersAndCandyCustomer.iOS.DependencyInterface;
using FlowersAndCandyCustomer.Models;
using Foundation;
using Xamarin.Forms;

[assembly: Dependency(typeof(Locale_iOS))]
namespace FlowersAndCandyCustomer.iOS.DependencyInterface
{
    public class Locale_iOS : ILocale
    {
        public void SetLocale()
        {
            try
            {
                var iosLocaleAuto = NSLocale.AutoUpdatingCurrentLocale.LocaleIdentifier;
                var netLocale = iosLocaleAuto.Replace("_", "-");
                var ci = new System.Globalization.CultureInfo(netLocale);
                Thread.CurrentThread.CurrentCulture = ci;
                Thread.CurrentThread.CurrentUICulture = ci;
            }
            catch
            {
            }
        }
        public string GetCurrent()
        {
            var iosLocaleAuto = NSLocale.AutoUpdatingCurrentLocale.LocaleIdentifier;
            var iosLanguageAuto = NSLocale.AutoUpdatingCurrentLocale.LanguageCode;

            var netLocale = iosLocaleAuto.Replace("_", "-");
            var netLanguage = iosLanguageAuto.Replace("_", "-");

            try
            {
                //var sqlLiteResult = App.Database.GetLng();
                //if (sqlLiteResult != null)
                //{
                //    netLanguage = sqlLiteResult.Language;
                //    netLocale = sqlLiteResult.Language;
                //}
                //else
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

            #region Debugging Info


            Console.WriteLine("nslocaleid:" + iosLocaleAuto);
            Console.WriteLine("nslanguage:" + iosLanguageAuto);
            Console.WriteLine("ios:" + iosLanguageAuto + " " + iosLocaleAuto);
            Console.WriteLine("net:" + netLanguage + " " + netLocale);

            var ci = new System.Globalization.CultureInfo(netLocale);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            Console.WriteLine("thread:  " + Thread.CurrentThread.CurrentCulture);
            Console.WriteLine("threadui:" + Thread.CurrentThread.CurrentUICulture);
            #endregion

            return netLanguage;
        }
    }
}
