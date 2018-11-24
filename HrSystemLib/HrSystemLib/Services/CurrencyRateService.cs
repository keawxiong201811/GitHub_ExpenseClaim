using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrSystemLib.DataAccess;
using HrSystemLib.Models.Interfaces;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using System.Configuration;
using HrSystemLib.Helper;
using Newtonsoft.Json.Linq;

namespace HrSystemLib.Services
{
    public class CurrencyRateService : Interfaces.ICurrencyRateService
    {
        public readonly ICurrency LocalCurrency;
        public CurrencyRateService(ICurrency LocalCurrency)
        {
            this.LocalCurrency = LocalCurrency;
        }

        public decimal GetRate(ICurrency ForeignCurrency, DateTime OnDate)
        {
            if (ForeignCurrency.Code.ToUpper() == LocalCurrency.Code.ToUpper())
                return 1;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings[FixerCurrencyHelper.BaseUrlConfig].ToString());
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string requesturi;
                if (OnDate.Date >= DateTime.Today)
                {
                    requesturi = String.Format("latest?access_key={0}&base={1}&symbols={2}",
                                               ConfigurationManager.AppSettings[FixerCurrencyHelper.AccessKeyConfig].ToString(),
                                               ForeignCurrency.Code,
                                               LocalCurrency.Code);
                }
                else
                {
                    requesturi = String.Format("{0}?access_key={1}&base={2}&symbols={3}",
                                               OnDate.ToString("yyyy-MM-dd"),
                                               ConfigurationManager.AppSettings[FixerCurrencyHelper.AccessKeyConfig].ToString(),
                                               ForeignCurrency.Code,
                                               LocalCurrency.Code);
                }
                HttpResponseMessage response = client.GetAsync(requesturi).Result;

                if (response.IsSuccessStatusCode)
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    JObject jo = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                    if (jo["success"].ToString().ToLower() == "true")
                        return Decimal.Parse(jo["rates"][LocalCurrency.Code.ToUpper()].ToString());
                    else
                        throw new Exception(jo["error"].ToString());
                }
                else
                    throw new Exception(response.StatusCode.ToString());
            }
        }
    }
}
