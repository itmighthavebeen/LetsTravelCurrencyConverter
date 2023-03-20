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
                Console.WriteLine("\nHello. Let us convert your currency, \nPlease enter a full country name.");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nFor Ivory Coast, enter Ivoire. \nFor Sao Tome and Principe, enter Tom.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nWhich country are you traveling to?");
                Console.WriteLine("(Type STOP to quit)");

                var input = Console.ReadLine();
                string? caseString = input;

                caseString = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(caseString.ToLower());
                input = caseString;

                //
                //Validate that the Country exists in CountryList
                //
                if (listOfCountries.Any(x => x.Contains(input)) == true)

                {

                    GetCurrencyType.CurrencyNames results = GetCurrencyType.MoneyType(input);

                    var apiRate = new ApiRate();
                    string RightCountryYesNo = "NO";

                    apiRate = await CurrencyRateProcessor.LoadRate(results.type);

                    //added this code late in the game while checking all UN nations. Special characters causiing issues
                    if (listOfCountries.Contains(input) == true)
                    {
                        RightCountryYesNo = "YES";
                    }
                    else
                    {
                        Console.WriteLine("Did you mean the country of " + results.country + " ?");
                        Console.WriteLine("Enter yes or no:");
                        RightCountryYesNo = Console.ReadLine();
                    }

                    switch (RightCountryYesNo.ToUpper())
                    {
                        case "YES":
                            {

                                // Print result of currency exchange rate
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("\nYou are converting from " + fromCurrency.ToUpper() + " to " + results.type.ToUpper());
                                Console.WriteLine(amount + " US Dollar" + " Equals " + apiRate.Result + " " + results.englishName + " as of date " + apiRate.Date + " Central European Time");
                                Console.ForegroundColor = ConsoleColor.White;
                                //Have some fun and check rate
                                //display method if strong or weak

                                switch (Decimal.Parse(apiRate.Result))
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
                        ContinueLookingForInput = false;
                    else
                    {
                        Console.WriteLine("\nYou have typed in an invalid or misspelled country name.");
                        Console.WriteLine("\nSome country names use & for and or St. for saint. For example St. Kitts & Nevis");
                        Console.WriteLine("Please try again");
                    }
                }
            }
            Console.WriteLine("Thank you for converting your money with us!");
        }
    }

}