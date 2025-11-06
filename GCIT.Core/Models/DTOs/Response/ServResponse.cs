using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCIT.Core.Models.DTOs.Response
{
    public class ServResponse<T>
    {
        public string Mensaje { get; set; }
        public bool Estatus { get; set; }
        public int idEstatus { get; set; }
        public T data { get; set; }
    }
}
