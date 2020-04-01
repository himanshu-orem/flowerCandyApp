using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contacts;
using FlowersAndCandyCustomer.DependencyInterface;
using FlowersAndCandyCustomer.iOS.DependencyInterface;
using FlowersAndCandyCustomer.Views;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(SendSmsWhatsApp))]
namespace FlowersAndCandyCustomer.iOS.DependencyInterface
{
    public class SendSmsWhatsApp : ISendSmsOnWhatsApp
    {
        

        public string sendMsg()
        {
            var keysToFetch = new[] { CNContactKey.GivenName, CNContactKey.FamilyName, CNContactKey.PhoneNumbers };
            NSError error;

            bool check = false;
            using (var store = new CNContactStore())
            {
                var allContainers = store.GetContainers(null, out error);
                foreach (var container in allContainers)
                {
                    try
                    {
                        using (var predicate = CNContact.GetPredicateForContactsInContainer(container.Identifier))
                        {
                            var containerResults = store.GetUnifiedContacts(predicate, keysToFetch, out error);
                            foreach (var item in containerResults)
                            {
                                var num = item.PhoneNumbers;
                                foreach (var cn in num)
                                {
                                    string mob = cn.Value.StringValue;
                                    if (mob == ProfileContentView.phone)
                                    {
                                        check = true;
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {

                    }
                }
            }
            if (check == false)
            {
                var store = new CNContactStore();
                var contact = new CNMutableContact();
                var cellPhone = new CNLabeledValue<CNPhoneNumber>(CNLabelPhoneNumberKey.Mobile, new CNPhoneNumber(ProfileContentView.phone));
                var phoneNumber = new[] { cellPhone };
                contact.PhoneNumbers = phoneNumber;
                contact.GivenName = ProfileContentView.name;
                var saveRequest = new CNSaveRequest();
                saveRequest.AddContact(contact, store.DefaultContainerIdentifier);
            }
            return "1";
        }

    }
}