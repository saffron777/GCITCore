using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCIT.Core.Models.DTOs.Request
{
    public class ObtenerClienteRequest
    {
        public int idCliente { get; set; }
        public string usuario { get; set; }
        public string cedula { get; set; }
        public string agente { get; set; }
        public string website { get; set; }
    }
}
