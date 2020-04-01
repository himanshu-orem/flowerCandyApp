using System;
using Xamarin.Forms;

namespace FlowersAndCandyCustomer.Repository
{
    public class App_FontFamilies
    {

        /// <summary>
        /// for page titles
        /// </summary>
        /// <value>The page titles.</value>
        public static string PageTitles
        {
            get
            {
                if (Device.RuntimePlatform == "iOS")
                {
                    return "CALIBRIB";
                }
                else
                {
                    return "CALIBRIB";
                }


            }

        }

        /// <summary>
        /// PageDiscription 
        /// </summary>
        /// <value>The page titles.</value>


        public static string PageTextRegular
        {
            get
            {
                if (Device.RuntimePlatform == "iOS")
                {
                    return "CALIBRI";
                }
                else
                {
                    return "CALIBRI";
                }


            }

        }

    }
}
