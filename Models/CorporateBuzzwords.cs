/* (3/17/24, 16) AGAIN, used https://wtools.io/convert-json-to-csharp-class to "reverse-engineer" the JSON returned by 
one of the publicly-accessible APIs I found into a C# class. */
using System.Text.Json.Serialization;

namespace PAPI_Libs.Models;

public class CorporateBuzzwords
{
    /* (3/21/24, 7) EXPERIMENTING with the `[JsonPropertyName]` attribute to see if it MIGHT help me to make my models 
    more "C#" in that I can implement the proper naming convention for each class's properties (i.e., AT LEAST capitalize 
    the first letter of each class property): */
    [JsonPropertyName("phrase")]
    public string Phrase { get; set; }

    /* (3/21/24, 8) It APPEARS that this test HAS been successful (i.e., the `POST` action of the API DOES still work even 
    after this change was made to the `CorporateBuzzwords` class). I will test the API's `POST` action a few more times, and 
    IF there are no issues, I will see about implementing `[JsonPropertyName]` attributes for each member of a model meant 
    to help in deserializing API responses (!): */
}