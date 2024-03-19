/* (3/17/24, 8) Adding-in these `using` statements I pulled from "APIGetTestController.cs" in an ATTEMPT to use `HttpClient` and the like to make requests 
to the publicly-accessible API from WITHIN this "service" file: */
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
/* (3/17/24, 13) Bringing this in to TRY and deserialize the responses we get from APIs (...): */
using System.Text.Json;

using PAPI_Libs.Models;

namespace PAPI_Libs.Services;

/* (3/17/24, 2) At this point, I consider most all the service-level infrastructure defined below to be more of a "proof-of-
concept" that allows me to build-out the core "business logic" underlying the app. Once I have the business logic more-or-less 
figured out, I will, of course, back-up my work via Git, but once that is done, I IMAGINE that a lot of what is here will be 
deleted OR OTHERWISE heavily edited: */
public static class PAPI_LibService
{
    static List<PAPI_LibTemplate> PAPI_LibTemplates { get; }
    static List<PAPI_Lib> PAPI_Libs { get; }
    static int nextId = 3;

    static PAPI_LibService()
    {
        PAPI_LibTemplates = new List<PAPI_LibTemplate>
        {
            new PAPI_LibTemplate
            {
                TemplateId = 1,
                TemplateString = "When life gives you {fruit}, {corporateBs}.",
                OriginalQuote = "When life gives you lemons, make lemonade.",
                OriginalQuoteAuthorOrSource = "Common English Proverb",
                /* (3/17/24, 10) Note the pre-inclusion of the `{id}` format item in this URL string so as to TRY to make use of `String.Format()` in place of 
                string interpolation: */
                ApiUrlOne = "https://www.fruityvice.com/api/fruit/{0}",
                ApiNameOne = "Fruityvice API",
                ApiUrlTwo = "https://corporatebs-generator.sameerkumar.website/",
                ApiNameTwo = "Corporate Buzzword Generator API"
            },
            new PAPI_LibTemplate
            {
                TemplateId = 2,
                TemplateString = "{musicGenre} makes the heart grow fonder.",
                OriginalQuote = "Absence makes the heart grow fonder.",
                OriginalQuoteAuthorOrSource = "Common English Proverb",
                ApiUrlOne = "https://binaryjazz.us/wp-json/genrenator/v1/genre/",
                ApiNameOne = "Genrenator API (`/genre/` endpoint)"
            },
            new PAPI_LibTemplate
            {
                TemplateId = 3,
                TemplateString = "As Jesus was walking beside the Sea of Galilee, he saw two brothers, Simon called Peter and his brother Andrew. They were casting a net into the lake, for they were fishermen. '{musicStory}' Jesus said. At once they left their nets and followed him.",
                OriginalQuote = "As Jesus was walking beside the Sea of Galilee, he saw two brothers, Simon called Peter and his brother Andrew. They were casting a net into the lake, for they were fishermen. 'Come, follow me,' Jesus said. At once they left their nets and followed him.",
                OriginalQuoteAuthorOrSource = "Matthew 4:18-20",
                ApiUrlOne = "https://binaryjazz.us/wp-json/genrenator/v1/story/",
                ApiNameOne = "Genrenator API (`/story/` endpoint)"
            },
            new PAPI_LibTemplate
            {
                TemplateId = 4,
                TemplateString = "{book} is the Guide Which I Will Never Abandon.",
                OriginalQuote = "The Constitution is the Guide I Will Never Abandon",
                OriginalQuoteAuthorOrSource = "George Washington",
                ApiUrlOne = "https://gutendex.com/books/",
                ApiNameOne = "Gutendex API"
            },
            new PAPI_LibTemplate
            {
                TemplateId = 5,
                TemplateString = "I really believe that if you practice enough, you could paint {artWork} with a two-inch brush.",
                OriginalQuote = "I really believe that if you practice enough, you could paint the 'Mona Lisa' with a two-inch brush.",
                OriginalQuoteAuthorOrSource = "Bob Ross",
                ApiUrlOne = "https://collectionapi.metmuseum.org/public/collection/v1/objects/",
                ApiNameOne = "The Metropolitan Museum of Art Collection API"
            },
        };

        PAPI_Libs = new List<PAPI_Lib>
        {
            new PAPI_Lib
            {
                Id = 1,
                PAPI_LibTemplate = "When life gives you {fruit}, {corporateBs}.",
                OriginalQuote = "When life gives you lemons, make lemonade.",
                OriginalQuoteAuthorOrSource = "Common English Proverb",
                TemplateId = 1,
                CompletedString = "When life gives you durian, completely envisioneer frictionless sprints.",
                ApiUrlOne = "https://www.fruityvice.com/api/fruit/",
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
                ApiNameOne = "Genrenator API (`/genre/` endpoint)",
            }
        };
    }

    public static List<PAPI_Lib> GetAll() => PAPI_Libs;

    public static PAPI_Lib? Get(int id) => PAPI_Libs.FirstOrDefault(p => p.Id == id);

