using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCIT.Core.Models
{
    public class Usuarios
    {
        public int idUser { get; set; }
        public string cedula { get; set; }
        public string NombreCliente { get; set; }
        public string correo { get; set; }
        public string NombreUser { get; set; }
        public string SubAgente { get; set; }
        public long clienteID { get; set; }

        public long idcliente { get; set; }
        public bool? notificaciones { get; set; }
        public string facebook { get; set; }
        public string twitter { get; set; }
        public string pregunta1 { get; set; }
        public string respuesta1 { get; set; }
        public string pregunta2 { get; set; }
        public string respuesta2 { get; set; }
    }
}
