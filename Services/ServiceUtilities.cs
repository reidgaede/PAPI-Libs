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
}
