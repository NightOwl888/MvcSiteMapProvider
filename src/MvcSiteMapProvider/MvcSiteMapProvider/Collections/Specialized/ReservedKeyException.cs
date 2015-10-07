﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Collections.Specialized
{
    /// <summary>
    /// ReservedKeyException
    /// </summary>
#if !MVC6
    [Serializable]
#endif
    public class ReservedKeyException : MvcSiteMapException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ReservedKeyException()
        { 
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Message</param>
        public ReservedKeyException(string message) 
            : base(message) 
        { 
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="innerException">Inner Exception</param>
        public ReservedKeyException(string message, Exception innerException) 
            : base(message, innerException) 
        { 
        }
    }
}
