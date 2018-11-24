using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrSystemLib.DataAccess;
using HrSystemLib.Models.Interfaces;

namespace HrSystemLib.Services
{
    public class BranchService : Interfaces.IBranchService
    {
        private BranchDA branchda;
        public BranchService()
        {
            branchda = new BranchDA();
        }
        public List<IBranch> ListBranches()
        {
            return branchda.GetBranches();
        }
        public IBranch GetBranch(int Id)
        {
            return ListBranches().Where(b => b.Id == Id).SingleOrDefault();
        }
    }
}
