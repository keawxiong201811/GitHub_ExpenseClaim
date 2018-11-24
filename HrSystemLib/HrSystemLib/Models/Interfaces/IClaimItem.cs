using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystemLib.Models.Interfaces
{
    public interface IClaimItem
    {
        int Id { get; set; }
        int ClaimId { get; set; }
        string Description { get; set; }
        int CurrencyId { get; set; }
        string CurrencyCode { get; set; }
        decimal Amount { get; set; }
        decimal ExchangeRate { get; set; }
        decimal LocalAmount { get; set; }
    }
}
