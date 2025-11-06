using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGIT.Core.Models
{
    public class SiteEmpresa
    {
        public int IdSite { get; set; }
        public int idEmpresa { get; set; }
        public string Site { get; set; }
        public string ConexionBD { get; set; }
        public string Nombre { get; set; }
        public string UrlWeb { get; set; }
        public string Email { get; set; }
        public string Rif { get; set; }
        public string Publicidad { get; set; }
        public string Telefonos { get; set; }
        public string Licencia { get; set; }
        public string UrlVerif { get; set; }
        public string UrlImag { get; set; }
        public long templateID { get; set; }
        public long templateTokenID { get; set; }
        public string codAfiliadoBT { get; set; }
    }
}
