using System.Text.Json;
using System.Text.Json.Serialization;
namespace PAPI_Libs.Models;

public class Author
    {
        public string name { get; set; }
        public int birth_year { get; set; }
        public int death_year { get; set; }
    }

    public class Formats
    {
        /* (3/18/24, 2) For what it's worth, I had ALMOST given up hope on most everything regarding this project seeing as how the JSON objects returned 
        from the "Gutendex" API were so strangely formatted (it would have been the straw that broke the camel's back). THAT SAID, when I made a last-ditch 
        effort at reverse-engineering a C# class from the JSON objects returned by the "Gutendex" API (using https://json2csharp.com/), I was returned the 
        contents of this file as seen now. The only modifications I made were the `using` and `namespace` statements at the top AND for every member in the 
        "Formats" class, to replace `JsonProperty` (which for some reason returned an error) with `JsonPropertyName` (no guarantee that this will ultimately 
        work, BUT it is worth a shot!): */
        [JsonPropertyName("text/html")]
        public string texthtml { get; set; }

        [JsonPropertyName("text/html; charsetutf-8")]
        public string texthtmlcharsetutf8 { get; set; }

        [JsonPropertyName("application/epub+zip")]
        public string applicationepubzip { get; set; }

        [JsonPropertyName("application/x-mobipocket-ebook")]
        public string applicationxmobipocketebook { get; set; }

        [JsonPropertyName("text/plain; charsetutf-8")]
        public string textplaincharsetutf8 { get; set; }

        [JsonPropertyName("application/rdf+xml")]
        public string applicationrdfxml { get; set; }

        [JsonPropertyName("image/jpeg")]
        public string imagejpeg { get; set; }

        [JsonPropertyName("application/octet-stream")]
        public string applicationoctetstream { get; set; }

        [JsonPropertyName("text/plain; charsetus-ascii")]
        public string textplaincharsetusascii { get; set; }
    }

    public class Book
    {
        public int id { get; set; }
        public string title { get; set; }
        public List<Author> authors { get; set; }
        public List<object> translators { get; set; }
        public List<string> subjects { get; set; }
        public List<string> bookshelves { get; set; }
        public List<string> languages { get; set; }
        public bool copyright { get; set; }
        public string media_type { get; set; }
        public Formats formats { get; set; }
        public int download_count { get; set; }
    }