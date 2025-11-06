using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCIT.Core.Models.DTOs.Request
{
    public class ClienteSaldoRequest
    {
        public long idAgente { get; set; }
        public long idCliente { get; set; }
        public string Compania { get; set; }
        public string Pais { get; set; }
    }
}
