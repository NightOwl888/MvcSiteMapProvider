#if MVC6
using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Microsoft.AspNet.Routing;
//using System.Web.Caching;
//using System.Web.Routing;

namespace MvcSiteMapProvider.Web
{
    public abstract class HttpResponseBase
    {
        //public virtual void AddCacheDependency(params CacheDependency[] dependencies)
        //{
        //    throw new NotImplementedException();
        //}

        //public virtual void AddCacheItemDependencies(ArrayList cacheKeys)
        //{
        //    throw new NotImplementedException();
        //}

        //public virtual void AddCacheItemDependencies(string[] cacheKeys)
        //{
        //    throw new NotImplementedException();
        //}

        //public virtual void AddCacheItemDependency(string cacheKey)
        //{
        //    throw new NotImplementedException();
        //}

        //public virtual void AddFileDependencies(ArrayList filenames)
        //{
        //    throw new NotImplementedException();
        //}

        //public virtual void AddFileDependencies(string[] filenames)
        //{
        //    throw new NotImplementedException();
        //}

        //public virtual void AddFileDependency(string filename)
        //{
        //    throw new NotImplementedException();
        //}

        public virtual void AddHeader(string name, string value)
        {
            throw new NotImplementedException();
        }

        //public virtual ISubscriptionToken AddOnSendingHeaders(Action<HttpContextBase> callback)
        //{
        //    throw new NotImplementedException();
        //}

        //public virtual void AppendCookie(HttpCookie cookie)
        //{
        //    throw new NotImplementedException();
        //}

        public virtual void AppendHeader(string name, string value)
        {
            throw new NotImplementedException();
        }

        public virtual void AppendToLog(string param)
        {
            throw new NotImplementedException();
        }

        public virtual string ApplyAppPathModifier(string virtualPath)
        {
            throw new NotImplementedException();
        }

        public virtual IAsyncResult BeginFlush(AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public virtual void BinaryWrite(byte[] buffer)
        {
            throw new NotImplementedException();
        }

        public virtual void Clear()
        {
            throw new NotImplementedException();
        }

        public virtual void ClearContent()
        {
            throw new NotImplementedException();
        }

        public virtual void ClearHeaders()
        {
            throw new NotImplementedException();
        }

        public virtual void Close()
        {
            throw new NotImplementedException();
        }

        public virtual void DisableKernelCache()
        {
            throw new NotImplementedException();
        }

        public virtual void DisableUserCache()
        {
            throw new NotImplementedException();
        }

        public virtual void End()
        {
            throw new NotImplementedException();
        }

        public virtual void EndFlush(IAsyncResult asyncResult)
        {
            throw new NotImplementedException();
        }

        public virtual void Flush()
        {
            throw new NotImplementedException();
        }

        public virtual void Pics(string value)
        {
            throw new NotImplementedException();
        }

        public virtual void Redirect(string url)
        {
            throw new NotImplementedException();
        }

        public virtual void Redirect(string url, bool endResponse)
        {
            throw new NotImplementedException();
        }

        public virtual void RedirectPermanent(string url)
        {
            throw new NotImplementedException();
        }

        public virtual void RedirectPermanent(string url, bool endResponse)
        {
            throw new NotImplementedException();
        }

        public virtual void RedirectToRoute(object routeValues)
        {
            throw new NotImplementedException();
        }

        public virtual void RedirectToRoute(string routeName)
        {
            throw new NotImplementedException();
        }

        public virtual void RedirectToRoute(RouteValueDictionary routeValues)
        {
            throw new NotImplementedException();
        }

        public virtual void RedirectToRoute(string routeName, object routeValues)
        {
            throw new NotImplementedException();
        }

        public virtual void RedirectToRoute(string routeName, RouteValueDictionary routeValues)
        {
            throw new NotImplementedException();
        }

        public virtual void RedirectToRoutePermanent(object routeValues)
        {
            throw new NotImplementedException();
        }

        public virtual void RedirectToRoutePermanent(string routeName)
        {
            throw new NotImplementedException();
        }

        public virtual void RedirectToRoutePermanent(RouteValueDictionary routeValues)
        {
            throw new NotImplementedException();
        }

        public virtual void RedirectToRoutePermanent(string routeName, object routeValues)
        {
            throw new NotImplementedException();
        }

        public virtual void RedirectToRoutePermanent(string routeName, RouteValueDictionary routeValues)
        {
            throw new NotImplementedException();
        }

        public virtual void RemoveOutputCacheItem(string path)
        {
            throw new NotImplementedException();
        }

        public virtual void RemoveOutputCacheItem(string path, string providerName)
        {
            throw new NotImplementedException();
        }

        //public virtual void SetCookie(HttpCookie cookie)
        //{
        //    throw new NotImplementedException();
        //}

        public virtual void TransmitFile(string filename)
        {
            throw new NotImplementedException();
        }

        public virtual void TransmitFile(string filename, long offset, long length)
        {
            throw new NotImplementedException();
        }

        public virtual void Write(char ch)
        {
            throw new NotImplementedException();
        }

        public virtual void Write(object obj)
        {
            throw new NotImplementedException();
        }

        public virtual void Write(string s)
        {
            throw new NotImplementedException();
        }

        public virtual void Write(char[] buffer, int index, int count)
        {
            throw new NotImplementedException();
        }

        public virtual void WriteFile(string filename)
        {
            throw new NotImplementedException();
        }

        public virtual void WriteFile(string filename, bool readIntoMemory)
        {
            throw new NotImplementedException();
        }

        public virtual void WriteFile(IntPtr fileHandle, long offset, long size)
        {
            throw new NotImplementedException();
        }

        public virtual void WriteFile(string filename, long offset, long size)
        {
            throw new NotImplementedException();
        }

        //public virtual void WriteSubstitution(HttpResponseSubstitutionCallback callback)
        //{
        //    throw new NotImplementedException();
        //}

        public virtual bool Buffer
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual bool BufferOutput
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        //public virtual HttpCachePolicyBase Cache
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        public virtual string CacheControl
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual string Charset
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual CancellationToken ClientDisconnectedToken
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual Encoding ContentEncoding
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual string ContentType
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        //public virtual HttpCookieCollection Cookies
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        public virtual int Expires
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual DateTime ExpiresAbsolute
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual Stream Filter
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual Encoding HeaderEncoding
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        //public virtual NameValueCollection Headers
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public virtual bool HeadersWritten
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        public virtual bool IsClientConnected
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual bool IsRequestBeingRedirected
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual TextWriter Output
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual Stream OutputStream
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual string RedirectLocation
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual string Status
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual int StatusCode
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual string StatusDescription
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual int SubStatusCode
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual bool SupportsAsyncFlush
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual bool SuppressContent
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual bool SuppressDefaultCacheControlHeader
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual bool SuppressFormsAuthenticationRedirect
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual bool TrySkipIisCustomErrors
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
#endif