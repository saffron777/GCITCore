using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCIT.Core.Models.DTOs.Response
{
    public class AgregaTransaccionResponse
    {
        public bool exitoso { get; set; }
        public string mensaje { get; set; }
        public int idStatus { get; set; }
        public int data { get; set; }
    }
}