    /* (3/17/24, 5) Intuition SUGGESTS to me that I SHOULD be able to remove the `PAPI_Lib papi_lib` parameter from this 
    method since I do NOT want my `POST` calls to accept ANY user input (...?) */
    public static void Add(PAPI_Lib papi_lib)
    {
        /* (3/17/24, 18) Random number generator, `switch` statement, and supporting infrastructure commented-out in (seemingly-vain) 
        hope of being able to test that AT LEAST one of the `switch` paths works (...): */
        Random numberGenerator = new Random();
        //int selectedTemplateId = numberGenerator.Next(0,5);
        int selectedTemplateId = 0;

        switch(selectedTemplateId)
        {
            case 0:
                PAPI_LibTemplate selectedTemplate = PAPI_LibTemplates[selectedTemplateId];

                int selectedFruitId = numberGenerator.Next(64, 73);

                /* (3/17/24, 14) I'm not even sure that I am doing things correctly at this point anymore... Where should I even DEFINE this `GET` method? How 
                can I be sure that I have correctly reverse-engineered the returned JSON into a class?*/
                [HttpGet("{id}")]
                async Task<ActionResult<string>> GetFruit(int id)
                {
                    /* (3/17/24, 9) Giving `String.Format()` a try in lieu of more-normal string interpolation due to concerns about being able to use 
                    string interpolation effectively in this context (consulted 
                    https://learn.microsoft.com/en-us/dotnet/fundamentals/runtime-libraries/system-string-format#insert-a-string for this idea): */
                    string url = String.Format(selectedTemplate.ApiUrlOne, id);
                    
                    /* (3/18/24, 6) Alright! It just so happens that when you set `Console.WriteLine()` statements like this, 
                    compile/run/whatever your application, open up HttpRepl in another terminal window, use it to issue a `POST`/
                    `post` command, complete said command (as described in your other notes/comments), and THEN check the "DEBUG 
                    CONSOLE" tab next to the "TERMINAL" tab in the terminal pane, it DOES appear that the URL used in this `GET` 
                    request IS being properly constructed! Way to go, bro (!): */
                    //Console.WriteLine($"Ready-to-Go API URL: {url}");
                    using (HttpClient client = new HttpClient())
                    {
                        /* (3/17/24, 12) PRESUMING that the data from this API ARE coming in as JSON: */
                        string fruityviceResponse = await client.GetStringAsync(url);

                        /* (3/18/24, 7) YES. YES. YES. Judging by the output in the "DEBUG CONSOLE" tab as described above when 
                        the `Console.WriteLine()` command below is active, it APPEARS that the above `GetStringAsync()` call IS 
                        functioning as desired by returning a JSON object as a string! This is HUGE progress! */
                        //Console.WriteLine($"String-Structured API Response: {fruityviceResponse}");

                        /* (3/17/24, 13) GEE DANGIT. I THINK that I will have to create models for EACH API response in order to make this work (...): */
                        Fruit fruit = JsonSerializer.Deserialize<Fruit>(fruityviceResponse);

                        Console.WriteLine($"`Fruit` object: {fruit}");
                        Console.WriteLine($"Value of `fruit.name`: {fruit.name}");
                        return fruit.name;
                    }
                }

                /* (3/17/24, 15) NO IDEA if this is what I am WANTING/TRYING to do, but AT LEAST it seems to PARTIALLY solve 
                the issue (?): */
                string fruit = GetFruit(selectedFruitId).ToString();

                [HttpGet]
                async Task<ActionResult<string>> GetCorporateBuzzwords()
                {
                    string url = selectedTemplate.ApiUrlTwo;
                    using (HttpClient client = new HttpClient())
                    {
                        string corporateBuzzwordsResponse = await client.GetStringAsync(url);
                        CorporateBuzzwords corporateBuzzwords = JsonSerializer.Deserialize<CorporateBuzzwords>(corporateBuzzwordsResponse);
                        return corporateBuzzwords.phrase;
                    }
                }

                string corporateBs = GetCorporateBuzzwords().ToString();

                /* (3/18/24, 5) BINGO. The `POST` functionality DOES work in a basic capacity! NOW to figure out why the heck 
                the data pulled from the publicly-availble APIs are NOT rendering/generating correctly (...): */
                papi_lib.Id = nextId++;
                papi_lib.PAPI_LibTemplate = selectedTemplate.TemplateString;
                papi_lib.OriginalQuote = selectedTemplate.OriginalQuote;
                papi_lib.OriginalQuoteAuthorOrSource = selectedTemplate.OriginalQuoteAuthorOrSource;
                papi_lib.TemplateId = selectedTemplate.TemplateId;
                papi_lib.CompletedString = $"When life gives you {fruit}, {corporateBs}.";
                papi_lib.ApiUrlOne = selectedTemplate.ApiUrlOne;
                papi_lib.ApiNameOne = selectedTemplate.ApiNameOne;
                papi_lib.ApiUrlTwo = selectedTemplate.ApiUrlTwo;
                papi_lib.ApiNameTwo = selectedTemplate.ApiNameTwo;

                PAPI_Libs.Add(papi_lib);
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }

    public static void Delete(int id)
    {
        var papi_lib = Get(id);
        if(papi_lib is null)
            return;

        PAPI_Libs.Remove(papi_lib);
    }

    public static void Update(PAPI_Lib papi_lib)
    {
        var index = PAPI_Libs.FindIndex(p => p.Id == papi_lib.Id);
        if(index == -1)
            return;

        PAPI_Libs[index] = papi_lib;
    }
}