using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCIT.Core.Models.DTOs.Response
{
    public class ClienteDTO
    {
        public int id { get; set; }
        public int idcompania { get; set; }
        public string compania { get; set; }
        public int idPaisdependencia { get; set; }
        public string paisdependencia { get; set; }
        public int idAgente { get; set; }
        public string nombreAgente { get; set; }
        public string local { get; set; }
        public int idlocal { get; set; }
        public string dimCedula { get; set; }
        public int cedula { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string usuario { get; set; }
        public int idUsuario { get; set; }
        public string clave { get; set; }
        public string pin { get; set; }
        public string tipocuenta { get; set; }
        public DateTime fnacimiento { get; set; }
        public string pais { get; set; }
        public string paiscodigo { get; set; }
        public string ciudad { get; set; }
        public string ciudadcodigo { get; set; }
        public int idCiudad { get; set; }
        public string moneda { get; set; }
        public string defMoneda { get; set; }
        public string cpostal { get; set; }
        public string direccion { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public object template { get; set; }
        public int hashname { get; set; }
        public int perfil { get; set; }
        public DateTime fechaIngreso { get; set; }
        public bool validadoporFecha { get; set; }
        public DateTime validohastaFec { get; set; }
        public bool esdemo { get; set; }
        public bool status { get; set; }
        public bool reescribe { get; set; }
        public int saldooCredito { get; set; }
        public bool logeado { get; set; }
        public bool fromGC { get; set; }
        public bool registroRYR { get; set; }
        public string pregunta1 { get; set; }
        public string respuesta1 { get; set; }
        public string pregunta2 { get; set; }
        public string respuesta2 { get; set; }
        public string twitter { get; set; }
        public string facebook { get; set; }
        public bool is2Auth { get; set; }
        public int porcComision { get; set; }
        public object ctasAsociadas { get; set; }
    }

    public class ClienteSaldoDTO
    {
        public int idCliente { get; set; }
        public int idPaisdependencia { get; set; }
        public string paisdependencia { get; set; }
        public int idAgente { get; set; }
        public string nombreAgente { get; set; }
        public int idlocal { get; set; }
        public string local { get; set; }
        public object terminal { get; set; }
        public double balance { get; set; }
        public double balancependiente { get; set; }
        public double disponible { get; set; }
        public double balanceBono { get; set; }
        public double pendienteBono { get; set; }
        public double credito { get; set; }
        public double creditoTemporal { get; set; }
        public double saldoDiferido { get; set; }
        public double operacion { get; set; }
        public double saldooCredito { get; set; }
    }

    public class DataDTO
    {
        public string mensaje { get; set; }
        public bool exitoso { get; set; }
        public int idEstatus { get; set; }
        public ClienteDTO cliente { get; set; }
        public ClienteSaldoDTO clienteSaldo { get; set; }
        public object clienteImp { get; set; }
        public object ex { get; set; }
    }

    public class ObtenerClienteResponse
    {
        public string mensaje { get; set; }
        public bool estatus { get; set; }
        public int idEstatus { get; set; }
        public DataDTO data { get; set; }
    }
}
