// See https://aka.ms/new-console-template for more information
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Net.NetworkInformation;

namespace LetsTravelCurrencyConverter

{

    class Program
    {
        //Define Method called CoutryList that will return a sorted list of Countries
        //Countries are retrieved using CutureInfo class from system.globalization
        //This Method is retrieving the EnglishName of the country
        //



        public static List<string> CountryList()
        {
            //Creating list
            List<string> CultureList = new();



            //getting  the specific  CultureInfo from CultureInfo class
            CultureInfo[] getCultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            foreach (CultureInfo getCulture in getCultureInfo)
            {
                //creating the object of RegionInfo class
                RegionInfo GetRegionInfo = new(getCulture.Name);
                //adding each county English Name into the arraylist of CultureList
                if (!CultureList.Contains(GetRegionInfo.EnglishName))
                {
                    CultureList.Add(GetRegionInfo.EnglishName);
                }
            }
            //sorting array by using sort method to get countries in order
            CultureList.Sort();
            //returning country list
            return CultureList;
        }
        //
        //End of Method CountryList
        //

        static async Task Main(string[] args)       //needs C# 7.1 or later
        {
            // 
            // Ask Which Country and get input from terminal
            // Note this program isn't coded to parse the country name for mispellings of no capital letter in front
            // Country name must match exactly. This is an area I wish I had time and expertise to fix.
            //  Do this Until User enters STOP
            //
            ApiHelper.InitializeClient();
            bool Continue = true;    // Loop Control
            var getCurrencyType = new GetCurrencyType();      //instance of GetCurrencyType
            int amount = 1;

            string fromCurrency = "USD";
            //Read from Console
            while (Continue)        // Continue asking for country name until asked to STOP
            {
                Console.WriteLine("Hello. Which Country Are you traveling to?");
                Console.WriteLine("Or type STOP to quit");

                var input = Console.ReadLine();     //Get input from terminal
                //
                //Validate that the Country exists in CountryList
                //
                if (CountryList().Contains(input) == true)      //Does exist logic
                {
                    Console.WriteLine("Item exists! Now I need to get Currency code");
                    Console.Write(getCurrencyType.MoneyType(input));
                    string toCurrency = getCurrencyType.MoneyType(input);
                    var apiRate = new ApiRate();
                    apiRate = await CurrencyRateProcessor.LoadRate(toCurrency);
                    Console.WriteLine(apiRate.Result);

                    // Calls a method to get the exchange rate between 2 currencies
                    // float exchangeRate = ConvertMyCurrency.GetExchangeRate(fromCurrency, toCurrency, amount);
                    // Print result of currency exchange rate
                    Console.WriteLine("FROM " + amount + " " + fromCurrency.ToUpper() + " TO " + toCurrency.ToUpper() + " = " + apiRate.Result);
                    //Have some fun and check rate
                    //if (foundRate <= 1)
                    //   Console.WriteLine("Maybe not the best time to go here, dollar not strong");
                    //else
                    //   Console.WriteLine("Great choice of travel to " + input + ". The dollar is STRONG");
                }
                else                                            //Does not exist logic
                {
                    if (input == "STOP")                        //Checking if user asked to STOP
                        Continue = false;
                    else                                        //else prompt user to try country name again
                    {
                        Console.WriteLine("You have typed in an iinvalid or misspelled country name.");
                        Console.WriteLine("Please try again");
                    }

                }
            } //End of While. Once out of While loop, stop program.
            Console.WriteLine("Thank you for converting your money with us!");
        }

    }

}