#if !MVC6
using System;
using System.Collections;
using System.Web.Compilation;

namespace MvcSiteMapProvider.Web.Compilation
{
    /// <summary>
    /// Adaptor class so test doubles can be injected for <see cref="T:System.Web.Compilation.BuildManager"/>.
    /// </summary>
    [Obsolete("Use BuildManagerAdapter (spelled with an e) class instead. This class will be removed in version 5.")]
    public class BuildManagerAdaptor
        : IBuildManager
    {

        #region IBuildManager Members

        public ICollection GetReferencedAssemblies()
        {
            return BuildManager.GetReferencedAssemblies();
        }

        #endregion
    }
}
#endif
