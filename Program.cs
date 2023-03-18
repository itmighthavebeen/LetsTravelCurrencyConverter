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

        public static string IsoCountryCodeToFlagEmoji(string countryCode) 
            => string.Concat(countryCode.ToUpper().Select(x => char.ConvertFromUtf32(x+0x1F1A5)));

        public static string GetFlag(string country)     //gives back correct flag, but only displays on Mac
        {
            var regions = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(x => new RegionInfo(x.Name));
            var englishRegion = regions.FirstOrDefault(region => region.EnglishName.Contains(country));

            if (englishRegion == null) return "🏳";

            var countryAbbrev = englishRegion.TwoLetterISORegionName;
            return IsoCountryCodeToFlagEmoji(countryAbbrev);
           
        }

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
            Console.WriteLine("This program will give you the value of a foreign currency to $1.");
            Console.WriteLine("\nIn order for the foreign currency symbols to appear correctly in windows,");
            Console.WriteLine("please set font to  SimSun-ExtB");

            ApiHelper.InitializeClient();

            bool Continue = true;    // Loop Control
            var getCurrencyType = new GetCurrencyType();      //instance of GetCurrencyType
            int amount = 1;
            string fromCurrency = "USD";

            //Read from Console
            while (Continue)        // Continue asking for country name until asked to STOP
            {
                Console.WriteLine("\nHello. Let us convert your currency, \nWhich country are you traveling to?");
                Console.WriteLine("Or type STOP to quit");

                var input = Console.ReadLine();     //Get input from terminal
                string caseString = string.Copy(input);
                caseString = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(caseString.ToLower());
                input = string.Copy(caseString);

                //
                //Validate that the Country exists in CountryList
                //
                if (CountryList().Contains(input) == true)      //Does exist logic
                {
                   
                    string toCurrency = getCurrencyType.MoneyType(input);    //get currency type ie USD
                    string toSymbol = getCurrencyType.MoneySymbol(input);    //get symbol ie $

                    //This will get an emoji flag but doesn't work on Windows, better on Mac. Didn't want to include if not universal
                    // var myFlag = GetFlag(input);
                    // Console.OutputEncoding = System.Text.Encoding.UTF8;

                    var apiRate = new ApiRate();
                    apiRate = await CurrencyRateProcessor.LoadRate(toCurrency);

                    // Calls a method to get the exchange rate between 2 currencies
                    // float exchangeRate = ConvertMyCurrency.GetExchangeRate(fromCurrency, toCurrency, amount);
                    // Print result of currency exchange rate
                    Console.WriteLine("You are converting from " + fromCurrency.ToUpper() + " to " + toCurrency.ToUpper());
                    Console.WriteLine( "$" + amount + " " + " EQUALS " + toSymbol  + apiRate.Result +  " as of date " + apiRate.Date);

                    //Have some fun and check rate
                    //display method if strong or weak
                    if (Decimal.Parse(apiRate.Result) <= 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.WriteLine("\nMaybe not the best time to go here, dollar not strong!");

                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("");
                        Console.WriteLine("\nGreat choice of travel to " + input + ". The dollar is STRONG $$");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                else                                            //Does not exist logic
                {
                    if (input.ToUpper() == "STOP" )                        //Checking if user asked to STOP
                        Continue = false;
                    else                                        //else prompt user to try country name again
                    {
                        Console.WriteLine("\nYou have typed in an invalid or misspelled country name.");
                        Console.WriteLine("Please try again");
                    }

                }
            } //End of While. Once out of While loop, stop program.
            Console.WriteLine("Thank you for converting your money with us!");
        }

      
    }

}