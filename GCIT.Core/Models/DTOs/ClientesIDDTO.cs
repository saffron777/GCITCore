using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCIT.Core.Models.DTOs
{
    public class ClientesIDDTO
    {
        public long idCliente { get; set; }
        public long idcompania { get; set; }
        public string compania { get; set; }
        public long idPaisdependencia { get; set; }
        public string Paisdependencia { get; set; }
        public long idAgente { get; set; }
        public string NombreAgente { get; set; }
        public long idlocal { get; set; }
        public string local { get; set; }
        public string terminal { get; set; }
        public DateTime FechaIngreso { get; set; }
    }
}
