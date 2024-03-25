using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
/* (3/17/24, 13) Bringing this in to try and deserialize the responses we get from APIs: */
using System.Text.Json;
using PAPI_Libs.Models;
using System.Security.Cryptography.X509Certificates;
/* (3/24/24, 7) Adding-in code per instructions/guidance available at 
https://learn.microsoft.com/en-us/training/modules/persist-data-ef-core/4-interacting-data: */
using PAPI_Libs.Data;
using Microsoft.EntityFrameworkCore;


namespace PAPI_Libs.Services;

/* (3/17/24, 2) At this point, I consider most all the service-level infrastructure defined below to be more of a "proof-of-
concept" that allows me to build-out the core "business logic" underlying the app. Once I have the business logic more-or-less 
figured out, I will, of course, back-up my work via Git, but once that is done, I IMAGINE that a lot of what is here will be 
deleted OR OTHERWISE heavily edited: */
public class PAPI_LibService
{
    /* (3/24/24, 35) HOW do I deal with the twin issues of the `PAPI_LibTemplates` List and the `nextId` variable?! */
    //static List<PAPI_LibTemplate> PAPI_LibTemplates { get; }
    //static int nextId = 3;

    //static List<PAPI_Lib> PAPI_Libs { get; }

    /* (3/24/24, 7) Adding-in code per instructions/guidance available at 
    https://learn.microsoft.com/en-us/training/modules/persist-data-ef-core/4-interacting-data: */
    private readonly PAPI_LibContext _context;

    /* (3/24/24, 8) Removing the `static` modifier from in front of the constructor and replacing it 
    with the `public` access modifier so that the code is more in-accord with the contents of 
    "PizzaService.cs" as shown in the "Code the CRUD operations" section of the content available at 
    https://learn.microsoft.com/en-us/training/modules/persist-data-ef-core/4-interacting-data: */
    public PAPI_LibService(PAPI_LibContext context)
    {
        _context = context;

        /* (3/24/24, 9) Commenting-out the "hard-seeded" `PAPI_LibTemplates` and `PAPI_Lib` objects I 
        PREVIOUSLY used when trying to get this Web API to work at a basic level (after all, IF things go 
        "according to plan", I SHOULD (hopefully?) be able to "seed" (correct term?) each `PAPI_LibTemplate` 
        object in the "PAPI_LibTemplates" TABLE in the database and subsequently use them to build `PAPI_Lib` 
        objects (?)): */
        /* PAPI_LibTemplates = new List<PAPI_LibTemplate>
        {
            new PAPI_LibTemplate
            {
                Id = 1,
                TemplateString = "When life gives you {fruitFormatted}, {corporateBsFormatted}.",
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
                TemplateString = "{musicGenreFormatted} makes the heart grow fonder.",
                OriginalQuote = "Absence makes the heart grow fonder.",
                OriginalQuoteAuthorOrSource = "Common English Proverb",
                ApiUrlOne = "https://binaryjazz.us/wp-json/genrenator/v1/genre/",
                ApiNameOne = "Genrenator API (`/genre/` Endpoint)"
            },
            new PAPI_LibTemplate
            {
                Id = 3,
                TemplateString = "As Jesus was walking beside the Sea of Galilee, he saw two brothers, Simon called Peter and his brother Andrew. They were casting a net into the lake, for they were fishermen. '{musicStoryFormatted}' Jesus said. At once they left their nets and followed him.",
                OriginalQuote = "As Jesus was walking beside the Sea of Galilee, he saw two brothers, Simon called Peter and his brother Andrew. They were casting a net into the lake, for they were fishermen. 'Come, follow me,' Jesus said. At once they left their nets and followed him.",
                OriginalQuoteAuthorOrSource = "Matthew 4:18-20",
                ApiUrlOne = "https://binaryjazz.us/wp-json/genrenator/v1/story/",
                ApiNameOne = "Genrenator API (`/story/` Endpoint)"
            },
            new PAPI_LibTemplate
            {
                Id = 4,
                TemplateString = "'{book}' is the Guide Which I Will Never Abandon.",
                OriginalQuote = "The Constitution is the Guide I Will Never Abandon",
                OriginalQuoteAuthorOrSource = "George Washington",
                ApiUrlOne = "https://gutendex.com/books/{0}",
                ApiNameOne = "Gutendex API"
            },
            new PAPI_LibTemplate
            {
                Id = 5,
                TemplateString = "I really believe that if you practice enough, you could paint '{artWork}' with a two-inch brush.",
                OriginalQuote = "I really believe that if you practice enough, you could paint the 'Mona Lisa' with a two-inch brush.",
                OriginalQuoteAuthorOrSource = "Bob Ross",
                ApiUrlOne = "https://collectionapi.metmuseum.org/public/collection/v1/objects/{0}",
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
        }; */
    }

