# LetsTravelCurrencyConverter
CodeLouisville C# project - Converts Currency from Country Name to USD

This console app displays currency valuations to 1 US dollar.  Hints are given at runtime how to enter country names with 
special characters due to the limitations of windows terminal. Internet connection is needed to get results from this program.
The user is asked to enter a country name. A list of valid country names is dynamically obtained at the beginning of the program
from CultureInfo.  
Information about currency exchange rates is obtained from an API with url https://api.exchangerate.host/convert?from=USD&to=IRR. 
This example url is for the country of Iran.  The url needs the from 3 digit iso country code and the 3 digit iso to country code.
Date and rate are obtained from the url.
The program usees the country name entered by the user to find the 3 digit iso country code. Also obtains the english name of the 
to countries currency.

The program uses .net 7.0.


