using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrSystemLib.DataAccess;
using HrSystemLib.Models.Interfaces;

namespace HrSystemLib.Services
{
    public class CurrencyService : Interfaces.ICurrencyService
    {
        private CurrencyDA currencyda;
        public CurrencyService()
        {
            currencyda = new CurrencyDA();
        }
        public List<ICurrency> ListCurrencies()
        {
            return currencyda.GetCurrencies();
        }
        public ICurrency GetCurrency(int Id)
        {
            return currencyda.GetCurrencies().Where(c => c.Id == Id).SingleOrDefault();
        }
    }
}
