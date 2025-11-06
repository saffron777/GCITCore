using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCIT.Core.Models.DTOs
{
    public class ApiWebSiteDTO
    {
        public int idApi { get; set; }
        public string NameApi { get; set; }
        public int idSite { get; set; }
        public string WebSite { get; set; }
        public string SecretKey { get; set; }
        public string PublicKey { get; set; }
        public string Empresa { get; set; }
        public int idEmpresa { get; set; }
        public string Proveedor { get; set; }
        public int idProveedor { get; set; }
    }
}
