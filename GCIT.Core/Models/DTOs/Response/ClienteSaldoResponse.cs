using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCIT.Core.Models.DTOs.Response
{
    public class ClienteSaldoResponse
    {
        public decimal? SaldoDisponible { get; set; }
        public decimal? SaldoDiferido { get; set; }
        public decimal? BalanceBono { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
