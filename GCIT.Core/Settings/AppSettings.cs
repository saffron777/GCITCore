using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCIT.Core.Settings
{
    public class AppSettings
    {
        public string urlApiTransaccion { get; set; }
        public ApiCustomerSettings ApiCustomerSettings { get; set; }
        public string idApi { get; set; }
    }

    public class ApiCustomerSettings
    {
        public string BaseUrl { get; set; }
        public string SecretKey { get; set; }
        public string PublicKey { get; set; }
    }
}
