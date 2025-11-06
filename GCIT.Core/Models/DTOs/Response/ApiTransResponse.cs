using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCIT.Core.Models.DTOs.Response
{
    public class ApiTransResponse
    {
        public bool exitoso { get; set; }
        public string mensaje { get; set; }
        public int idEstatus { get; set; }
        public long data { get; set; }
    }
}
