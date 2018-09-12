Using NDepend to analyze Geo Localization Project.

I created a lightweight system to localize places location that uses GoogleAPI maps and is possible store the searches with MySQL.

The Project is divided into 4 main layers.

Presentation, Services, Database and Tests.

functionalities

ASP.NET MVC site that displays a page containing a map at global extent, a text input field and submit button. 

On clicking the submit button, the page should use any text entered into the input field to look up the location 

using a geocoding service and display a pin on the map for the top result.  The search term and location information will be sent to the server and logged in a text file.

NDepend First Analisys

NDepend Report

[![logo](https://github.com/OsvaldoMartini/NDepend-Architecture-Analisys/blob/master/NDepend_Report.png)](http://www.wservices.co.uk/geolocalization/NDependOut/NDependReport.html)

DASHBOARD:

![logo](https://github.com/OsvaldoMartini/NDepend-Architecture-Analisys/blob/master/Dashboard_first_analisys.PNG)

QUERIES AND RULES

![logo](https://github.com/OsvaldoMartini/NDepend-Architecture-Analisys/blob/master/Queries_and_Rules.PNG)


 Notes:
•         Use google maps JavaScript API v3 and google maps geocoder
•         Use C# on the server side
•         Appropriate warning messages are displayed to the user where required
 Bonus:
•         Zoom the map to an appropriate extent when displaying the pin
•         Display any additional information from the geocoder in a popup when the pin is clicked on
•         Display a confirmation when the results have been saved to the server

Adicional Tools:
NDepend

JQGrid to display data saved in a Grid.

Some Extra functions:

Logs in to the system, that allows to access "Places Searches" stored in the Database.

Hover above When you click the Pin of the saved location, you will be presented with basic information about the Location (Markers).

The system is complete and the purpose of this repository is to use NDepend to analyze, identify bugs, measure and improve the architecture. 

With Ndepend it will be possible to track problems and know the effort to correct.

NDepend provides for the team a differentiated vision of the project that allows aplication of new strategies for correction and creation of new functionalities.

Some Classes were left incomplete, as an example "CustomActionFilter.cs", with the intention of using NDepend to identify the weaknesses to be improved.

I will be inserting the analyzes performed with NDepend and the appropriate implementations with the purpose of correcting and improving the architecture.

GOOGLE API

[![logo](https://github.com/OsvaldoMartini/NDepend-Architecture-Analisys/blob/master/GoogleAPI.png)](https://developers.google.com/maps/documentation/javascript/get-api-key)
