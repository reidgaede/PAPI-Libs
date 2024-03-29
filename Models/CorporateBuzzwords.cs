/* Used https://wtools.io/convert-json-to-csharp-class to reverse-engineer the JSON returned by
this publicly-accessible API into a C# class. */
using System.Text.Json.Serialization;

namespace PAPI_Libs.Models;

public class CorporateBuzzwords
{
    [JsonPropertyName("phrase")]
    public string Phrase { get; set; }
}
