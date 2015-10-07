using System;
using System.Collections.Generic;
using MvcSiteMapProvider.Caching;
using MvcSiteMapProvider.Web.Mvc;
#if MVC6
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
#endif

// TODO: Test

namespace MvcSiteMapProvider.Web.Script.Serialization
{
    /// <summary>
    /// Specialized class to deserialize JSON into a <see cref="T:System.Collections.Generic.IDictionary{string, object}"/>. The 
    /// value is request cached so if the string has been encountered before in the current request it will not be deserialized again.
    /// </summary>
    public class JsonToDictionaryDeserializer : MvcSiteMapProvider.Web.Script.Serialization.IJsonToDictionaryDeserializer
    {
#if MVC6
        public JsonToDictionaryDeserializer(
            IMvcContextFactory mvcContextFactory
            )
        {
            if (mvcContextFactory == null)
                throw new ArgumentNullException("mvcContextFactory");

            this.requestCache = mvcContextFactory.GetRequestCache();
        }
#else
        public JsonToDictionaryDeserializer(
            IJavaScriptSerializer javaScriptSerializer,
            IMvcContextFactory mvcContextFactory
            )
        {
            if (javaScriptSerializer == null)
                throw new ArgumentNullException("javaScriptSerializer");
            if (mvcContextFactory == null)
                throw new ArgumentNullException("mvcContextFactory");

            this.javaScriptSerializer = javaScriptSerializer;
            this.requestCache = mvcContextFactory.GetRequestCache();
        }

        protected readonly IJavaScriptSerializer javaScriptSerializer;
#endif
        protected readonly IRequestCache requestCache;

        public virtual IDictionary<string, object> Deserialize(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return new Dictionary<string, object>();
            }
            
            var key = "__JsonToDictionaryDeserializer_" + json;
            var result = this.requestCache.GetValue<IDictionary<string, object>>(key);
            if (result == null)
            {
                result = this.DeserializeJson(json);
                this.requestCache.SetValue<IDictionary<string, object>>(key, result);
            }

            return result;
        }


        protected virtual IDictionary<string, object> DeserializeJson(string json)
        {
            try
            {
#if MVC6
                return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
#else
                return this.javaScriptSerializer.Deserialize<Dictionary<string, object>>(json);
#endif
            }
            catch (Exception ex)
            {
                throw new MvcSiteMapException(string.Format(Resources.Messages.JsonToDictionaryDeserializerJsonInvalid, json, ex.Message), ex);
            }
        }
    }
}
