using System;
using System.Collections.Generic;
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
           
            var regions = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(x => new RegionInfo(x.Name));
            var englishRegion = regions.FirstOrDefault(region => region.EnglishName.Contains(Namein));

            string? NameinEnglishCountryName = englishRegion.ToString();

            // Searches for the Currency Type for the given NameIn (EnglishCountry Name)
            //
            RegionInfo myCurrencyType = new (NameinEnglishCountryName);

            values.type = myCurrencyType.ISOCurrencySymbol;
            values.englishName = myCurrencyType.CurrencyEnglishName;
            values.country = myCurrencyType.EnglishName;
            return values;

        }  
    }       
}        

