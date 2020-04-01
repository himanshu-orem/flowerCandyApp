using System;
using System.Collections.Generic;
using System.Text;

namespace FlowersAndCandyCustomer.DependencyInterface
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}
