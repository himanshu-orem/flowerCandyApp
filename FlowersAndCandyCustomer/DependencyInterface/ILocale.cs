using System;
namespace FlowersAndCandyCustomer.DependencyInterface
{
    public interface ILocale
    {
        string GetCurrent();

        void SetLocale();
    }
}
