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
            List<string> CultureList = new();
            
            CultureInfo[] getCultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            foreach (CultureInfo getCulture in getCultureInfo)
            {
                RegionInfo GetRegionInfo = new(getCulture.Name);

                //adding each county English Name into the arraylist of CultureList
                if (!CultureList.Contains(GetRegionInfo.EnglishName))
                {
                    CultureList.Add(GetRegionInfo.EnglishName);
                }
            }

            CultureList.Sort();

            return CultureList;
        }
    }
}
