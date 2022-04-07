# CovidSledilnik
Project contains very simple .NET web api that exposes the Slovenian Covid statistics.
API is protected with simple authorization, which can be modified in appsettings.json:
username: admin
password: indigolabs

API is working with the latest data avalible online from Slovenian webpage where we can find official statistics.
URL for file in use can be modified in appsettings.json.
API has two endpoints api/region/cases and api/region/lastweek.
Endpoint cases supports three query parameters: region, fromDate and toDate and returns:
- Date
- Region
- Number of active cases per day
- Number of vaccinated 1st
- Number of vaccinated 2nd
- Deceased to date

Avalible regions can be modified in appsettings.json.

On endpoint lastweek we get resultset that contains a list of regions with sum of active cases from the last week.