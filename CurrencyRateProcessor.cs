using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Web;
using System.Net.Http;
using System.Runtime.ConstrainedExecution;
using System.IO;
using System.Net.Http.Json;

namespace LetsTravelCurrencyConverter
{
    public class CurrencyRateProcessor
    {
        static readonly HttpClient client = new HttpClient();        //instantiated once per application
        public static async Task<ApiRate> LoadRate(string toCountry = "")
        {
            string url = "";
            if (toCountry != "")
            {
                url = $"https://api.exchangerate.host/convert?from=USD&to=" + toCountry;
                var responseTask = client.GetStringAsync(url);
                responseTask.Wait();
                if (responseTask.IsCompleted)
                {
                    var result = responseTask.Result;
                    Console.WriteLine(responseTask.Result);
                }
            }
            else
            {
                url = $"https://api.exchangerate.host/convert?from=USD&to=USD";                     //WHAT TO DO HERE?
                Console.WriteLine("ELSE" + url);

            }
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    ApiRate foundRate = await response.Content.ReadAsAsync<ApiRate>();
                    // ApiRate foundRate = reponseBody.
                    Console.WriteLine(foundRate.Date);

                    Console.WriteLine(foundRate.Result);
                    // Print result of currency exchange rate
                    //Console.WriteLine("FROM " + amount + " " + fromCurrency.ToUpper() + " TO " + toCurrency.ToUpper() + " = " + foundRate.Rate + " as of date " + foundRate.Date );
                    return foundRate;



                }
                else
                {
                    Console.WriteLine("Inside  Else");
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}

