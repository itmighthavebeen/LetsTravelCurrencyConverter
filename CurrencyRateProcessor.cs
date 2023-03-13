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
            //  HttpClient client = new HttpClient();   



            if (toCountry != "")
            {
                url = $"https://api.exchangerate.host/convert?from=USD&to=" + toCountry;

                Console.WriteLine("IF" + url);
                var responseTask = client.GetStringAsync(url);
                responseTask.Wait();
                if (responseTask.IsCompleted)
                {
                    var result = responseTask.Result;
                    Console.WriteLine(responseTask.Result);
                    // var message = result;
                    //  message.Wait();
                    //  Console.WriteLine(message);

                }

            }
            else
            {
                url = $"https://api.exchangerate.host/convert?from=USD&to=USD";
                Console.WriteLine("ELSE" + url);

            }
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();


                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("InsodeIF" + responseBody);
                    ApiRate foundRate = await response.Content.ReadAsAsync<ApiRate>();
                    // ApiRate foundRate = reponseBody.
                    Console.WriteLine(foundRate.Date);
                    Console.WriteLine(foundRate.Result);
                    Console.WriteLine(foundRate);
                    Console.WriteLine(foundRate.Rate);
                    // Print result of currency exchange rate
                    //Console.WriteLine("FROM " + amount + " " + fromCurrency.ToUpper() + " TO " + toCurrency.ToUpper() + " = " + foundRate.Rate);
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

