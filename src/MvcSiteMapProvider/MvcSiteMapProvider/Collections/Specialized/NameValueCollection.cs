#if MVC6
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace MvcSiteMapProvider.Collections.Specialized
{
    public class NameValueCollection : List<KeyValuePair<string, IList<string>>>
    {
        private IReadableStringCollection query;

        public NameValueCollection()
            :base()
        { }

        public NameValueCollection(int capacity)
            : base(capacity)
        { }

        public NameValueCollection(IEnumerable<KeyValuePair<string, IList<string>>> collection)
            : base(collection)
        { }

        public NameValueCollection(IEnumerable<KeyValuePair<string, string[]>> collection)
            : base(collection.ToDictionary(pair => pair.Key, pair => (IList<string>)new List<string>(pair.Value)).ToArray())
        { }

        private IList<string> GetList(string name)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Key == name)
                {
                    return this[i].Value;
                }
            }
            return null;
        }

        public string[] AllKeys
        {
            get
            {
                return this.Select(x => x.Key).ToArray();
            }
        }

        public void Add(string name, string value)
        {
            var list = GetList(name);
            if (list != null)
            {
                list.Add(value);
            }
            else
            {
                this.Add(new KeyValuePair<string, IList<string>>(name, new List<string>() { value }));
            }
        }

        public bool Contains(string name)
        {
            return GetList(name) != null;
        }

        public virtual string[] GetValues(string name)
        {
            var list = GetList(name);
            if (list != null)
            {
                return list.ToArray();
            }
            return new string[0];
        }

        public void Remove(string name)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Key == name)
                {
                    this.RemoveAt(i);
                }
            }
        }

        public string this[string name]
        {
            get
            {
                var list = GetList(name);
                if (list != null)
                {
                    return GetAsOneString(list);
                }
                return string.Empty;
            }
        }

        private static string GetAsOneString(IEnumerable<string> list)
        {
            int num = (list != null) ? list.Count() : 0;
            if (num == 1)
            {
                return (string)list.ElementAt(0);
            }
            if (num <= 1)
            {
                return null;
            }
            StringBuilder builder = new StringBuilder((string)list.ElementAt(0));
            for (int i = 1; i < num; i++)
            {
                builder.Append(',');
                builder.Append((string)list.ElementAt(i));
            }
            return builder.ToString();
        }
    }
}
#endif
