using System.Text.Json;
using System.Text.Json.Serialization;
namespace PAPI_Libs.Models;

public class Author
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("birth_year")]
        public int? BirthYear { get; set; }

        [JsonPropertyName("death_year")]
        public int? DeathYear { get; set; }
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
        public string TextHtml { get; set; }

        [JsonPropertyName("text/html; charsetutf-8")]
        public string TextHtmlCharsetUtf8 { get; set; }

        [JsonPropertyName("application/epub+zip")]
        public string ApplicationepubZip { get; set; }

        [JsonPropertyName("application/x-mobipocket-ebook")]
        public string ApplicationXMobipocketEbook { get; set; }

        [JsonPropertyName("text/plain; charsetutf-8")]
        public string TextPlainCharsetUtf8 { get; set; }

        [JsonPropertyName("application/rdf+xml")]
        public string ApplicationRdfXml { get; set; }

        [JsonPropertyName("image/jpeg")]
        public string ImageJpeg { get; set; }

        [JsonPropertyName("application/octet-stream")]
        public string ApplicationOctetStream { get; set; }

        [JsonPropertyName("text/plain; charsetus-ascii")]
        public string TextPlainCharsetUsAscii { get; set; }
    }

    public class Book
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("authors")]
        public List<Author> Authors { get; set; }

        [JsonPropertyName("translators")]
        public List<object> Translators { get; set; }

        [JsonPropertyName("subjects")]
        public List<string> Subjects { get; set; }

        [JsonPropertyName("bookshelves")]
        public List<string> Bookshelves { get; set; }

        [JsonPropertyName("languages")]
        public List<string> Languages { get; set; }

        [JsonPropertyName("copyright")]
        public bool Copyright { get; set; }

        [JsonPropertyName("media_type")]
        public string MediaType { get; set; }

        [JsonPropertyName("formats")]
        public Formats Formats { get; set; }

        [JsonPropertyName("download_count")]
        public int Download_count { get; set; }
    }