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

    internal static PAPI_Lib PAPI_LibBuilder(
        PAPI_Lib papi_lib, PAPI_LibTemplate template, string libValue1, string libValue2 = "No Input.")
    {
        papi_lib.PAPI_LibTemplate = template.TemplateString;
        papi_lib.OriginalQuote = template.OriginalQuote;
        papi_lib.OriginalQuoteAuthorOrSource = template.OriginalQuoteAuthorOrSource;
        papi_lib.TemplateId = template.Id;
        papi_lib.CompletedString = (libValue2 == "No Input.") ? String.Format(
                template.TemplateString, libValue1) : String.Format(
                    template.TemplateString, libValue1, libValue2);
        papi_lib.ApiUrlOne = template.ApiUrlOne;
        papi_lib.ApiNameOne = template.ApiNameOne;
        papi_lib.ApiUrlTwo = template.ApiUrlTwo;
        papi_lib.ApiNameTwo = template.ApiNameTwo;

        return papi_lib;
    }

    /* (3/26/24, 1) Figured this out with Ernesto's help! */
    public static List<int> ArtworkIdList { get; set; } = new List<int>
    {
        16460,
        72520,
        207528,
        261876,
        259686,
        269278,
        270797,
        282161,
        344619,
        347567,
        907116,
        905064,
        889576,
        888476,
        853075,
        814822,
        815150,
        787967,
        782239,
        756448,
        751191,
        716307,
        698842,
        687539,
        569710,
        495324
    };
}
