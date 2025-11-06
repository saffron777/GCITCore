using GCIT.Core.Models.DTOs.Request;
using GCIT.Core.Models.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCIT.Core.Services.Interfaces
{
    public interface ICABServices
    {
        Task<AgregaTransaccionResponse> AgregaTransaccionAsync(AgregaTransaccionRequest request);
        Task<ObtenerClienteResponse> ObtenerClienteAsync(ObtenerClienteRequest request);
        Task<ServResponse<ClienteSaldoResponse>> ObtenerSaldoAsync(ClienteSaldoRequest request);
    }
}
