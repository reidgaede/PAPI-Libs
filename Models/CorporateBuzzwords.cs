/* (3/17/24, 16) Again, used https://wtools.io/convert-json-to-csharp-class to "reverse-engineer" the JSON returned by 
one of the publicly-accessible APIs I found into a C# class. */
using System.Text.Json.Serialization;

namespace PAPI_Libs.Models;

public class CorporateBuzzwords
{
    [JsonPropertyName("phrase")]
    public string Phrase { get; set; }
}