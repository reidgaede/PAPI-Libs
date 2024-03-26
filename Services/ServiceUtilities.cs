using System.Text.Json;
using PAPI_Libs.Models;

namespace PAPI_Libs;

public class ServiceUtilities
{
    public static string UppercaseFirst(string s)
    {
        return char.ToUpper(s[0]) + s.Substring(1);
    }

    internal static async Task<string> GetFruit(string url)
    {   
        using (HttpClient client = new HttpClient())
        {
            string fruityviceResponse = await client.GetStringAsync(url);
            Fruit fruit = JsonSerializer.Deserialize<Fruit>(fruityviceResponse);

            return fruit.Name;
        }
    }

    internal static async Task<string> GetCorporateBuzzwords(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            string corporateBuzzwordsResponse = await client.GetStringAsync(url);
            CorporateBuzzwords corporateBuzzwords = JsonSerializer.Deserialize<CorporateBuzzwords>(corporateBuzzwordsResponse);
            return corporateBuzzwords.Phrase;
        }
    }

    internal static async Task<string> GetGenre(string url)
    {   
        using (HttpClient client = new HttpClient())
        {
            return await client.GetStringAsync(url);
        }
    }

    internal static async Task<string> GetStory(string url)
    {   
        using (HttpClient client = new HttpClient())
        {
            return await client.GetStringAsync(url);
        }
    }

    internal static async Task<string> GetBook(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            string gutenedexResponse =  await client.GetStringAsync(url);

            Book book = JsonSerializer.Deserialize<Book>(gutenedexResponse);

            return book.Title;
        }
    }

    internal static async Task<string> GetArtwork(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            string collectionApiResponse =  await client.GetStringAsync(url);

            Artwork artWork = JsonSerializer.Deserialize<Artwork>(collectionApiResponse);

            return artWork.Title;
        }
    }

    internal static string FormatMusicStory(string musicStory)
    {
        musicStory = musicStory.Trim('"');
        string musicStoryFormatted = musicStory.Remove(musicStory.Length - 1);
        musicStoryFormatted += ",";
        return musicStoryFormatted;
    }

    /* (3/25/24, 1) STILL worthwhile to try adding this in for `case`s 2-5 in the `switch()` statements in the 
    `Add()` and `Update()` methods in "PAPI_LibService.cs"? THAT SAID, prioritize doing this AFTER you get the rest 
    of the project figured-out because in doing so, you will LIKELY have to update the seeded `PAPI_LibTemplate` data 
    that you have in "DbInitializer.cs" so that you can properly use `String.Format()` AND SUBSEQUENTLY use a migration to 
    update the database (?): */
    internal static PAPI_Lib PAPI_LibBuilder(PAPI_Lib papi_lib, PAPI_LibTemplate template, string libValue1, string libValue2 = "No Input.")
    {
        /* (3/26/24, 4) After manually modifying the `TemplateString` value of the FIRST `PAPI_LibTemplate` object in 
        "DbInitializer.cs" to account for the `String.Format()` invocations in this method, DELETING the "PAPI-Libs.db" database 
        altogether, and re-building/-running the application, it APPEARS that this is all working! NEXT STEP: TRY To make this 
        method work for the four other `case`s in the `switch()` statement within the `Add()` method's definition in 
        "PAPI_LibService.cs" (AFTER modifying the OTHER "seed" `PAPI_LibTemplate` objects in "DbInitializer.cs" accordingly so that they 
        are formatted to work with `String.Format()`, but BEFORE re-deleting the "PAPI-Libs.db" database so the values in the 
        "PAPI_LibTemplates" TABLE will be reset!): */
        papi_lib.PAPI_LibTemplate = template.TemplateString;
        papi_lib.OriginalQuote = template.OriginalQuote;
        papi_lib.OriginalQuoteAuthorOrSource = template.OriginalQuoteAuthorOrSource;
        papi_lib.TemplateId = template.Id;
        /* (3/26/24, 2) Fancy flying courtesy of info found at 
        https://www.tutlane.com/tutorial/csharp/csharp-ternary-operator-with-examples (?): */
        papi_lib.CompletedString = (libValue2 == "No Input.") ? String.Format(template.TemplateString, libValue1) : String.Format(template.TemplateString, libValue1, libValue2);
        //CompletedString = String.Format(templateString, libValue1, libValue2),
        papi_lib.ApiUrlOne = template.ApiUrlOne;
        papi_lib.ApiNameOne = template.ApiNameOne;
        papi_lib.ApiUrlTwo = template.ApiUrlTwo;
        papi_lib.ApiNameTwo = template.ApiNameTwo;

        return papi_lib;
    }
}
