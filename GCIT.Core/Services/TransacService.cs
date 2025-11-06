using GCIT.Core.Common;
using GCIT.Core.Helpers;
using GCIT.Core.Models.DTOs.Request;
using GCIT.Core.Models.DTOs.Response;
using GCIT.Core.Services.Interfaces;
using GCIT.Core.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Gaming.Core.Services
{
    public class TransacService : ITransacService
    {
        public static string _baseurl;
        private readonly ILogger<TransacService> _logger;
        private readonly AppSettings _settings;
        public TransacService(ILogger<TransacService> logger, IOptions<AppSettings> options)
        {
            _settings = options.Value;
            _baseurl = _settings.urlApiTransaccion;
            _logger = logger;
        }

        public async Task<ApiTransResponse> AgregarTransaccion(TransAddRequest req, Dictionary<string, string> Headers)
        {
            ApiTransResponse? result = null;
            try
            {
                _logger.LogInformation($"AgregaTransaccion request => {JsonConvert.SerializeObject(req)}");
                //HttpClientHandler clientHandler = new HttpClientHandler();
                //clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClientHandler handler = new HttpClientHandler()
                {
                    AutomaticDecompression = DecompressionMethods.GZip |
                                       DecompressionMethods.Deflate
                };
                var cliente = new HttpClient(handler);
                cliente.BaseAddress = new Uri(_baseurl);
                foreach (var item in Headers)
                {
                    cliente.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
                var content = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
                var response = await cliente.PostAsync("Transaccion/AgregaTransaccion", content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonRespo = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<ApiTransResponse>(jsonRespo);
                    _logger.LogInformation($"AgregaTransaccion response => {jsonRespo}");
                    return result!;
                }
                else
                {
                    result = new ApiTransResponse();
                    result.exitoso = false;
                    result.idEstatus = -10;
                    result.mensaje = response.RequestMessage!.ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                //writer.WriteToLog($"Error: {ex.Message}-->{ex.ToString()}");
                return result!;
            }
        }

        public async Task<AgregaTransaccionResponse> AgregaTransaccionAsync(AgregaTransaccionRequest request)
        {
            var webSiteSettings = Utils.GetApiWebSiteSettings(Constantes.API_TRANSACTION, Constantes.API_PROVEEDOR, request.webSite);
            _logger.LogInformation($"AgregaTransaccionAsync");
            var json = JsonConvert.SerializeObject(request);
            _logger.LogInformation($"request => {json}");
            var url = _baseurl;

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
    }
}
