using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace MvcSiteMapProvider.Collections.Specialized
{
    /// <summary>
    /// Specialized dictionary for storing metadata about a specific instance of a MvcSiteMapProvider Html Helper.
    /// </summary>
    public class SourceMetadataDictionary 
        : IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
    {
        private Dictionary<string, object> _dictionary;

        public SourceMetadataDictionary()
        {
            this._dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        }

        public SourceMetadataDictionary(IDictionary<string, object> dictionary)
        {
            this._dictionary = new Dictionary<string, object>(dictionary, StringComparer.OrdinalIgnoreCase);
        }

        public SourceMetadataDictionary(object values)
        {
            this._dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            this.AddValues(values);
        }

        public void Add(string key, object value)
        {
            this._dictionary.Add(key, value);
        }

        private void AddValues(object values)
        {
            if (values != null)
            {
#if MVC6
                var type = values.GetType();
                var allProperties = type.GetRuntimeProperties();

                // This is done to support 'new' properties that hide a property on a base class
                var orderedByDeclaringType = allProperties.OrderBy(p => p.DeclaringType == type ? 0 : 1);
                foreach (var property in orderedByDeclaringType)
                {
                    if (property.GetMethod != null &&
                        property.GetMethod.IsPublic &&
                        !property.GetMethod.IsStatic &&
                        property.GetIndexParameters().Length == 0)
                    {
                        var value = property.GetValue(values);
                        if (ContainsKey(property.Name) && property.DeclaringType != type)
                        {
                            // This is a hidden property, ignore it.
                        }
                        else
                        {
                            Add(property.Name, value);
                        }
                    }
                }
#else
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(values))
                {
                    object value = descriptor.GetValue(values);
                    this.Add(descriptor.Name, value);
                }
#endif
            }
        }

        public void Clear()
        {
            this._dictionary.Clear();
        }

        public bool ContainsKey(string key)
        {
            return this._dictionary.ContainsKey(key);
        }

        public bool ContainsValue(object value)
        {
            return this._dictionary.ContainsValue(value);
        }

        public Dictionary<string, object>.Enumerator GetEnumerator()
        {
            return this._dictionary.GetEnumerator();
        }

        public bool Remove(string key)
        {
            return this._dictionary.Remove(key);
        }

        void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
        {
            ((ICollection<KeyValuePair<string, object>>)this._dictionary).Add(item);
        }

        bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
        {
            return this._dictionary.Contains(item);
        }

        void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<string, object>>)this._dictionary).CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
        {
            return ((ICollection<KeyValuePair<string, object>>)this._dictionary).Remove(item);
        }

        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public bool TryGetValue(string key, out object value)
        {
            return this._dictionary.TryGetValue(key, out value);
        }

        public int Count
        {
            get
            {
                return this._dictionary.Count;
            }
        }

        public object this[string key]
        {
            get
            {
                object obj2;
                this.TryGetValue(key, out obj2);
                return obj2;
            }
            set
            {
                this._dictionary[key] = value;
            }
        }

        public Dictionary<string, object>.KeyCollection Keys
        {
            get
            {
                return this._dictionary.Keys;
            }
        }

        bool ICollection<KeyValuePair<string, object>>.IsReadOnly
        {
            get
            {
                return ((ICollection<KeyValuePair<string, object>>)this._dictionary).IsReadOnly;
            }
        }

        ICollection<string> IDictionary<string, object>.Keys
        {
            get
            {
                return this._dictionary.Keys;
            }
        }

        ICollection<object> IDictionary<string, object>.Values
        {
            get
            {
                return this._dictionary.Values;
            }
        }

        public Dictionary<string, object>.ValueCollection Values
        {
            get
            {
                return this._dictionary.Values;
            }
        }
    }
}
