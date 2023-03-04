using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LetsTravelCurrencyConverter
{
    internal class GetCurrencyType                     //Purpuse of this class is to get the Currency type 
                                                       //for a country passed as english name country
        {
        public string MoneyType(string Namein)        //Bring in name types by user on console ie United States
        {
            //
            //Need to use EnglishName entered by user and get 2character name ie United States is US
            //use 2 character name to get currency info
            //a better programmer would make an array of all this info to easily reference. get once - use many times. still learning
            //

            var regions = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(x => new RegionInfo(x.Name));
            var englishRegion = regions.FirstOrDefault(region => region.EnglishName.Contains(Namein));
            string Namein2 = englishRegion.ToString();
            // Searches for the Currency Type for the given NameIn (EnglishCountry Name)
            //
            RegionInfo myCurrencyType = new RegionInfo(Namein2);
              
            return myCurrencyType.ISOCurrencySymbol;
           
        }   //end MoneyType
    }       //end GetCurrencyType
}           //end namespace

