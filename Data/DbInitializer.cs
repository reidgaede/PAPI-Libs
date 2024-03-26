using PAPI_Libs.Models;

namespace PAPI_Libs.Data;

public static class DbInitializer
{
    public static void Initialize(PAPI_LibContext context)
    {
        if (context.PAPI_LibTemplates.Any())
        {
            return;   // DB has been seeded
        }

        var pAPI_LibTemplatesSeed = new PAPI_LibTemplate[]
        {
            new PAPI_LibTemplate
            {
                Id = 1,
                TemplateString = "When life gives you {0}, {1}.",
                OriginalQuote = "When life gives you lemons, make lemonade.",
                OriginalQuoteAuthorOrSource = "Common English Proverb",
                ApiUrlOne = "https://www.fruityvice.com/api/fruit/{0}",
                ApiNameOne = "Fruityvice API",
                ApiUrlTwo = "https://corporatebs-generator.sameerkumar.website/",
                ApiNameTwo = "Corporate Buzzword Generator API"
            },
            new PAPI_LibTemplate
            {
                Id = 2,
                TemplateString = "{0} makes the heart grow fonder.",
                OriginalQuote = "Absence makes the heart grow fonder.",
                OriginalQuoteAuthorOrSource = "Common English Proverb",
                ApiUrlOne = "https://binaryjazz.us/wp-json/genrenator/v1/genre/",
                ApiNameOne = "Genrenator API (`/genre/` Endpoint)"
            },
            new PAPI_LibTemplate
            {
                Id = 3,
                TemplateString = "As Jesus was walking beside the Sea of Galilee, he saw two brothers, Simon called Peter and his brother Andrew. They were casting a net into the lake, for they were fishermen. '{0}' Jesus said. At once they left their nets and followed him.",
                OriginalQuote = "As Jesus was walking beside the Sea of Galilee, he saw two brothers, Simon called Peter and his brother Andrew. They were casting a net into the lake, for they were fishermen. 'Come, follow me,' Jesus said. At once they left their nets and followed him.",
                OriginalQuoteAuthorOrSource = "Matthew 4:18-20",
                ApiUrlOne = "https://binaryjazz.us/wp-json/genrenator/v1/story/",
                ApiNameOne = "Genrenator API (`/story/` Endpoint)"
            },
            new PAPI_LibTemplate
            {
                Id = 4,
                TemplateString = "'{0}' is the Guide Which I Will Never Abandon.",
                OriginalQuote = "The Constitution is the Guide I Will Never Abandon",
                OriginalQuoteAuthorOrSource = "George Washington",
                ApiUrlOne = "https://gutendex.com/books/{0}",
                ApiNameOne = "Gutendex API"
            },
            new PAPI_LibTemplate
            {
                Id = 5,
                TemplateString = "I really believe that if you practice enough, you could paint '{0}' with a two-inch brush.",
                OriginalQuote = "I really believe that if you practice enough, you could paint the 'Mona Lisa' with a two-inch brush.",
                OriginalQuoteAuthorOrSource = "Bob Ross",
                ApiUrlOne = "https://collectionapi.metmuseum.org/public/collection/v1/objects/{0}",
                ApiNameOne = "The Metropolitan Museum of Art Collection API"
            },
        };

        var pAPI_LibsSeed = new PAPI_Lib[]
        {
            new PAPI_Lib
            {
                Id = 1,
                PAPI_LibTemplate = "When life gives you {fruit}, {corporateBs}.",
                OriginalQuote = "When life gives you lemons, make lemonade.",
                OriginalQuoteAuthorOrSource = "Common English Proverb",
                TemplateId = 1,
                CompletedString = "When life gives you durian, completely envisioneer frictionless sprints.",
                ApiUrlOne = "https://www.fruityvice.com/api/fruit/{0}",
                ApiNameOne = "Fruityvice API",
                ApiUrlTwo = "https://corporatebs-generator.sameerkumar.website/",
                ApiNameTwo = "Corporate Buzzword Generator API"
            },
            new PAPI_Lib
            {
                Id = 2,
                PAPI_LibTemplate = "{musicGenre} makes the heart grow fonder.",
                OriginalQuote = "Absence makes the heart grow fonder.",
                OriginalQuoteAuthorOrSource = "Common English Proverb",
                TemplateId = 2,
                CompletedString = "Hawaiian Cornet Revival makes the heart grow fonder.",
                ApiUrlOne = "https://binaryjazz.us/wp-json/genrenator/v1/genre/",
                ApiNameOne = "Genrenator API (`/genre/` Endpoint)",
            }
        };

        context.PAPI_LibTemplates.AddRange(pAPI_LibTemplatesSeed);
        context.PAPI_Libs.AddRange(pAPI_LibsSeed);
        context.SaveChanges();
    }
}
