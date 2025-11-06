using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGIT.Core.Models
{
    public class Empresa
    {
        public int Id { get; set; }

        public string Nombre { get; set; }


        public string ConexionBD { get; set; }


        public string CuentaBancaria { get; set; }


        public string CuentaMiBanco { get; set; }


        public string CuentaBDT { get; set; }


        public string Rif { get; set; }


        public string TlfPagoMovil { get; set; }


        public string Apisecret_KeyPM { get; set; }


        public string Apipublic_KeyPM { get; set; }


        public string Correo { get; set; }


        public string Telefonos { get; set; }

        // varchar(max) maps to string without length limit
        public string TokenPM { get; set; }


        public string TelefMiBanco { get; set; }
        
    }
}
