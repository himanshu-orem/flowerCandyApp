using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Provider;
using Android.Widget;
using FlowersAndCandyCustomer.DependencyInterface;
using FlowersAndCandyCustomer.Droid.DependencyInterface;
using FlowersAndCandyCustomer.Views;
using Java.Net;
using Xamarin.Forms;
 

[assembly: Dependency(typeof(SendSmsWhatsApp))]
namespace FlowersAndCandyCustomer.Droid.DependencyInterface
{

    public class SendSmsWhatsApp : ISendSmsOnWhatsApp
    {
        public string sendMsg()
        {
            string data = "1";
            try
            {
                bool check = false;
                using (var phones = Android.App.Application.Context.ContentResolver.Query(ContactsContract.CommonDataKinds.Phone.ContentUri, null, null, null, null))
                {

                    if (phones != null)
                    {
                        while (phones.MoveToNext())
                        {
                            try
                            {
                                // string name = phones.GetString(phones.GetColumnIndex(ContactsContract.Contacts.InterfaceConsts.DisplayName));
                                string phoneNumber = phones.GetString(phones.GetColumnIndex(ContactsContract.CommonDataKinds.Phone.Number));
                                if (phoneNumber == ProfileContentView.phone)
                                {
                                    check = true;
                                }
                            }
                            catch (Exception ex)
                            {
                                data = "0";
                            }
                        }

                        phones.Close();
                    }
                    if (check == false)
                    {
                        var activity = Forms.Context as Activity;
                        var intent = new Intent(Intent.ActionInsert);
                        intent.SetType(ContactsContract.Contacts.ContentType);
                        intent.PutExtra(ContactsContract.Intents.Insert.Name, ProfileContentView.name);
                        intent.PutExtra(ContactsContract.Intents.Insert.Phone, ProfileContentView.phone);
                        activity.StartActivity(intent);
                    }

                }
            }
            catch (Exception ex)
            {
                data = "0";
            }
            return data;
        }
    }

}
