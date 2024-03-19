/* (3/17/24, 16) AGAIN, used https://wtools.io/convert-json-to-csharp-class to "reverse-engineer" the JSON returned by 
one of the publicly-accessible APIs I found into a C# class. */
namespace PAPI_Libs.Models;

public class CorporateBuzzwords
{
    public string phrase { get; set; }
}