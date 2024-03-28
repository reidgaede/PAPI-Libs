# PAPI-Libs - Capstone Project for [Code Kentucky](https://codekentucky.org/)'s August 2023 Software Development Cohort
PAPI-Libs is a CRUD API built using C#/.NET 8.0 and Entity Framework Core 8.0 that fetches random data from five publicly-accessible APIs (i.e., "PAPIs"; one of these APIs has two separate endpoints, both of which are used in this project) and interpolates them into one of five pre-defined, randomly-selected templates built from common proverbs or the musings of famous individuals (sort of like a Mad Lib, but deadpan (or just lame, depending on one's sense of humor)).

## Summary
PAPI-Libs is built around an ASP.NET Core Web API Project that uses a service (`PAPI_LibService`) to make `GET` requests to five publicly-accessible APIs whenever a `POST` or `PUT` request is made within the app by a user. Whenever a `POST` request is made, PAPI-Libs randomly chooses a `PAPI_LibTemplate` on which to build a `PAPI_Lib` object from the "PAPI_LibTemplates" table within the app's SQLite-structured database (built using Entity Framework Core). Within that template are API URLs, the original version of the quote/proverb to be modified, the author or source of said quote/proverb, and template strings formatted for modification via string interpolation and `String.Format()`.

With the PAPI-Lib template selected, one or more `GET` requests returning random, JSON-structured data are executed. The returned JSON objects are deserialized into models such as `Fruit` and `Book`, and the applicable "Name", "Title", etc. value from each object is interpolated into the template's previously-incomplete string, whereupon the finished `PAPI_Lib` is added to the database and made available for further querying, editing, or deletion via the app's Swagger-based UI or HttpRepl.

## APIs Used

| API | URL | Description |
| ----------- | ----------- | ----------- |
| Fruityvice API | https://www.fruityvice.com/ | Data regarding different kinds of fruit. |
| Corporate Buzzword Generator API | https://corporatebs-generator.sameerkumar.website/ | RESTful API that generates random expressions inundated with corporate buzzwords. |
| Genrenator API (`/genre/` Endpoint) | https://binaryjazz.us/genrenator-api/ | Returns a randomly-generated music genre. |
| Genrenator API (`/story/` Endpoint) | https://binaryjazz.us/genrenator-api/ | Returns a randomly-generated one-sentence story involving a random music genre. |
| Gutendex API | https://gutendex.com/ | Book data from [Project Gutenberg](https://www.gutenberg.org/). |
| The Metropolitan Museum of Art Collection API | https://metmuseum.github.io/ |  Information on more than 470,000 pieces of artwork in the possession of the Metropolitan Museum of Art. |

## `PAPI_Lib` Object Schema

| Property | Type | Nullable | Description |
| ----------- | ----------- | ----------- | ----------- |
| `Id` | Integer | No | Primary key; auto-increments by 1 with each record added |
| `PAPI_LibTemplate` | String | No | Version of string contained in `OriginalQuote` structured for data insertion via string interpolation or `String.Format()`|
| `OriginalQuote` | String | No | Quote or proverb to be "PAPI-Libbed" |
| `OriginalQuoteAuthorOrSource` | String | No | Author or source of quote or proverb to be "PAPI-Libbed" |
| `TemplateId` | Integer | No | Primary key of `PAPI_LibTemplate` from which each `PAPI_Lib` is built; particulary useful for `PUT` requests |
| `CompletedString` | String | Yes | Post-interpolation string (i.e., "the finished PAPI-Lib") |
| `ApiUrlOne` | String | No | URL through which `GET` request may be made to publicly-accessible API |
| `ApiNameOne` | String | No | Name of first publicly-accessible API providing data to build `PAPI_Lib` |
| `ApiUrlTwo` | String | Yes | URL through which `GET` request may be made to publicly-accessible API (if applicable) |
| `ApiNameTwo` | String | Yes | Name of second (if applicable) publicly-accessible API providing data to build `PAPI_Lib` |

## System Requirements and Recommendations

Prior installation of an IDE such as Visual Studio or an editor such as Visual Studio Code are recommended for interacting with PAPI-Libs' source code.

The app was built using .NET 8.0 and Entity Framework Core 8.0. As such, installation of these or newer versions of .NET and Entity Framework Core are recommened to ensure an optimized user experience.

Once these prerequisites are handled, one should simply need to clone this repository to their machine, open the repository in Visual Studio or Visual Studio Code, open a terminal, and input `dotnet run`. Shortly after doing so, a Swagger page should open in a web browser window, allowing the user to freely and easily interact with the PAPI-Libs API.

## Features Implemented
Though we were only required to implement three of the features listed in the capstone requirements document, I ultimately opted to implement four.

| Feature | Difficulty | Notes |
| ----------- | ----------- | ----------- |
| Create a dictionary or list, populate it with several values, retrieve at least one value, and use it in your program | Easy | E.g., please see `List<int> ArtworkIdList` in "ServiceUtilities.cs". |
| Make your application an API | Medium | Please see contents of "PAPI_LibController.cs" and "PAPI_LibService.cs" |
| Make your application a CRUD API | Medium/Hard | Please see contents of "PAPI_LibController.cs" and "PAPI_LibService.cs". |
| Make your application asynchronous | Medium | Please see method definitions in "PAPI_LibController.cs" and "PAPI_LibService.cs". |