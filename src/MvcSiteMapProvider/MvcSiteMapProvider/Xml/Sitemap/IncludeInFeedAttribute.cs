using System;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class IncludeInFeedAttribute
        : FeedPriorityAttribute
    {
        public IncludeInFeedAttribute(string feedName)
            : this(feedName, 0)
        {
        }

        public IncludeInFeedAttribute(string feedName, int feedPriority)
            : base(feedPriority)
        {
            if (string.IsNullOrEmpty(feedName))
                throw new ArgumentNullException("feedName");
            this.FeedName = feedName;
        }

        public string FeedName { get; private set; }
    }
}