    /* (3/24/24, 10) ORIGINAL code for `GetAll()` method commented-out immediately below this comment: */
    //public static List<PAPI_Lib> GetAll() => PAPI_Libs;

    /* (3/24/24, 11) NEW code for `GetAll()` method (adapted from code shown in step #2 in the "Code the 
    CRUD operations" section of the instructions/guidance available from 
    https://learn.microsoft.com/en-us/training/modules/persist-data-ef-core/4-interacting-data): */
    public IEnumerable<PAPI_Lib> GetAll()
    {
        return _context.PAPI_Libs
        .AsNoTracking()
        .ToList();
    }

    /* (3/24/24, 11) ORIGINAL code for `Get()` method commented-out immediately below this comment: */
    //public static PAPI_Lib? Get(int id) => PAPI_Libs.FirstOrDefault(p => p.Id == id);

    /* (3/24/24, 12) NEW code for `GetById()` method (adapted from code shown in step #3 in the "Code the 
    CRUD operations" section of the instructions/guidance available from 
    https://learn.microsoft.com/en-us/training/modules/persist-data-ef-core/4-interacting-data): */
    public PAPI_Lib? GetById(int id)
    {
        /* (3/24/24, 13) No idea as to whether or not all these `.Include()` clauses are necessary, but 
        leaving them in there for the time being out of an abundance of caution:  */
        return _context.PAPI_Libs
            /* (3/24/24, 36) Ended up commenting-out all of this in order to get the `GetById()` method to "work" 
            on the front-end (!): */
            /* .Include(p => p.Id)
            .Include(p => p.PAPI_LibTemplate)
            .Include(p => p.OriginalQuote)
            .Include(p => p.OriginalQuoteAuthorOrSource)
            .Include(p => p.TemplateId)
            .Include(p => p.CompletedString)
            .Include(p => p.ApiUrlOne)
            .Include(p => p.ApiNameOne)
            .Include(p => p.ApiUrlTwo)
            .Include(p => p.ApiNameTwo) */
            .AsNoTracking()
            .SingleOrDefault(p => p.Id == id);
    }

    /* (3/24/24, 14) ORIGINAL "signature" for `Add()` method commented-out immediately below this comment: */
    // public static async Task Add(PAPI_Lib papi_lib)

