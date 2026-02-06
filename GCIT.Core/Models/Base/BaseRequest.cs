using System;
using System.Collections.Generic;
using System.Text;

namespace GCIT.Core.Models.Base
{
    public abstract class BaseRequest
    {
        
    }

    /// <summary>
    /// Request base para todas las peticiones.
    /// </summary>
    public class BaseRequest<T> : BaseRequest
    {
        /// <summary>
        /// Datos de la petición.
        /// </summary>
        public required T Datos { get; set; }
    }
}
