// See https://aka.ms/new-console-template for more information

namespace LetsTravelCurrencyConverter
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("This program will give you the value of a foreign currency to 1 US Dollar");
            List<string> listOfCountries = CreateListOfValidCountries.CountryList();
            ApiHelper.InitializeClient();

            bool ContinueLookingForInput = true;
            int amount = 1;
            string fromCurrency = "USD";

            while (ContinueLookingForInput)
            {

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n\nHello. Let us convert your currency, \nPlease enter a full country name or the first few letters. ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nCountries with diacritics must be input as shown below:");
                Console.WriteLine("For Ivory Coast, enter Ivoire. \nFor Sao Tome and Principe, enter Tom.");
                Console.WriteLine("For Aland Islands, enter islands. \nFor St. Barthelemy, enter st. ");
                Console.WriteLine("For Congo DRC enter congo (d ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nWhich country are you traveling to?");
                Console.WriteLine("(Type STOP to quit)");

                var input = Console.ReadLine();
                string? caseString = input;

                caseString = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(caseString!.ToLower());

                input = caseString;

                bool CountryFoundInList = listOfCountries.Any(x => x.Contains(input));
                bool InputFromConsoleNotSpaceOrEnter = true;

                //checking for valid input

                if (String.IsNullOrEmpty(input) || String.IsNullOrWhiteSpace(input))
                    InputFromConsoleNotSpaceOrEnter = false;


                //Validate that the Country exists in CountryList

                if (CountryFoundInList && InputFromConsoleNotSpaceOrEnter)

                {
                    string FindCountryNameInList = listOfCountries.First(s => s.Contains(input));

                    string RightCountryYesNo = "NO";
                    string valueToMatch = input;
                    var matchedValue = listOfCountries.FirstOrDefault(x => x.Equals(valueToMatch, StringComparison.OrdinalIgnoreCase));


                    //added this code late in the game while checking all UN nations. Special characters causing issues like in Ivoiry Coast

                    string WhatCountryToLookFor = FindCountryNameInList;

                    //check if the country entered is exact match for a country, else try to guess what was meant to be typed

                    if (matchedValue == valueToMatch)
                    {

                        RightCountryYesNo = "YES";
                        WhatCountryToLookFor = input;
                    }
                    else
                    {
                        Console.WriteLine("Did you mean the country of " + FindCountryNameInList + "?");
                        Console.WriteLine("Enter yes(y) or no(n):");
                        RightCountryYesNo = Console.ReadLine()!;
                    }

                    switch (RightCountryYesNo.ToUpper())
                    {
                        case "YES":
                        case "YE":
                        case "Y":
                            {

                                GetCurrencyType.CurrencyNames results = GetCurrencyType.MoneyType(WhatCountryToLookFor);

                                var apiRate = new ApiRate();
                                apiRate = await CurrencyRateProcessor.LoadRate(results.type);


                                // Print result of currency exchange rate
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("\nYou are converting from " + fromCurrency.ToUpper() + " to " + results.type.ToUpper());
                                Console.WriteLine(amount + " US Dollar" + " Equals " + apiRate.Result + " " + results.englishName + " as of date " + apiRate.Date + " Central European Time");
                                Console.ForegroundColor = ConsoleColor.White;
                                //Have some fun and check rate
                                //display method if strong or weak

                                switch (Decimal.Parse(apiRate.Result!))
                                {

                                    case < 1:
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("\nMaybe not the best time to travel to " + results.country + ". The dollar is weak!");
                                            Console.ForegroundColor = ConsoleColor.White;
                                            break;
                                        }

                                    case > 1:
                                        {
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.Write("");
                                            Console.WriteLine("\nGreat choice of travel to " + results.country + ". The dollar is STRONG $$");
                                            Console.ForegroundColor = ConsoleColor.White;
                                            break;
                                        }

                                    default:
                                        {
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.Write("");
                                            Console.WriteLine("\nGreat choice of travel to " + results.country + ". The dollar will break even");
                                            Console.ForegroundColor = ConsoleColor.White;
                                            break;
                                        }

                                }
                                break;
                            }
                        case "NO":
                        case "N":
                            {
                                Console.WriteLine("Please try again and be more specific with the country name");
                                break;
                            }
                        default:
                            break;
                    }
                }
                else
                {
                    if (input.ToUpper() == "STOP")
                    {
                        ContinueLookingForInput = false;
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nYou have typed in an invalid or misspelled country name.");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\nSome country names use & for and or St. for saint. For example St. Kitts & Nevis");
                        Console.WriteLine("Please try again");
                    }
                }
                Console.Write("\nPress <Enter> to continue... ");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                Console.Clear();

            }
            Console.WriteLine("Thank you for converting your money with us!");
        }
    }

}