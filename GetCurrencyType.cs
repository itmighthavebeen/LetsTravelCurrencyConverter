using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LetsTravelCurrencyConverter
{
    public class GetCurrencyType                     
    {
        public struct CurrencyNames
        {
            public string type;
            public string englishName;
            public string country;
        }
        public static CurrencyNames MoneyType(string Namein)        
        {
            CurrencyNames values = new();
            string cultureNameFromSpecificCulture = System.String.Empty;

            //need the regioninfo to get the English Currency Name
            var regions = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(x => new RegionInfo(x.Name));
            
            //need the specific culture info from CultureInfo to properly access the RegionInfo
            var specificCultures = System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.SpecificCultures);

            //country name matching on in specificCultures is enclosed in ( )s so must parse
            //search through to find the one that matches

            foreach (var culture in specificCultures)
            {

                string s = culture.EnglishName;
                int start = s.IndexOf("(") + 1;
                int end = s.IndexOf(")", start);
                string parsedCountryNameFromSpecificCultures = s.Substring(start, end - start);

                //if can find a matching culturename ie. en-US, use it
                if (Namein == parsedCountryNameFromSpecificCultures)
                {
                    cultureNameFromSpecificCulture = culture.Name;
                }
                
            }

            var englishRegion = regions.LastOrDefault(region => region.EnglishName.Contains(Namein));

            string? NameinEnglishCountryName = englishRegion.ToString();

            string NameToSearchWith = System.String.Empty;

            // Searches for the Currency Type for the given NameIn (EnglishCountry Name ie US) or (cultureName ie en-US) in RegionInfo
            //
            if (cultureNameFromSpecificCulture == "")
            {
               NameToSearchWith = NameinEnglishCountryName;
            }
            else
            {
                NameToSearchWith = cultureNameFromSpecificCulture;
            }

            RegionInfo myCurrencyType = new(NameToSearchWith);


            values.type = myCurrencyType.ISOCurrencySymbol;
            values.englishName = myCurrencyType.CurrencyEnglishName;
            values.country = myCurrencyType.EnglishName;
            
            return values;

        }  
    }       
}        

