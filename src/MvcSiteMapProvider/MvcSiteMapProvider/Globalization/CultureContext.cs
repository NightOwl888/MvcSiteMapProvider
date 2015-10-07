using System;
using System.Globalization;
#if !MVC6
using System.Threading;
#endif
using MvcSiteMapProvider.DI;

namespace MvcSiteMapProvider.Globalization
{
    /// <summary>
    /// Allows switching the current thread to a new culture in a using block that will automatically 
    /// return the culture to its previous state upon completion.
    /// </summary>
    [ExcludeFromAutoRegistration]
    public class CultureContext
        : ICultureContext
    {
        public CultureContext(
            CultureInfo culture,
            CultureInfo uiCulture
            )
        {
            if (culture == null)
                throw new ArgumentNullException("culture");
            if (uiCulture == null)
                throw new ArgumentNullException("uiCulture");

#if MVC6
            // Sample taken from here: https://github.com/DamianEdwards/i18nStarterWeb/blob/master/src/Microsoft.AspNet.Localization/RequestLocalizationMiddleware.cs

            // Record the current culture settings so they can be restored later.
            this.originalCulture = CultureInfo.CurrentCulture;
            this.originalUICulture = CultureInfo.CurrentUICulture;

            // Set both the culture and UI culture for this context.
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = uiCulture;
#else
            this.currentThread = Thread.CurrentThread;

            // Record the current culture settings so they can be restored later.
            this.originalCulture = this.currentThread.CurrentCulture;
            this.originalUICulture = this.currentThread.CurrentUICulture;

            // Set both the culture and UI culture for this context.
            this.currentThread.CurrentCulture = culture;
            this.currentThread.CurrentUICulture = uiCulture;
#endif
        }

#if !MVC6
        private readonly Thread currentThread;
#endif
        private readonly CultureInfo originalCulture;
        private readonly CultureInfo originalUICulture;

        public CultureInfo OriginalCulture
        {
            get { return this.originalCulture; }
        }

        public CultureInfo OriginalUICulture
        {
            get { return this.originalUICulture; }
        }

        public void Dispose()
        {
#if MVC6
            // Restore the culture to the way it was before the constructor was called.
            CultureInfo.CurrentCulture = this.originalCulture;
            CultureInfo.CurrentUICulture = this.originalUICulture;
#else
            // Restore the culture to the way it was before the constructor was called.
            this.currentThread.CurrentCulture = this.originalCulture;
            this.currentThread.CurrentUICulture = this.originalUICulture;
#endif
        }
    }
}
