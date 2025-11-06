using CGIT.Core.Models;
using Dapper;
using GCIT.Core.Models;
using GCIT.Core.Models.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCIT.Core.Helpers
{
    public static class Utils
    {
        private static string _cadenaConex;
        public static string _cadenaRR;

        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static string DBConnectionString { get; set; }

        public static void Initialize(IConfiguration configuration)
        {
            DBConnectionString = configuration.GetConnectionString("DefaultDB");
            _cadenaConex = DBConnectionString;
        }

        public static ApiWebSiteDTO GetApiWebSiteSettings(string idApi, string proveedor, string website)
        {
            var apiws = new ApiWebSiteDTO();
            string sql = $"SELECT * FROM [GCITBR].dbo.[ApiWebSite] WHERE idApi = {idApi} AND rtrim(ltrim(Proveedor)) = '{proveedor}' and UPPER(rtrim(ltrim(WebSite))) = '{website.ToUpper()}'";
            using (SqlConnection con = new(_cadenaConex))
            {
                apiws = con.Query<ApiWebSiteDTO>(sql).FirstOrDefault();
            }

            return apiws;
        }

        public static Empresa GetEmpresa(string agente)
        {
            Empresa Empresa = default;
            string sql = $"SELECT e.*, se.codAfiliadoBT " +
                $"FROM [GCITBR].dbo.SiteAgenteMoneda sam " +
                $"inner join [GCITBR].dbo.SiteEmpresa se on sam.idSite = se.IdSite  " +
                $"inner join [GCITBR].dbo.Empresa e on se.idEmpresa = e.id  where Lower(sam.Agente) = '{agente.ToLower().Trim()}' ";

            using (SqlConnection con = new (DBConnectionString))
            {
                Empresa = con.Query<Empresa>(sql).FirstOrDefault();
            }
            return Empresa;

        }

        public static Empresa GetEmpresa(int idCliente)
        {
            Empresa? Empresa;

            string sql = $"select e.* " +
                $"from [GCITBR].dbo.clientes c " +
                $"inner join [GCITBR].dbo.clientesID cid on c.id = cid.idCliente " +
                $"inner join [GCITBR].dbo.SiteAgenteMoneda sam on sam.idAgente = cid.idAgente " +
                $"inner join [GCITBR].dbo.SiteEmpresa se on sam.idSite = se.IdSite " +
                $"inner join [GCITBR].dbo.Empresa e on se.idEmpresa = e.id " +
                $"where c.id = {idCliente}";


            using (SqlConnection con = new(_cadenaConex))
            {
                Empresa = con.Query<Empresa>(sql).FirstOrDefault();
            }

            return Empresa!;
        }
        public static Empresa GetEmpresa(string usuario, bool esUsuario)
        {
            Empresa? Empresa;

            if (!esUsuario)
            {
                return GetEmpresa(usuario);
            }

            string sql = $"select e.* " +
                $"from [GCITBR].dbo.clientes c " +
                $"inner join [GCITBR].dbo.clientesID cid on c.id = cid.idCliente " +
                $"inner join [GCITBR].dbo.SiteAgenteMoneda sam on sam.idAgente = cid.idAgente " +
                $"inner join [GCITBR].dbo.SiteEmpresa se on sam.idSite = se.IdSite " +
                $"inner join [GCITBR].dbo.Empresa e on se.idEmpresa = e.id " +
                $"where c.Usuario = '{usuario}'";


            using (SqlConnection con = new(_cadenaConex))
            {
                Empresa = con.Query<Empresa>(sql).FirstOrDefault();
            }

            return Empresa!;
        }

        public static Compania GetCompania(int idEmpresa)
        {
            Compania? Compania;
            string sql = $"select * from [GCITBR].dbo.Compania where id={idEmpresa}";
            using (SqlConnection con = new(DBConnectionString))
            {
                Compania = con.Query<Compania>(sql).FirstOrDefault();
            }
            return Compania!;
        }

        public static SiteAgenteMoneda GetSitPorIdCliente(int idCliente)
        {
            SiteAgenteMoneda sam = default;
            string sql = $"select a.* from [GCITBR].dbo.Clientes c " +
            "inner join [GCITBR].dbo.ClientesID cid on c.id = cid.[idCliente] " +
            "inner join [GCITBR].dbo.SiteAgenteMoneda a on cid.[idAgente] = a.[idAgente] " +
            $"where c.id = {idCliente}";

            using (SqlConnection con = new (DBConnectionString))
            {
                sam = con.Query<SiteAgenteMoneda>(sql).FirstOrDefault();
            }
            return sam;

        }

        public static Empresa GetEmpresa(string telef, string rif, string ctaBanco)
        {
            Empresa? Empresa;
            string sql = "";

            if (!string.IsNullOrEmpty(rif))
                sql = @$"SELECT TOP 1 e.*, se.codAfiliadoBT FROM [GCITBR].dbo.Empresa as e INNER JOIN [GCITBR].dbo.SiteEmpresa as se ON se.idEmpresa=e.id 
                         WHERE REPLACE(e.Rif,'-','')='{rif}'";

            if (!string.IsNullOrEmpty(telef) && string.IsNullOrEmpty(sql))
                sql = @$"SELECT TOP 1 e.*, se.codAfiliadoBT FROM [GCITBR].dbo.Empresa as e INNER JOIN [GCITBR].dbo.SiteEmpresa as se ON se.idEmpresa=e.id 
                                INNER JOIN [GCITBR].dbo.CuentaBancoEmpresa as cbe ON cbe.idSite=se.IdSite
                       WHERE cbe.TelefonoPM='{telef}' OR e.TlfPagoMovil='{telef}'";

            if (!string.IsNullOrEmpty(ctaBanco) && string.IsNullOrEmpty(sql))
                sql = @$"SELECT TOP 1 e.*, se.codAfiliadoBT FROM [GCITBR].dbo.Empresa as e INNER JOIN [GCITBR].dbo.SiteEmpresa as se ON se.idEmpresa=e.id 
                                INNER JOIN [GCITBR].dbo.CuentaBancoEmpresa as cbe ON cbe.idSite=se.IdSite
                       WHERE cbe.Cuenta='{ctaBanco}'";


            using (SqlConnection con = new(_cadenaConex))
            {
                Empresa = con.Query<Empresa>(sql).FirstOrDefault();
            }
            return Empresa!;
        }

        public static SiteEmpresa GetSiteEmpresa(string telef, string rif, string ctaBanco)
        {
            SiteEmpresa siteEmp = default;
            string sql = "";

            if (!string.IsNullOrEmpty(rif))
                sql = $@"SELECT TOP 1 se.*,e.ConexionBD FROM [GCITBR].dbo.Empresa as e INNER JOIN [GCITBR].dbo.SiteEmpresa as se ON se.idEmpresa=e.id 
                         WHERE REPLACE(e.Rif,'-','')='{rif}'";

            if (!string.IsNullOrEmpty(telef) && string.IsNullOrEmpty(sql))
                sql = $@"SELECT TOP 1 se.*,e.ConexionBD FROM [GCITBR].dbo.Empresa as e INNER JOIN [GCITBR].dbo.SiteEmpresa as se ON se.idEmpresa=e.id 
                                INNER JOIN [GCITBR].dbo.CuentaBancoEmpresa as cbe ON cbe.idSite=se.IdSite
                       WHERE cbe.TelefonoPM='{telef}' OR e.TlfPagoMovil='{telef}'";

            if (!string.IsNullOrEmpty(ctaBanco) && string.IsNullOrEmpty(sql))
                sql = $@"SELECT TOP 1 se.*,e.ConexionBD FROM [GCITBR].dbo.Empresa as e INNER JOIN [GCITBR].dbo.SiteEmpresa as se ON se.idEmpresa=e.id 
                                INNER JOIN [GCITBR].dbo.CuentaBancoEmpresa as cbe ON cbe.idSite=se.IdSite
                       WHERE cbe.Cuenta='{ctaBanco}'";

            // Replaced System.Data.SqlClient with Microsoft.Data.SqlClient


            using (SqlConnection con = new SqlConnection(DBConnectionString))
            {
                siteEmp = con.Query<SiteEmpresa>(sql).FirstOrDefault();
            }
            return siteEmp;
        }

        public static Usuarios? getUserPorIDCliente(long idCliente)
        {
            Usuarios? user = null;
            string sql = "";


            sql = @$"SELECT TOP 1 u.idUser,u.correo,u.NombreCliente,u.NombreUser,u.cedula,u.SubAgente,u.clienteID 
                          FROM dbo.Usuarios as u 
                          WHERE u.[clienteID]={idCliente} AND u.activa=1";
            try
            {
                using (SqlConnection con = new(_cadenaRR))
                {
                    con.Open();
                    user = con.Query<Usuarios>(sql).FirstOrDefault();
                }
                return user;
            }
            catch (Exception ex)
            {
                //writer.WriteToLog($"Error: {ex.Message}-->{ex.ToString()}");
                return null;
            }
        }

        public static Usuarios? getUserPorNombre(string nombreUser)
        {
            Usuarios? user = null;
            string sql = "";


            sql = @$"SELECT TOP 1 u.idUser,u.correo,u.NombreCliente,u.NombreUser,u.cedula,u.SubAgente,u.clienteID 
                          FROM dbo.Usuarios as u 
                          WHERE u.[NombreUser]='{nombreUser}' AND u.activa=1";
            try
            {
                using (SqlConnection con = new(_cadenaRR))
                {
                    con.Open();
                    user = con.Query<Usuarios>(sql).FirstOrDefault();
                }
                return user;
            }
            catch (Exception ex)
            {
                //writer.WriteToLog($"Error: {ex.Message}-->{ex.ToString()}");
                return null;
            }
        }

        public static ClientesIDDTO GetClienteID(int idCliente)
        {
            ClientesIDDTO? clientesIDDTO;
            string sql = $"select * from [GCITBR].dbo.[ClientesID] where idCliente = {idCliente}";
            using (SqlConnection con = new(_cadenaConex))
            {
                clientesIDDTO = con.Query<ClientesIDDTO>(sql).FirstOrDefault();
            }
            return clientesIDDTO!;
        }

        public static long ConvertToTimestamp(DateTime value)
        {
            TimeSpan elapsedTime = value - Epoch;
            return (long)elapsedTime.TotalSeconds;
        }
    }
}
