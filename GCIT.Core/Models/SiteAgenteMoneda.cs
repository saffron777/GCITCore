using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGIT.Core.Models
{
    public class SiteAgenteMoneda
    {
        public long idAgente { get; set; }
        public string Agente { get; set; }
        public int idSite { get; set; }
        public string Site { get; set; }
        public string IsoMoneda { get; set; }
        public string defMoneda { get; set; }
        public bool isDefaultAg { get; set; }
    }
}
