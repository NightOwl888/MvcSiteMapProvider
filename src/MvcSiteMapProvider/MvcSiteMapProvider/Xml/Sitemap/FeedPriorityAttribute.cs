using System;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class FeedPriorityAttribute
        : Attribute
    {
        public const int DefaultPriority = 0;

        public FeedPriorityAttribute(int priority)
        {
            this.Priority = priority;
        }

        public int Priority { get; private set; }
    }
}
