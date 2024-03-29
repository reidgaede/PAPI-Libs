using System.Text.Json;
using System.Text.Json.Serialization;
namespace PAPI_Libs.Models;

public class Person
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
        public List<Person> Authors { get; set; }

        [JsonPropertyName("translators")]
        public List<Person> Translators { get; set; }

        [JsonPropertyName("subjects")]
        public List<string> Subjects { get; set; }

        [JsonPropertyName("bookshelves")]
        public List<string> Bookshelves { get; set; }

        [JsonPropertyName("languages")]
        public List<string> Languages { get; set; }

        /* After consulting documentation available at https://gutendex.com/, making the `Copyright` 
        property nullable in the hope that it will prevent seemingly-random 404 codes from being returned 
        to the Swagger UI-based front-end when `POST` or `PUT` are called such that a `GET` call needs to 
        be made to the Gutendex API: */
        [JsonPropertyName("copyright")]
        public bool? Copyright { get; set; }

        [JsonPropertyName("media_type")]
        public string MediaType { get; set; }

        [JsonPropertyName("formats")]
        public Formats Formats { get; set; }

        [JsonPropertyName("download_count")]
        public int DownloadCount { get; set; }
    }
