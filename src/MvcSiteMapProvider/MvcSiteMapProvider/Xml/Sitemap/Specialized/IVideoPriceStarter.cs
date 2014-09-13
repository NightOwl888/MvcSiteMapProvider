using System;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface IVideoPriceStarter
    {
        IVideoPriceOptionalValueBuilder WithAmount(double price, string currencyCode);

        IVideoPriceOptionalValueBuilder WithAmount(decimal price, string currencyCode);
    }
}
