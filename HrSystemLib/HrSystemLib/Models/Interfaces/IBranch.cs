using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystemLib.Models.Interfaces
{
    public interface IBranch
    {
        int Id { get; set; }
        string Code { get; set; }
        string Name { get; set; }
        int CurrencyId { get; set; }
        int CreatedByUserId { get; set; }
        DateTime CreatedOnDate { get; set; }
        bool IsDeleted { get; set; }

    }
}
