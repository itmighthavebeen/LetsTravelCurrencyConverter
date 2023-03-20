namespace LetsTravelCurrencyConverter
{
    public class CurrencyRateProcessor
    {
        static readonly HttpClient client = new HttpClient();       
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
                catch (AggregateException)  //needs internet to call url 
                {
                    // When waiting on the task, an AggregateException is thrown.
                    Console.WriteLine("This app requires an internet connection");

                    Environment.Exit(1);                                //Error 1 need internet for program to run
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

                }
            }
            else
            {
                Console.WriteLine("null value error on country");
                Environment.Exit(3);                                   
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

