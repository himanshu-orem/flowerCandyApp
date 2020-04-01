using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FlowersAndCandyCustomer.DependencyInterface
{
    public interface IDownloadService
    {
        void Share(ImageSource url);
    }
}
