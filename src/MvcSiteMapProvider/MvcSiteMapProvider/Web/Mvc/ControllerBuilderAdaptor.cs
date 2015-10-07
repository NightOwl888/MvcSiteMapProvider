#if !MVC6
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcSiteMapProvider.Web.Mvc
{
    /// <summary>
    /// Adaptor class so test doubles can be injected for <see cref="T:System.Web.Mvc.ControllerBuilder"/>.
    /// </summary>
    [Obsolete("Please use the ControllerBuilderAdapter (spelled with an e) instead. This class will be removed in version 5.")]
    public class ControllerBuilderAdaptor
        : IControllerBuilder
    {
        public ControllerBuilderAdaptor(
            ControllerBuilder controllerBuilder
            ) 
        {
            if (controllerBuilder == null)
                throw new ArgumentNullException("controllerBuilder");

            this.controllerBuilder = controllerBuilder;
        }

        protected readonly ControllerBuilder controllerBuilder;

        #region IControllerBuilder Members

        public HashSet<string> DefaultNamespaces
        {
            get { return controllerBuilder.DefaultNamespaces; }
        }

        public IControllerFactory GetControllerFactory()
        {
            return controllerBuilder.GetControllerFactory();
        }

        public void SetControllerFactory(Type controllerFactoryType)
        {
            controllerBuilder.SetControllerFactory(controllerFactoryType);
        }

        public void SetControllerFactory(IControllerFactory controllerFactory)
        {
            controllerBuilder.SetControllerFactory(controllerFactory);
        }

        #endregion
    }
}
#endif
