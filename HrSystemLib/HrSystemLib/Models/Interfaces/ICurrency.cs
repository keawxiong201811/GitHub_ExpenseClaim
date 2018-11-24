using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystemLib.Models.Interfaces
{
    public interface ICurrency
    {
        int Id { get; set; }
        string Code { get; set; }
        string Description { get; set; }
        int CreatedByUserId { get; set; }
        DateTime CreatedOnDate { get; set; }
        bool IsDeleted { get; set; }
    }
}
