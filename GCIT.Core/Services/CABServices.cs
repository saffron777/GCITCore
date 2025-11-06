using GCIT.Core.Common;
using GCIT.Core.Helpers;
using GCIT.Core.Models.DTOs.Request;
using GCIT.Core.Models.DTOs.Response;
using GCIT.Core.Services.Interfaces;
using GCIT.Core.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCIT.Core.Services
{
    public class CABServices : ICABServices
    {
        private readonly ILogger<CABServices> _logger;
        private readonly AppSettings _settings;
        private RestSharp.RestClient Client;
        public CABServices(
            ILogger<CABServices> logger,
            IOptions<AppSettings> settings
            )
        {
            _logger = logger;
            _settings = settings.Value;
            Client = new RestSharp.RestClient(_settings.ApiCustomerSettings.BaseUrl);
        }

        public async Task<ServResponse<ClienteSaldoResponse>> ObtenerSaldoAsync(ClienteSaldoRequest request)
        {
            return await ProcesarRequestAsync<ClienteSaldoRequest, ServResponse<ClienteSaldoResponse>>("/api/Customer/saldo", Method.Post, request);
        }

        public async Task<ObtenerClienteResponse> ObtenerClienteAsync(ObtenerClienteRequest request)
        {
            return await ProcesarRequestAsync<ObtenerClienteRequest, ObtenerClienteResponse>("/api/Customer/ObtenerCliente", Method.Post, request);
        }

        public async Task<AgregaTransaccionResponse> AgregaTransaccionAsync(AgregaTransaccionRequest request)
        {
            var webSiteSettings = Utils.GetApiWebSiteSettings(Constantes.API_TRANSACTION, Constantes.API_PROVEEDOR, request.webSite);
            _logger.LogInformation($"AgregaTransaccionAsync");
            var json = JsonConvert.SerializeObject(request);
            _logger.LogInformation($"request => {json}");
            var url = _settings.urlApiTransaccion;

            var client = new RestSharp.RestClient(url);

            var req = new RestRequest("/Transaccion/AgregaTransaccion", Method.Post);

            req.AddHeader("SecretKey", webSiteSettings.SecretKey);
            req.AddHeader("PublicKey", webSiteSettings.PublicKey);
            req.AddHeader("Content-Type", "application/json");

            req.AddStringBody(json, DataFormat.Json);

            RestResponse resp = await client.ExecuteAsync(req);

            if (!resp.IsSuccessStatusCode)
                throw new Exception(resp.Content);

            var response = JsonConvert.DeserializeObject<AgregaTransaccionResponse>(resp.Content);
            _logger.LogInformation($"reponse => {resp.Content}");
            return response;
        }

        private async Task<TResponse> ProcesarRequestAsync<TRequest, TResponse>(string endpoint, Method method, TRequest request)
        {
            _logger.LogInformation($"ProcesarRequestAsync - Endpoint: {endpoint}");

            var url = _settings.ApiCustomerSettings.BaseUrl;

            var client = new RestSharp.RestClient(url);

            var req = new RestRequest(endpoint, method);



            req.AddHeader("SecretKey", _settings.ApiCustomerSettings.SecretKey);

            req.AddHeader("PublicKey", _settings.ApiCustomerSettings.PublicKey);
            req.AddHeader("Content-Type", "application/json");

            if (method == Method.Get)
            {
                // Agregar parámetros como query parameters para solicitudes GET
                var properties = typeof(TRequest).GetProperties();
                foreach (var property in properties)
                {
                    var value = property.GetValue(request);
                    if (value != null)
                    {
                        req.AddQueryParameter(property.Name, value.ToString());
                    }
                }
            }
            else
            {
                // Serializar el cuerpo para solicitudes POST
                var json = JsonConvert.SerializeObject(request);
                _logger.LogInformation($"Request => {json}");
                req.AddStringBody(json, DataFormat.Json);
            }

            RestResponse resp = await client.ExecuteAsync(req);

            if (!resp.IsSuccessStatusCode)
                throw new Exception(resp.Content);

            var response = JsonConvert.DeserializeObject<TResponse>(resp.Content);
            _logger.LogInformation($"Response => {resp.Content}");
            return response;
        }

    }
}
