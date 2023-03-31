using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsTravelCurrencyConverter
{
    internal class CreateListOfValidCountries
    {
     
        public static List<string> CountryList()
          
        {
            List<string> ListOfCountriesFromCultureInfo = new();
            
            CultureInfo[] getCultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            foreach (CultureInfo getCulture in getCultureInfo)
            {
                RegionInfo GetRegionInfo = new(getCulture.Name);

                //adding each county English Name into the stringlist of ListOfCountriesFromCultureInfo only once
               if (!ListOfCountriesFromCultureInfo.Contains(GetRegionInfo.EnglishName))
               {
                    ListOfCountriesFromCultureInfo.Add(GetRegionInfo.EnglishName );
                                 
                }
            }

            ListOfCountriesFromCultureInfo.Sort();

            return ListOfCountriesFromCultureInfo;
        }
    }
}
