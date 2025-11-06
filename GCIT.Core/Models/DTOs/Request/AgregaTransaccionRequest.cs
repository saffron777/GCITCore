using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCIT.Core.Models.DTOs.Request
{
    public class AgregaTransaccionRequest
    {
        public double monto { get; set; }
        public string descripcion { get; set; }
        public string usuario { get; set; }
        public bool isWeb { get; set; }
        public int idlocal { get; set; }
        public int idAgente { get; set; }
        public string agente { get; set; }
        public int idcliente { get; set; }
        public string tipoTransaccion { get; set; }
        public string tipoProducto { get; set; }
        public string tipoSaldo { get; set; }
        public string webSite { get; set; }
    }
}
