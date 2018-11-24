using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrSystemLib.Models.Interfaces;

namespace HrSystemLib.Services.Interfaces
{
    public interface IBranchService
    {
        List<IBranch> ListBranches();
        IBranch GetBranch(int Id);
    }
}