    /* (3/24/24, 15) NEW code for `Add()` method (adapted from code shown in step #4 in the "Code the 
    CRUD operations" section of the instructions/guidance available from 
    https://learn.microsoft.com/en-us/training/modules/persist-data-ef-core/4-interacting-data): */
    public async Task Add(PAPI_Lib papi_lib)
    {
        Random numberGenerator = new Random();
        int selectedTemplateId = numberGenerator.Next(1, 6);

        switch(selectedTemplateId)
        {
            case 1:
                /* (3/24/24, 37) PLEASE let this implementation throughout the various `case`s get the `Add()` method 
                working (...): */
                //PAPI_LibTemplate selectedTemplate1 = PAPI_LibTemplates[selectedTemplateId];
                PAPI_LibTemplate selectedTemplate1 = _context.PAPI_LibTemplates.Find(selectedTemplateId);

                int selectedFruitId = numberGenerator.Next(64, 73);

                /* (3/19/24, 1) Note the changes to the method signature of `GetFruit()` (it would DEFINITELY behoove you to 
                brush back up at some point on your knowledge of `async`/`await`): */
                async Task<string> GetFruit(int id)
                {
                    string url = String.Format(selectedTemplate1.ApiUrlOne, id);
                    
                    //Console.WriteLine($"Ready-to-Go API URL: {url}");
                    // CHECK THE VERSION OF THIS FILE ON GITHUB TO SEE EXACTLY WHAT YOU REMOVED/CHANGED!!!
                    using (HttpClient client = new HttpClient())
                    {
                        string fruityviceResponse = await client.GetStringAsync(url);
                        //Console.WriteLine($"String-Structured API Response: {fruityviceResponse}");

                        /* (3/17/24, 13) Dang... Looks like I will have to create models for EACH API response in order to 
                        make this work: */
                        Fruit fruit = JsonSerializer.Deserialize<Fruit>(fruityviceResponse);

                        //Console.WriteLine($"`Fruit` object: {fruit}");
                        //Console.WriteLine($"Value of `fruit.name`: {fruit.name}");
                        return fruit.Name;
                    }
                }

                /* (3/19/24, 6) You were right! This WAS where the problem was taking place in your `POST` action (MOSTLY 
                because you had not used `await` for the `async`-defined method `GetFruit()` (see your notes/screenshots 
                for full(er) details)): */
                string fruit = await GetFruit(selectedFruitId);
                string fruitFormatted = fruit.ToLower();

                /* (3/19/24, 3) NOTE AS WELL the changes you HAD to make to the `GetCorporateBuzzwords()` method in order for 
                it to be PROPERLY "asynchronous": */
                async Task<string> GetCorporateBuzzwords()
                {
                    string url = selectedTemplate1.ApiUrlTwo;
                    using (HttpClient client = new HttpClient())
                    {
                        string corporateBuzzwordsResponse = await client.GetStringAsync(url);
                        CorporateBuzzwords corporateBuzzwords = JsonSerializer.Deserialize<CorporateBuzzwords>(corporateBuzzwordsResponse);
                        return corporateBuzzwords.Phrase;
                    }
                }

                string corporateBs = await GetCorporateBuzzwords();
                string corporateBsFormatted = corporateBs.ToLower();

                //papi_lib.Id = nextId++;
                papi_lib.PAPI_LibTemplate = selectedTemplate1.TemplateString;
                papi_lib.OriginalQuote = selectedTemplate1.OriginalQuote;
                papi_lib.OriginalQuoteAuthorOrSource = selectedTemplate1.OriginalQuoteAuthorOrSource;
                papi_lib.TemplateId = selectedTemplate1.Id;
                papi_lib.CompletedString = $"When life gives you {fruitFormatted}, {corporateBsFormatted}.";
                papi_lib.ApiUrlOne = selectedTemplate1.ApiUrlOne;
                papi_lib.ApiNameOne = selectedTemplate1.ApiNameOne;
                papi_lib.ApiUrlTwo = selectedTemplate1.ApiUrlTwo;
                papi_lib.ApiNameTwo = selectedTemplate1.ApiNameTwo;

                /* (3/24/24, 16) NOTE that all things considered, it SEEMS (?!) like the changeover from how you had things 
                structured to a project that integrates EF Core IS rather simple in regard to modifying the methods in the 
                applicable service! Note how immediately below this comment, we are simply commenting-out the original code 
                we had in this method and replacing it with `_context.PAPI_Libs.Add(papi_lib);` (adapted from code shown in 
                step #5 in the "Code the CRUD operations" section of the instructions/guidance available from 
                https://learn.microsoft.com/en-us/training/modules/persist-data-ef-core/4-interacting-data); NOTE AS WELL 
                that in order for this code change to work, you had to remove the `static` modifier from the method signature 
                for the `Add()` method: */
                //PAPI_Libs.Add(papi_lib);
                _context.PAPI_Libs.Add(papi_lib);
                _context.SaveChanges();
                break;
            case 2:
                /* (3/24/24, 37) PLEASE let this implementation throughout the various `case`s get the `Add()` method 
                working (...): */
                //PAPI_LibTemplate selectedTemplate2 = PAPI_LibTemplates[selectedTemplateId];
                PAPI_LibTemplate selectedTemplate2 = _context.PAPI_LibTemplates.Find(selectedTemplateId);

                async Task<string> GetGenre()
                {
                    string url = selectedTemplate2.ApiUrlOne;
                    
                    using (HttpClient client = new HttpClient())
                    {
                        return await client.GetStringAsync(url);
                    }
                }

                string musicGenre = await GetGenre();
                /* (3/20/24, 2) The advice available at 
                https://stackoverflow.com/questions/42964150/how-to-remove-first-and-last-character-of-a-string-in-c was 
                quite helpful in figuring out how to EASILY remove the quotation marks around the strings returned from 
                the Genrenator API's `/genre/` endpoint: */
                musicGenre = musicGenre.Trim('"');
                /* (3/20/24, 3) Could PROBABLY be considered rather lazy of me, but defined the `UppercaseFirst()` 
                method at the bottom of this service class based on code obtained from 
                https://www.dotnetperls.com/uppercase-first-letter. I would call this "lazy" in that I PROBABLY should've 
                defined the method in a class named, for example, "Utilities.cs", but granted that (to my current knowledge) 
                I will ONLY be using such a method within my PAPI_Lib service, I suppose it WOULD make sense just to define 
                it here (...?): */
                string musicGenreFormatted = UppercaseFirst(musicGenre);

                //papi_lib.Id = nextId++;
                papi_lib.PAPI_LibTemplate = selectedTemplate2.TemplateString;
                papi_lib.OriginalQuote = selectedTemplate2.OriginalQuote;
                papi_lib.OriginalQuoteAuthorOrSource = selectedTemplate2.OriginalQuoteAuthorOrSource;
                papi_lib.TemplateId = selectedTemplate2.Id;
                papi_lib.CompletedString = $"{musicGenreFormatted} makes the heart grow fonder.";
                papi_lib.ApiUrlOne = selectedTemplate2.ApiUrlOne;
                papi_lib.ApiNameOne = selectedTemplate2.ApiNameOne;

                /* (3/24/24, 17) NOTE that all things considered, it SEEMS (?!) like the changeover from how you had things 
                structured to a project that integrates EF Core IS rather simple in regard to modifying the methods in the 
                applicable service! Note how immediately below this comment, we are simply commenting-out the original code 
                we had in this method and replacing it with `_context.PAPI_Libs.Add(papi_lib);` (adapted from code shown in 
                step #5 in the "Code the CRUD operations" section of the instructions/guidance available from 
                https://learn.microsoft.com/en-us/training/modules/persist-data-ef-core/4-interacting-data); NOTE AS WELL 
                that in order for this code change to work, you had to remove the `static` modifier from the method signature 
                for the `Add()` method: */
                //PAPI_Libs.Add(papi_lib);
                _context.PAPI_Libs.Add(papi_lib);
                _context.SaveChanges();
                break;
            case 3:
                /* (3/24/24, 37) PLEASE let this implementation throughout the various `case`s get the `Add()` method 
                working (...): */
                //PAPI_LibTemplate selectedTemplate3 = PAPI_LibTemplates[selectedTemplateId];
                PAPI_LibTemplate selectedTemplate3 = _context.PAPI_LibTemplates.Find(selectedTemplateId);

                async Task<string> GetStory()
                {
                    string url = selectedTemplate3.ApiUrlOne;
                    
                    using (HttpClient client = new HttpClient())
                    {
                        return await client.GetStringAsync(url);
                    }
                }

                string musicStory = await GetStory();
                /* (3/20/24, 4) Cleaning up the response obtained from the Genrenator API's `/story/` endpoint: */
                musicStory = musicStory.Trim('"');
                /* (3/20/24, 5) Consulted https://www.w3schools.blog/how-to-replace-last-character-of-string-in-c for a 
                refresher on removing the last character in a string in C#: */
                string musicStoryFormatted = musicStory.Remove(musicStory.Length - 1);
                musicStoryFormatted += ",";

                //papi_lib.Id = nextId++;
                papi_lib.PAPI_LibTemplate = selectedTemplate3.TemplateString;
                papi_lib.OriginalQuote = selectedTemplate3.OriginalQuote;
                papi_lib.OriginalQuoteAuthorOrSource = selectedTemplate3.OriginalQuoteAuthorOrSource;
                papi_lib.TemplateId = selectedTemplate3.Id;
                papi_lib.CompletedString = $"As Jesus was walking beside the Sea of Galilee, he saw two brothers, Simon called Peter and his brother Andrew. They were casting a net into the lake, for they were fishermen. '{musicStoryFormatted}' Jesus said. At once they left their nets and followed him.";
                papi_lib.ApiUrlOne = selectedTemplate3.ApiUrlOne;
                papi_lib.ApiNameOne = selectedTemplate3.ApiNameOne;

                /* (3/24/24, 18) NOTE that all things considered, it SEEMS (?!) like the changeover from how you had things 
                structured to a project that integrates EF Core IS rather simple in regard to modifying the methods in the 
                applicable service! Note how immediately below this comment, we are simply commenting-out the original code 
                we had in this method and replacing it with `_context.PAPI_Libs.Add(papi_lib);` (adapted from code shown in 
                step #5 in the "Code the CRUD operations" section of the instructions/guidance available from 
                https://learn.microsoft.com/en-us/training/modules/persist-data-ef-core/4-interacting-data); NOTE AS WELL 
                that in order for this code change to work, you had to remove the `static` modifier from the method signature 
                for the `Add()` method: */
                //PAPI_Libs.Add(papi_lib);
                _context.PAPI_Libs.Add(papi_lib);
                _context.SaveChanges();
                break;
            case 4:
                /* (3/24/24, 37) PLEASE let this implementation throughout the various `case`s get the `Add()` method 
                working (...): */
                //PAPI_LibTemplate selectedTemplate4 = PAPI_LibTemplates[selectedTemplateId];
                PAPI_LibTemplate selectedTemplate4 = _context.PAPI_LibTemplates.Find(selectedTemplateId);

                int selectedBookId = numberGenerator.Next(1, 1001);

                async Task<string> GetBook(int id)
                {
                    string url = String.Format(selectedTemplate4.ApiUrlOne, id);
                    
                    using (HttpClient client = new HttpClient())
                    {
                        string gutenedexResponse =  await client.GetStringAsync(url);

                        Book book = JsonSerializer.Deserialize<Book>(gutenedexResponse);

                        //Console.WriteLine($"`Fruit` object: {fruit}");
                        //Console.WriteLine($"Value of `fruit.name`: {fruit.name}");
                        return book.Title;
                    }
                }

                string book = await GetBook(selectedBookId);

                //papi_lib.Id = nextId++;
                papi_lib.PAPI_LibTemplate = selectedTemplate4.TemplateString;
                papi_lib.OriginalQuote = selectedTemplate4.OriginalQuote;
                papi_lib.OriginalQuoteAuthorOrSource = selectedTemplate4.OriginalQuoteAuthorOrSource;
                papi_lib.TemplateId = selectedTemplate4.Id;
                papi_lib.CompletedString = $"'{book}' is the Guide Which I Will Never Abandon.";
                papi_lib.ApiUrlOne = selectedTemplate4.ApiUrlOne;
                papi_lib.ApiNameOne = selectedTemplate4.ApiNameOne;

                /* (3/24/24, 19) NOTE that all things considered, it SEEMS (?!) like the changeover from how you had things 
                structured to a project that integrates EF Core IS rather simple in regard to modifying the methods in the 
                applicable service! Note how immediately below this comment, we are simply commenting-out the original code 
                we had in this method and replacing it with `_context.PAPI_Libs.Add(papi_lib);` (adapted from code shown in 
                step #5 in the "Code the CRUD operations" section of the instructions/guidance available from 
                https://learn.microsoft.com/en-us/training/modules/persist-data-ef-core/4-interacting-data); NOTE AS WELL 
                that in order for this code change to work, you had to remove the `static` modifier from the method signature 
                for the `Add()` method: */
                //PAPI_Libs.Add(papi_lib);
                _context.PAPI_Libs.Add(papi_lib);
                _context.SaveChanges();
                break;
            case 5:
                /* (3/24/24, 37) PLEASE let this implementation throughout the various `case`s get the `Add()` method 
                working (...): */
                //PAPI_LibTemplate selectedTemplate5 = PAPI_LibTemplates[selectedTemplateId];
                PAPI_LibTemplate selectedTemplate5 = _context.PAPI_LibTemplates.Find(selectedTemplateId);

                /* (3/21/24, 2) The Metropolitan Museum of Art's Collection API is structured such that items with similar 
                (or sometimes the exact same) names are stored right next to each other, meaning that if, for example, an 
                object from the API with an ID value of 2447 were selected and then another object from the API with an 
                ID value with an ID vlaue of 2452 were selected, they would likely have very similar name/title values. 
                Compounding the difficulty is that there are numerous gaps in ID values among the objects in the Collection 
                API, meaning that simply generating a random integer and appending it to a URL string to make the API `GET` 
                request will almost certainly result in a bad request. As such, I have pre-selected ~25 objects (represented 
                here via their ID value) that may be used to "randomly" generate a response back from this API: */
                List<int> idList = new List<int>
                {
                    16460,
                    72520,
                    207528,
                    261876,
                    259686,
                    269278,
                    270797,
                    282161,
                    344619,
                    347567,
                    907116,
                    905064,
                    889576,
                    888476,
                    853075,
                    814822,
                    815150,
                    787967,
                    782239,
                    756448,
                    751191,
                    716307,
                    698842,
                    687539,
                    569710,
                    495324
                };

                /* (3/21/24, 3) Consulted https://stackoverflow.com/questions/2019417/how-to-access-random-item-in-list 
                to try and make this work: */
                int randomIndex = numberGenerator.Next(idList.Count);
                int selectedArtworkId = idList[randomIndex];

                async Task<string> GetArtwork(int id)
                {
                    string url = String.Format(selectedTemplate5.ApiUrlOne, id);
                    
                    using (HttpClient client = new HttpClient())
                    {
                        string collectionApiResponse =  await client.GetStringAsync(url);

                        Artwork artWork = JsonSerializer.Deserialize<Artwork>(collectionApiResponse);

                        return artWork.Title;
                    }
                }

                string artWork = await GetArtwork(selectedArtworkId);

                //papi_lib.Id = nextId++;
                papi_lib.PAPI_LibTemplate = selectedTemplate5.TemplateString;
                papi_lib.OriginalQuote = selectedTemplate5.OriginalQuote;
                papi_lib.OriginalQuoteAuthorOrSource = selectedTemplate5.OriginalQuoteAuthorOrSource;
                papi_lib.TemplateId = selectedTemplate5.Id;
                papi_lib.CompletedString = $"I really believe that if you practice enough, you could paint '{artWork}' with a two-inch brush.";
                papi_lib.ApiUrlOne = selectedTemplate5.ApiUrlOne;
                papi_lib.ApiNameOne = selectedTemplate5.ApiNameOne;

                /* (3/24/24, 20) NOTE that all things considered, it SEEMS (?!) like the changeover from how you had things 
                structured to a project that integrates EF Core IS rather simple in regard to modifying the methods in the 
                applicable service! Note how immediately below this comment, we are simply commenting-out the original code 
                we had in this method and replacing it with `_context.PAPI_Libs.Add(papi_lib);` (adapted from code shown in 
                step #5 in the "Code the CRUD operations" section of the instructions/guidance available from 
                https://learn.microsoft.com/en-us/training/modules/persist-data-ef-core/4-interacting-data); NOTE AS WELL 
                that in order for this code change to work, you had to remove the `static` modifier from the method signature 
                for the `Add()` method: */
                //PAPI_Libs.Add(papi_lib);
                _context.PAPI_Libs.Add(papi_lib);
                _context.SaveChanges();
                break;
        }
    }

