# LetsTravelCurrencyConverter
CodeLouisville C# project - Converts Currency from Country Name to USD

This Windows console app displays currency valuations to 1 US dollar.  Hints are given at runtime how to enter country names with 
special characters due to the limitations of windows terminal. Internet connection is needed to get results from this program.
The user is asked to enter a country name. A list of valid country names is dynamically obtained at the beginning of the program
from CultureInfo class. The program checks for the country input to be in the created stringlist of countries so only valid input is processed.

Information about currency exchange rates is obtained from an API with url https://api.exchangerate.host/convert?from=USD&to=IRR.
This example url is for the country of Iran.  The url needs the from 3 digit iso country code and the 3 digit iso to country code.
Date and rate are obtained from the url.

The program usees the country name entered by the user to find the 3 character iso region name that is used to call the api. Also obtains the 
countries culturename from CultureInfo class to pass to RegionInfo inorder to obtain the proper currency name. 
The culture name is a combination of a two-letter lowercase language code and a two-letter uppercase country/region code, separated by a hyphen (-).
An example of culturename is en-US.
RegionInfo should not be read with a 2 character code such as US, some countries will return nuetral culture error.

The program uses .net 6.0.

Improvements to make on next release would be to incorporate more logic into methods for cleaner code.


