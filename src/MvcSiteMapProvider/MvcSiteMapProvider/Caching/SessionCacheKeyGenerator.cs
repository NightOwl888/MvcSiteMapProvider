using System;
using MvcSiteMapProvider.Web.Mvc;

namespace MvcSiteMapProvider.Caching
{
    /// <summary>
    /// Default implementation of ISessionCacheKeyGenerator. Creates a key for each user
    /// of the site that can be used to access the session cache for that user.
    /// </summary>
    /// <remarks>
    /// This class relies on ASP.NET session state to be enabled in the web.config file and
    /// the machine key to be set when using out of process session state. See the following
    /// link for information on how to configure a machine key.
    /// 
    /// http://msdn.microsoft.com/en-us/library/ff649308.aspx
    /// </remarks>
    public class SessionCacheKeyGenerator
        : ISessionCacheKeyGenerator
    {
        public SessionCacheKeyGenerator(
            IMvcContextFactory contextFactory
            )
        {
            if (contextFactory == null)
                throw new ArgumentNullException("contextFactory");
            this.contextFactory = contextFactory;
        }
        protected readonly IMvcContextFactory contextFactory;

        #region ISessionCacheKeyGenerator Members

        public string GenerateKey()
        {
            var context = contextFactory.CreateHttpContext();

            if (context.Session == null)
                throw new MvcSiteMapException(Resources.Messages.SessionStateRequired);

            var key = "__MvcSiteMapProvider_SessionID__";
            var result = context.Session[key];
            if (result == null)
            {
                result = Guid.NewGuid().ToString();
                context.Session[key] = result;
            }
            return (string)result;
        }

        #endregion
    }
}