    /* (3/24/24, 21) REMOVED the `static` modifier in the method signature for `Delete()` below so that the `GetById()` 
    invocation within the method definition would SEEMINGLY work (?!): */
    public void Delete(int id)
    {
        /* (3/24/24, 29) Commenting out the old code for the `Delete()` method and replacing it with cannibalized code from 
        step #7 in the "Code the CRUD operations" section of the instructions/guidance available from 
        https://learn.microsoft.com/en-us/training/modules/persist-data-ef-core/4-interacting-data): */
        /* var papi_lib = GetById(id);
        if(papi_lib is null)
            return;

        PAPI_Libs.Remove(papi_lib); */

        var pAPI_LibToDelete = _context.PAPI_Libs.Find(id);
        if (pAPI_LibToDelete is not null)
        {
            _context.PAPI_Libs.Remove(pAPI_LibToDelete);
            _context.SaveChanges();
        }        
    }

    /* (3/24/24, 21) PREEMPTIVELY REMOVED the `static` modifier in the method signature for `Update()` because thus far, it 
    seems like ALMOST anything in this file prefaced by `static` is generating errors as I progressively try to make these 
    changes: */
    public async Task Update(PAPI_Lib papi_lib)
    {
        /* (3/24/24, 22) Commenting out the old code for the `Update()` method and replacing it with cannibalized code from 
        step #6 in the "Code the CRUD operations" section of the instructions/guidance available from 
        https://learn.microsoft.com/en-us/training/modules/persist-data-ef-core/4-interacting-data): */
        /* var index = PAPI_Libs.FindIndex(p => p.Id == papi_lib.Id);
        if(index == -1)
            return; */

        /* (3/24/24, 23) Replacing the above code with cannibalized code from step #6 in the "Code the CRUD operations" 
        section of the instructions/guidance available from 
        https://learn.microsoft.com/en-us/training/modules/persist-data-ef-core/4-interacting-data): */
        var pAPI_LibToUpdate = _context.PAPI_Libs.Find(papi_lib.Id);

        if (pAPI_LibToUpdate is null)
        {
            throw new InvalidOperationException("Error: PAPI-Lib with corresponding ID value not found.");
        }

        /* (3/22/24, 5) Past this point, BASICALLY just copied/pasted the `switch` statement from the `Add()` method in this file
        and replaced any reference to `PAPI_Libs.Add(papi_lib);` with `PAPI_Libs[index] = papi_lib` AND "culled" unnecessary code 
        from the original version of this `switch` statement that would be unnecessary in a `PUT` action (!): */
        Random putNumberGenerator = new Random();

        switch(pAPI_LibToUpdate.TemplateId)
        {
            /* (3/22/24, 6) CRUCIAL - I ALMOST forgot to increment the value tested in each `case` block by 1 (in the original version of 
            this `switch` statement in the `Add()` method in this file, each case was testing the ZERO-INDEXED INDEX VALUE of a `PAPI_LibTemplate` 
            object that would exist in the `PAPI_LibTemplates` List in this file; in THIS `switch` statement, on the other hand, we want to test the 
            `TemplateId` value of a given `PAPI_Lib` object passed into this service's `Update()` method via the PAPI_Lib controller): */
            case 1:
                int selectedFruitId = putNumberGenerator.Next(64, 73);

                async Task<string> GetFruit(int id)
                {
                    string url = String.Format(pAPI_LibToUpdate.ApiUrlOne, id);
                    
                    using (HttpClient client = new HttpClient())
                    {
                        string fruityviceResponse = await client.GetStringAsync(url);
                        
                        Fruit fruit = JsonSerializer.Deserialize<Fruit>(fruityviceResponse);

                        return fruit.Name;
                    }
                }

                string fruit = await GetFruit(selectedFruitId);
                string fruitFormatted = fruit.ToLower();

                async Task<string> GetCorporateBuzzwords()
                {
                    string url = pAPI_LibToUpdate.ApiUrlTwo;
                    using (HttpClient client = new HttpClient())
                    {
                        string corporateBuzzwordsResponse = await client.GetStringAsync(url);
                        CorporateBuzzwords corporateBuzzwords = JsonSerializer.Deserialize<CorporateBuzzwords>(corporateBuzzwordsResponse);
                        return corporateBuzzwords.Phrase;
                    }
                }

                string corporateBs = await GetCorporateBuzzwords();
                string corporateBsFormatted = corporateBs.ToLower();

                pAPI_LibToUpdate.CompletedString = $"When life gives you {fruitFormatted}, {corporateBsFormatted}.";

                /* (3/24/24, 24) NOTE that all things considered, it SEEMS (?!) like the changeover from how you had things 
                structured to a project that integrates EF Core IS rather simple in regard to modifying the methods in the 
                applicable service! Note how immediately below this comment, we are simply commenting-out the original code 
                we had in this method and replacing it with `_context.SaveChanges();` (adapted from code shown in 
                step #6 in the "Code the CRUD operations" section of the instructions/guidance available from 
                https://learn.microsoft.com/en-us/training/modules/persist-data-ef-core/4-interacting-data); NOTE AS WELL 
                that in order for this code change to work, you had to remove the `static` modifier from the method signature 
                for the `Add()` method: */
                //PAPI_Libs[index] = papi_lib;
                _context.SaveChanges();
                break;
            case 2:
                async Task<string> GetGenre()
                {
                    string url = pAPI_LibToUpdate.ApiUrlOne;
                    
                    using (HttpClient client = new HttpClient())
                    {
                        return await client.GetStringAsync(url);
                    }
                }

                string musicGenre = await GetGenre();
                musicGenre = musicGenre.Trim('"');
                string musicGenreFormatted = UppercaseFirst(musicGenre);

                pAPI_LibToUpdate.CompletedString = $"{musicGenreFormatted} makes the heart grow fonder.";
                
                /* (3/24/24, 25) NOTE that all things considered, it SEEMS (?!) like the changeover from how you had things 
                structured to a project that integrates EF Core IS rather simple in regard to modifying the methods in the 
                applicable service! Note how immediately below this comment, we are simply commenting-out the original code 
                we had in this method and replacing it with `_context.SaveChanges();` (adapted from code shown in 
                step #6 in the "Code the CRUD operations" section of the instructions/guidance available from 
                https://learn.microsoft.com/en-us/training/modules/persist-data-ef-core/4-interacting-data); NOTE AS WELL 
                that in order for this code change to work, you had to remove the `static` modifier from the method signature 
                for the `Add()` method: */
                //PAPI_Libs[index] = papi_lib;
                _context.SaveChanges();
                break;
            case 3:
                async Task<string> GetStory()
                {
                    string url = pAPI_LibToUpdate.ApiUrlOne;
                    
                    using (HttpClient client = new HttpClient())
                    {
                        return await client.GetStringAsync(url);
                    }
                }

                string musicStory = await GetStory();
                musicStory = musicStory.Trim('"');
                string musicStoryFormatted = musicStory.Remove(musicStory.Length - 1);
                musicStoryFormatted += ",";

                pAPI_LibToUpdate.CompletedString = $"As Jesus was walking beside the Sea of Galilee, he saw two brothers, Simon called Peter and his brother Andrew. They were casting a net into the lake, for they were fishermen. '{musicStoryFormatted}' Jesus said. At once they left their nets and followed him.";
                
                /* (3/24/24, 26) NOTE that all things considered, it SEEMS (?!) like the changeover from how you had things 
                structured to a project that integrates EF Core IS rather simple in regard to modifying the methods in the 
                applicable service! Note how immediately below this comment, we are simply commenting-out the original code 
                we had in this method and replacing it with `_context.SaveChanges();` (adapted from code shown in 
                step #6 in the "Code the CRUD operations" section of the instructions/guidance available from 
                https://learn.microsoft.com/en-us/training/modules/persist-data-ef-core/4-interacting-data); NOTE AS WELL 
                that in order for this code change to work, you had to remove the `static` modifier from the method signature 
                for the `Add()` method: */
                //PAPI_Libs[index] = papi_lib;
                _context.SaveChanges();
                break;
            case 4:
                int selectedBookId = putNumberGenerator.Next(1, 1001);

                async Task<string> GetBook(int id)
                {
                    string url = String.Format(pAPI_LibToUpdate.ApiUrlOne, id);
                    
                    using (HttpClient client = new HttpClient())
                    {
                        string gutenedexResponse =  await client.GetStringAsync(url);

                        Book book = JsonSerializer.Deserialize<Book>(gutenedexResponse);

                        return book.Title;
                    }
                }

                string book = await GetBook(selectedBookId);

                pAPI_LibToUpdate.CompletedString = $"'{book}' is the Guide Which I Will Never Abandon.";

                /* (3/24/24, 27) NOTE that all things considered, it SEEMS (?!) like the changeover from how you had things 
                structured to a project that integrates EF Core IS rather simple in regard to modifying the methods in the 
                applicable service! Note how immediately below this comment, we are simply commenting-out the original code 
                we had in this method and replacing it with `_context.SaveChanges();` (adapted from code shown in 
                step #6 in the "Code the CRUD operations" section of the instructions/guidance available from 
                https://learn.microsoft.com/en-us/training/modules/persist-data-ef-core/4-interacting-data); NOTE AS WELL 
                that in order for this code change to work, you had to remove the `static` modifier from the method signature 
                for the `Add()` method: */
                //PAPI_Libs[index] = papi_lib;
                _context.SaveChanges();
                break;
            case 5:
                List<int> idList = new List<int>
                {
                    16460,
                    72520,
                    207528,
                    261876,
                    259686,
                    269278,
                    270797,
                    282161,
                    344619,
                    347567,
                    907116,
                    905064,
                    889576,
                    888476,
                    853075,
                    814822,
                    815150,
                    787967,
                    782239,
                    756448,
                    751191,
                    716307,
                    698842,
                    687539,
                    569710,
                    495324
                };

                int randomIndex = putNumberGenerator.Next(idList.Count);
                int selectedArtworkId = idList[randomIndex];

                async Task<string> GetArtwork(int id)
                {
                    string url = String.Format(pAPI_LibToUpdate.ApiUrlOne, id);
                    
                    using (HttpClient client = new HttpClient())
                    {
                        string collectionApiResponse =  await client.GetStringAsync(url);

                        Artwork artWork = JsonSerializer.Deserialize<Artwork>(collectionApiResponse);

                        return artWork.Title;
                    }
                }

                string artWork = await GetArtwork(selectedArtworkId);

                pAPI_LibToUpdate.CompletedString = $"I really believe that if you practice enough, you could paint '{artWork}' with a two-inch brush.";

                /* (3/24/24, 28) NOTE that all things considered, it SEEMS (?!) like the changeover from how you had things 
                structured to a project that integrates EF Core IS rather simple in regard to modifying the methods in the 
                applicable service! Note how immediately below this comment, we are simply commenting-out the original code 
                we had in this method and replacing it with `_context.SaveChanges();` (adapted from code shown in 
                step #6 in the "Code the CRUD operations" section of the instructions/guidance available from 
                https://learn.microsoft.com/en-us/training/modules/persist-data-ef-core/4-interacting-data); NOTE AS WELL 
                that in order for this code change to work, you had to remove the `static` modifier from the method signature 
                for the `Add()` method: */
                //PAPI_Libs[index] = papi_lib;
                _context.SaveChanges();
                break;
        }        
    }

    public static string UppercaseFirst(string s)
    {
        // Return char and concat substring.
        return char.ToUpper(s[0]) + s.Substring(1);
    }
}