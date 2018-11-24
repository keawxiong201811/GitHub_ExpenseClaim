using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrSystemLib.DataAccess;
using HrSystemLib.Models.Interfaces;

namespace HrSystemLib.Services
{
    public class BankService : Interfaces.IBankService
    {
        private BankDA bankda;
        public BankService()
        {
            bankda = new BankDA();
        }

        public List<IBank> ListBanks()
        {
            return bankda.GetBanks();
        }

        public IBank GetBank(int Id)
        {
            return ListBanks().Where(b => b.Id == Id).SingleOrDefault();
        }
    }
}
