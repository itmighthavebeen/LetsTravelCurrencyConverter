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
                try
                {
                    responseTask.Wait();
                }
                catch (AggregateException)  //needs internet to call url - found this error thanks to wind storm
                {
                    // When waiting on the task, an AggregateException is thrown.
                    Console.WriteLine("This app requires an internet connection");

                    Environment.Exit(1);                                //Error 2 need internet for program to run
                }
                if (responseTask.IsCompleted)
                {
                    try
                    {
                        var result = responseTask.Result;
                    }
                    catch (AggregateException)
                    {
                        Console.WriteLine("unable to get needed info from internet");
                        Environment.Exit(2);
                    }

                    // Console.WriteLine(responseTask.Result);
                }
            }
            else
            {
                Console.WriteLine("null value error on country");
                Environment.Exit(3);                                    //Error value 3 is null value of toCountry

            }

            //return rate

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    ApiRate foundRate = await response.Content.ReadAsAsync<ApiRate>();
                    return foundRate;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}

